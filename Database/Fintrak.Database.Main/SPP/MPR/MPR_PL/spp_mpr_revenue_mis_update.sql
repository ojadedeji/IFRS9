Create proc [dbo].[spp_mpr_revenue_mis_update]
as

/* Fix Tag Team and Account Offficer MIS to Accounts*/
Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

--IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'mpr_team_structure') DROP TABLE mpr_team_structure;

--Select b.Code MIS, b.ParentCode, d.Code ParentMIS into mpr_team_structure from mpr_team b left outer join mpr_team d on 
--b.Parentcode = d.Code 
--and d.Active = 1 and d.Deleted = 0 and b.Year = d.Year 
--where  b.Active = 1 and b.Deleted = 0 and b.Year = Year(@Rundate);

--Account Table mis update
Update mpr_revenue
set TeamCode = a.TeamCode,
AccountOfficerCode = a.AccountOfficerCode
from dbo.cor_cust_account a where a.AccountNo = mpr_revenue.relatedaccount;

--Fix Team code of Account Officers
Update mpr_revenue
set TeamCode = a.ParentCode
from mpr_team a where  a.Year = Year(@Rundate) and
 a.Code = mpr_revenue.AccountOfficerCode;



--GL mis update
Update mpr_revenue
set TeamCode = a.TeamCode,
AccountOfficerCode = a.accountofficercode
from dbo.mpr_glmis a where a.GLAccount = mpr_revenue.glcode;

--Branch Update
Update mpr_revenue
set TeamCode = a.MisCode,
AccountOfficerCode = 'n/a'
from dbo.mpr_branchdefaultmis a where a.BranchCode = mpr_revenue.BranchCode
and mpr_revenue.TeamCode is null
and a.Deleted = 0 and a.Active = 1;

--Fintrak Account MIS
Update mpr_revenue
set TeamCode = a.TeamCode,
AccountOfficerCode = a.AccountOfficerCode
from dbo.mpr_accountmis a where
a.accountno = mpr_revenue.relatedaccount and a.Deleted = 0 and a.Active = 1;





















