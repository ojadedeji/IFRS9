CREATE proc [dbo].[spp_upload_mpr_product]
(   @ProductCode varchar(50),
	@Caption varchar(200), 
	@VolumeGL varchar(50),
	@InterestGL varchar(50),
	@Budgetable bit,
	@Notional bit,
	@CompanyCode varchar(50),
	@username varchar(50)
)

as

If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End;

declare @captioncode varchar(20) = (select CaptionCode from mpr_bs_caption where CaptionName = @Caption )

If Not Exists (Select * from mpr_product b where ProductCode = @ProductCode and CaptionCode = @captioncode)
Begin
	Insert into mpr_product(ProductCode, CaptionCode,CompanyCode , VolumeGL, InterestGL, Budgetable, IsNotional, CreatedOn, CreatedBy)
	Select @ProductCode, @captioncode,@CompanyCode,@VolumeGL,@InterestGL ,@Budgetable, @Notional  , GETDATE(), @username 
End
Else
Begin
	Update mpr_product
	set CaptionCode = @captioncode,
	UpdatedOn = GETDATE(),
	VolumeGL = @VolumeGL , InterestGL  = @InterestGL , Budgetable = @Budgetable , IsNotional = @Notional ,
	UpdatedBy= @username
	where ProductCode = @ProductCode and CaptionCode = @captioncode
End


GO


