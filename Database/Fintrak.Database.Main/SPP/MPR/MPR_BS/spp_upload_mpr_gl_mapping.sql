Create proc spp_upload_mpr_gl_mapping
(   @GLCode varchar(50),
	@Caption varchar(200), 
	@CompanyCode varchar(50),
	@username varchar(50)
)

as

If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End

If Not Exists (Select * from mpr_gl_mapping b where b.GLCode = @GLCode and CompanyCode = @CompanyCode)
Begin
	Insert into mpr_gl_mapping(GLCode, CaptionCode,CompanyCode, CreatedOn, CreatedBy)
	Select @GLCode, a.Code,@CompanyCode, GETDATE(), @username from mpr_pl_caption a where a.Name = @Caption and Active = 1 and Deleted = 0 
End
Else
Begin
	Update mpr_gl_mapping
	set CaptionCode = a.Name,
	UpdatedOn = GETDATE(),
	UpdatedBy = @username
	from mpr_pl_caption a where
	a.Name = @Caption and a.Active = 1 and a.Deleted = 0 
	and mpr_gl_mapping.GLCode = @GLCode and mpr_gl_mapping.CompanyCode = @CompanyCode
End

