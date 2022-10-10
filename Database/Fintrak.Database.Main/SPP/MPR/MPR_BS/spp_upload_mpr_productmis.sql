Create proc [dbo].[spp_upload_mpr_productmis]
(   @Product varchar(50), 
	@TeamMIS varchar(50), 
	@AcctOffMIS varchar(50),
	@CompanyCode varchar(50),
	@username varchar(50)
)

as
declare @TeamDef varchar(20),@TeamDef2 varchar(20)
declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

Set @TeamDef2 = (Select b.DefinitionCode from mpr_team b where b.Code = @AcctOffMIS and b.[Year] = year(@rundate))
Set @TeamDef = (Select b.DefinitionCode from mpr_team b where b.Code = @TeamMIS and b.[Year] = year(@rundate))



If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End

If Not Exists (Select * from mpr_productmis b where b.CompanyCode = @CompanyCode and b.ProductCode = @Product)
Begin
	Insert into mpr_productmis(Year,ProductCode, TeamDefinitionCode, AccountOfficerDefinitionCode, TeamCode, AccountOfficerCode, CompanyCode, CreatedOn, CreatedBy)
	Select Year(@Rundate), @Product, @TeamDef,@TeamDef2,@TeamMIS,@AcctOffMIS,@CompanyCode,GETDATE(),@username
End
Else
Begin
	Update mpr_productmis
	set TeamCode=@TeamMIS,
	AccountOfficerCode = @AcctOffMIS,
	UpdatedOn = GETDATE(),
	UpdatedBy = @username,
	TeamDefinitionCode = @TeamDef,
	AccountOfficerDefinitionCode = @TeamDef2
	where CompanyCode = @CompanyCode and ProductCode = @Product
End

