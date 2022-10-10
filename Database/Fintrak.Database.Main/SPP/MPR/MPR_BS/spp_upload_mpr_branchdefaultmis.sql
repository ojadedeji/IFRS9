Create proc [dbo].[spp_upload_mpr_branchdefaultmis]
(   @BCode varchar(50), 
	@MIS varchar(50),
	@CompanyCode varchar(50),
	@username varchar(50)
)
as

declare @TeamDef varchar(20)
If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End;

declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

Set @TeamDef = (Select b.DefinitionCode from mpr_team b where b.Code = @MIS and b.[Year] = year(@rundate))

If Not Exists (Select * from mpr_branchdefaultmis b where BranchCode = @BCode)
Begin
	Insert into mpr_branchdefaultmis(BranchCode, DefinitionCode, MisCode, Year, CompanyCode, CreatedOn, CreatedBy)
	Select @BCode,@TeamDef,@MIS,year(@rundate),@CompanyCode,GETDATE(), @username
End
Else
Begin
	Update mpr_branchdefaultmis
	set UpdatedOn = GETDATE(),
	UpdatedBy = @username,
	BranchCode = @BCode, DefinitionCode = @TeamDef, MisCode = @MIS, Year = year(@rundate), CompanyCode= @CompanyCode
	where BranchCode = @BCode
End

