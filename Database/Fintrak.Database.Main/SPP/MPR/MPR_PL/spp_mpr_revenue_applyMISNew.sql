Create proc [dbo].[spp_mpr_revenue_applyMISNew]
as
/** Fix Closed or collapse MIS by replacing with a Parent MIS**/

-- Apply Team
Update mpr_revenue
set TeamCode = a.MISCode
from dbo.mpr_misreplacement a
where a.oldmiscode = mpr_revenue.TeamCode;

--Apply AcctOfficer
Update mpr_revenue
set AccountOfficerCode = a.MISCode
from dbo.mpr_misreplacement a
where a.oldmiscode = mpr_revenue.AccountOfficerCode;