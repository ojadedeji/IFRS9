Create proc spp_mpr_balancesheet_applyMISNew
as
/** Fix Closed or collapse MIS by replacing with a Parent MIS**/

-- Apply Team
Update mpr_balancesheet
set TeamCode = a.MISCode
from dbo.mpr_misreplacement a
where a.oldmiscode = mpr_balancesheet.TeamCode;

--Apply AcctOfficer
Update mpr_balancesheet
set AccountOfficerCode = a.MISCode
from dbo.mpr_misreplacement a
where a.oldmiscode = mpr_balancesheet.AccountOfficerCode;
