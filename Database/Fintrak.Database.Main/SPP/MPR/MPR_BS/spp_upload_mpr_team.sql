
Create proc [dbo].[spp_upload_mpr_team]
(   @Code varchar(50), 
	@Name varchar(50), 
	@Parent varchar(50),
	@TeamDefCode decimal(18,2),
	@CompanyCode varchar(50),
	@Year int,
	@username varchar(50)
)
as


If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End

If Not Exists (Select * from mpr_team b where code = @Code and year = @Year)
Begin
	Insert into mpr_team(Code, Name, ParentCode, DefinitionCode, CompanyCode, Year, CreatedOn, CreatedBy)
	Select @Code,@Name,@Parent,@TeamDefCode,@CompanyCode,@Year, GETDATE(), @username
End
Else
Begin
	Update mpr_team
	set UpdatedOn = GETDATE(),
	UpdatedBy = @username,
	Name = @Name, ParentCode = @Parent, DefinitionCode = @TeamDefCode, CompanyCode = @CompanyCode
	where year = @Year and code = @Code 
End

