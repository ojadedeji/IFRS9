Create proc spp_mpr_account_mis_update
as

/* Fix Tag Team and Account Offficer MIS to Accounts*/

--Account Table mis update
/* Fix Tag Team and Account Offficer MIS to Accounts*/
Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

--Account Table mis update
Update mpr_balancesheet
set TeamCode = a.TeamCode,
AccountOfficerCode = a.AccountOfficerCode
from dbo.cor_cust_account a where a.AccountNo = mpr_balancesheet.AccountNo;

--Fix Team code of Account Officers
Update mpr_balancesheet
set TeamCode = a.ParentCode
from mpr_team a where a.Year = Year(@Rundate) and
 a.Code = mpr_balancesheet.AccountOfficerCode
 and a.CompanyCode = mpr_balancesheet.companycode;



--Product mis update
Update mpr_balancesheet
set TeamCode = a.TeamCode,
AccountOfficerCode = a.accountofficercode
from dbo.vw_mpr_productmis a where a.ProductCode = mpr_balancesheet.ProductCode
and a.CaptionName = mpr_balancesheet.CaptionName;

--Branch Update
Update mpr_balancesheet
set TeamCode = a.MisCode,
AccountOfficerCode = 'n/a'
from dbo.mpr_branchdefaultmis a where a.BranchCode = mpr_balancesheet.BranchCode
and mpr_balancesheet.VolumeGL in (select b.ProductCode from mpr_product b)
and a.Deleted = 0 and a.Active = 1;

--Fintrak Account MIS
Update mpr_balancesheet
set TeamCode = a.TeamCode,
AccountOfficerCode = a.AccountOfficerCode
from dbo.mpr_accountmis a where
a.accountno = mpr_balancesheet.AccountNo and a.Deleted = 0 and a.Active = 1;


































































