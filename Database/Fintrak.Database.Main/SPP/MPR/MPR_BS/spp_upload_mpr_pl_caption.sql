Create proc spp_upload_mpr_pl_caption
(   @Code varchar(50), 
	@Caption varchar(200), 
	@Type varchar(50),
	@Position int,
	@CompanyCode varchar(50),
	@Parent varchar(50),
	@username varchar(50)
)
as

If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End

If Not Exists (Select * from mpr_pl_caption b where b.code = @code and b.CompanyCode = @CompanyCode  )
Begin
	Insert into mpr_pl_caption(Code, Name, AccountType, Position,CompanyCode,ParentCode,Color, CreatedOn, CreatedBy)
	Select @Code, @Caption,@Type,@Position,@CompanyCode,@Parent, 'transparent', GETDATE(), @username
End
Else
Begin
	Update mpr_pl_caption
	set Code = @Code , Name = @Caption, AccountType = @Type, Position = @Position,ParentCode = @Parent,
	UpdatedOn = GETDATE(), UpdatedBy = @username 
	where code = @code and CompanyCode = @CompanyCode
End
