Create proc [dbo].[spp_upload_mpr_bs_caption]
(   @Code varchar(50), 
	@Caption varchar(200), 
	@Category varchar(50),
	@Currency varchar(50),
	@BSType varchar(50),
	@Position int,
	@CompanyCode varchar(50),
	@Parent varchar(50),
	@username varchar(50)
)
as

If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End;

Declare @CurID int = (Select id from cor_currencytype where name = @Currency)
Declare @BSTypeID int = (Select balancesheettypeid from cor_balancesheettype where balancesheettype = @BSType)
Declare @CategoryID int = (Select balancesheetcategoryid from cor_balancesheetcategory where balancesheetcategory = @Category)



If Not Exists (Select * from mpr_bs_caption b where CaptionCode = @code and CompanyCode = @CompanyCode  )
Begin
	Insert into mpr_bs_caption(CaptionCode, CaptionName, Category,CurrencyType,BalanceSheetType, Position,CompanyCode, CreatedOn, CreatedBy)
	Select @Code, @Caption,@CategoryID,@CurID,@BSTypeID,@Position,@CompanyCode, GETDATE(), @username
End
Else
Begin
	Update mpr_bs_caption
	set CaptionName = @Caption, Category = @CategoryID,CurrencyType = @CurID,BalanceSheetType = @BSTypeID, Position = @Position,
	UpdatedOn = GETDATE(), UpdatedBy = @username 
	where CaptionCode = @code and CompanyCode = @CompanyCode
End


GO


