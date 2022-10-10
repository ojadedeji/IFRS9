create proc [dbo].[spp_upload_mpr_balancesheet]
(
	@AccountNo varchar(20), 
	@AccountName varchar(100), 
	@TeamCode varchar(50), 
	@AccountOfficerCode varchar(50), 
	@ProductCode varchar(20), 
	@Category varchar(20), 
	@CurrencyType varchar(20), 
	@ActualBal money, 
	@AverageBal money, 
	@Interest money, 
	--@Rundate date, 
	@CompanyCode varchar(20),
	@username varchar(50)
)
as

Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');
declare @PoolRate decimal(18,2), @Pool decimal(18,2), @CatID int, @CaptionCode varchar(20), @BSType varchar(50), @CurCode varchar(50)
declare @VolGL varchar(50), @IntGL varchar(50), @AcctStatus varchar(20)
declare @CurID int = (select a.id from cor_currencytype a where a.name = @CurrencyType)

declare @NDY int = case when year(@rundate)  % 4 = 0 then 366 else 365 end;

declare @Caption varchar(200) = (Select a.CaptionName from dbo.vw_mpr_product a 
where a.ProductCode = @ProductCode and a.Category = @Category and a.CurrencyType = @CurrencyType)

Select @CaptionCode = CaptionCode, @BSType = c.BSType from dbo.vw_mpr_bs_caption c where CaptionName = @Caption ;


if @Caption is null or @Caption = ''
Begin
	select @Caption = a.CaptionName  from dbo.vw_mpr_bs_gl a where a.glcode = @ProductCode and a.Category = @Category and a.CurrencyType = @CurrencyType
End

If @TeamCode is null or @TeamCode = ''
Begin
	Select @TeamCode = a.TeamCode from cor_cust_account a where a.AccountNo = @AccountNo 
End;

If @AccountOfficerCode is null or @AccountOfficerCode = ''
Begin
	Select @AccountOfficerCode = a.AccountOfficerCode from cor_cust_account a where a.AccountNo = @AccountNo 
End;

if Exists (select * from cor_cust_account where AccountNo = @AccountNo )
Begin
	Select @AccountName = a.AccountName,@CurCode =  Currency, @AcctStatus = [Status] from cor_cust_account a where a.AccountNo = @AccountNo 

End

Set @CatID = (Select a.id from cor_accounttype a where a.name = @Category)

--Pick Pool rate
 If Exists (select * from mpr_account_transfer_price a where a.Year = Year(@Rundate) and Period = Month(@Rundate) and a.AccountNo = @AccountNo and Category= @CatID )
 Begin --Check for Account Pool
	 Set @PoolRate = (select a.Rate from mpr_account_transfer_price a where a.Year = Year(@Rundate) and Period = Month(@Rundate) and a.AccountNo = @AccountNo and Category= @CatID)
 End
 Else if Exists (Select * from mpr_transfer_price a where a.Year = Year(@Rundate) and Period = Month(@Rundate)
 and a.ProductCode = @ProductCode and CaptionCode = @CaptionCode and a.MisCode in (select b.parentMIS  from distinct_team_mapping b where b.MIS = @TeamCode))
 Begin --Apply Product Pool if exists
	Set @PoolRate = (Select top 1 a.Rate from mpr_transfer_price a inner join mpr_team_definition b on a.DefinitionCode = b.Code and a.Year = b.Year 
	 where a.Year = Year(@Rundate) and Period = Month(@Rundate) and  a.ProductCode = @ProductCode and CaptionCode = @CaptionCode and
	a.MisCode in (select b.parentMIS  from distinct_team_mapping b where b.MIS = @TeamCode) order by b.Position asc)
 End
 Else
 Begin --otherwise use general pool
	Set @PoolRate = (Select top 1 a.Rate from mpr_general_transfer_price a inner join mpr_team_definition b on a.DefinitionCode = b.Code and a.Year = b.Year 
	 where a.Year = Year(@Rundate) and Period = Month(@Rundate) and a.Category = @CatID and a.CurrencyType = @CurID and
	a.MisCode in (select b.parentMIS  from distinct_team_mapping b where b.MIS = @TeamCode) order by b.Position asc)

 End

 Set @Pool = @AverageBal * @PoolRate/100 * cast(day(@rundate) as decimal(18,0))/ @NDY;

 if Exists (select * from mpr_product b where b.ProductCode = @ProductCode )
 Begin
 select @VolGL = a.VolumeGL ,@IntGL = a.InterestGL from vw_mpr_product a where a.ProductCode = @ProductCode and a.Category = @Category and a.CurrencyType = @CurrencyType
 End
 Else
 Begin
	Set @VolGL = @ProductCode 
	Set @IntGL = @ProductCode 
 End

Insert into mpr_balancesheet_adjustment (AccountNo, AccountName, TeamCode, AccountOfficerCode, ProductCode, Category, CurrencyType, ActualBal, AverageBal, 
Interest, RunDate, CompanyCode )
Select @AccountNo, @AccountName, @TeamCode, @AccountOfficerCode, @ProductCode, @Category, @CurrencyType, @ActualBal, @AverageBal, 
@Interest, @RunDate, @CompanyCode



Insert into mpr_balancesheet (AccountNo, AccountName, TeamCode, AccountOfficerCode, ProductCode, Category, CurrencyType, ActualBal, AverageBal, 
Interest, RunDate, CompanyCode , CaptionName,Pool, PoolRate, active,deleted,createdby,createdon,updatedby,updatedon,EntryStatus, BalanceSheetType,  Currency, VolumeGL , InterestGL, AcctStatus  )
Select @AccountNo, @AccountName, @TeamCode, @AccountOfficerCode, @ProductCode, @Category, @CurrencyType, @ActualBal, @AverageBal, 
@Interest, @RunDate, @CompanyCode , @Caption ,@Pool, @PoolRate,'true','false',@username,getdate(),@username,getdate(), 'Manual Entry' EntryStatus,@BSType,  @CurCode, @VolGL , @IntGL , @AcctStatus


EXEC cor_auto_complete_table_upload mpr_balancesheet_adjustment;
EXEC cor_auto_complete_table_upload mpr_balancesheet;



