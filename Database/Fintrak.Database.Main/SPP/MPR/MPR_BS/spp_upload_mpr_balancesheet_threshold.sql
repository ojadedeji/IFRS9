Create proc spp_upload_mpr_balancesheet_threshold
(   @Product varchar(50), 
	@Caption varchar(200), 
	@Rate decimal(18,2), 
	@CompanyCode varchar(50),
	@username varchar(50)
)
as
declare @Captioncode varchar(20)

Set @Captioncode = (Select b.CaptionCode from mpr_bs_caption b where b.CaptionName = @Caption)

If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End

If Not Exists (Select * from mpr_balancesheet_threshold b where b.ProductCode = @Product and b.CaptionCode = @Captioncode 
and CompanyCode = @CompanyCode )
Begin
	Insert into mpr_balancesheet_threshold(ProductCode, CaptionCode, Rate, CompanyCode, CreatedOn, CreatedBy)
	Select @Product, @Captioncode,@Rate,@CompanyCode, GETDATE(), @username
End
Else
Begin
	Update mpr_balancesheet_threshold
	set Rate = @Rate,
	UpdatedOn = GETDATE(),
	UpdatedBy = @username
	where ProductCode = @Product and CaptionCode = @Captioncode and CompanyCode = @CompanyCode
End

