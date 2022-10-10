Create proc [dbo].[spp_upload_mpr_misreplacement]
(   
	@MIS varchar(50), 
	@NewMIS varchar(50),
	@CompanyCode varchar(10),
	@username varchar(50)
)

as
declare @TeamDef varchar(20)
declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

Set @TeamDef = (Select b.DefinitionCode from mpr_team b where Code = @newMIS and b.[Year] = year(@rundate))

If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End


If Not Exists (Select * from mpr_misreplacement b where OldMISCode = @MIS and CompanyCode = @CompanyCode)
Begin
	Insert into mpr_misreplacement(OldMISCode, DefinitionCode, MISCode, CompanyCode,  CreatedOn, CreatedBy)
	Select @MIS, @TeamDef,@NewMIS,@CompanyCode,GETDATE(),@username
End
Else
Begin
	Update mpr_misreplacement
	set DefinitionCode = @TeamDef, MISCode = @NewMIS,
	UpdatedOn = GETDATE(),
	UpdatedBy = @username
	where OldMISCode = @MIS and CompanyCode = @CompanyCode
End

