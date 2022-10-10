Create View dbo.vw_cor_cust_account
as

SELECT a.CustAccountId, a.CustNo, AccountNo, AccountName,b.CustName, Sector, SubSector, TeamCode, AccountOfficerCode, 
ProductCode, BranchCode, Currency, DateOpened, Status,
                         SettlementAcct,  a.Active, a.Deleted, a.CreatedBy, 
						 a.CreatedOn, a.UpdatedBy, a.UpdatedOn, a.RowVersion
FROM            cor_cust_account AS a left outer join cor_cust b on a.CustNo = b.CustNo 