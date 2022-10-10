Create proc [dbo].[spp_upload_mpr_general_transfer_price]
(   
	@Category varchar(20),
	@Currency varchar(5),
	@MISCode varchar(50), 
	@Rate decimal(18,2), 
	@CompanyCode varchar(50),
	@Year int, 
	@Period int,
	@username varchar(50)
)

as
declare @Captioncode varchar(20),@SolutionId int, @TeamDef varchar(20)
declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

declare @CatID int  = (Select a.id from cor_accounttype a where a.name = @Category )
declare @CurID int  = (Select a.id from cor_currencytype a where a.name = @Currency )


Set @TeamDef = (Select b.DefinitionCode from mpr_team b where b.Code = @MISCode and b.[Year] = year(@rundate))


If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End

If Not Exists (Select * from mpr_general_transfer_price b where b.Year = @Year and b.Period = @Period 
and  CompanyCode = @CompanyCode and Category = @CatID 
and CurrencyType = @CurID and MisCode = @MISCode)
Begin
	Insert into mpr_general_transfer_price(Category, CurrencyType, Rate, DefinitionCode, MisCode, Period, Year, CompanyCode , CreatedOn, CreatedBy)
	Select @CatID, @CurID,@Rate,@TeamDef,@MISCode,@Period,@Year,@CompanyCode, GETDATE(), @username
End
Else
Begin
	Update mpr_general_transfer_price
	set Rate = @Rate,
	UpdatedOn = GETDATE(),
	UpdatedBy = @username,
	DefinitionCode = @TeamDef
	where Year = @Year and Period = @Period 
	and  CompanyCode = @CompanyCode and Category = @CatID 
	and CurrencyType = @CurID and MisCode = @MISCode
End

