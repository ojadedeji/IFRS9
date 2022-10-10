Create proc [dbo].[spp_upload_mpr_pl_income_report]
(
	@TeamCode varchar(50), 
	@AccountOfficerCode varchar(50), 
	@Narrative varchar(500), 
	@BranchCode varchar(20), 
	@GLCode varchar(20), 
	@Caption varchar(200), 
	@RelatedAccount varchar(50), 
	@Amount money, 
	@RunDate date,
	@username varchar(50)
)
as
declare @CompanyCode varchar(50) = (Select top 1 a.Code from cor_company a order by a.CompanyId )
declare @accounttitle varchar(250), @custcode varchar(50), @product varchar(50)

declare @MartDate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

Select @accounttitle = a.AccountName,@custcode = a.CustNo, @product = a.ProductCode from cor_cust_account a where a.AccountNo = @RelatedAccount;


Insert into mpr_pl_income_report_adjustment(TeamCode, AccountOfficerCode, Narrative, BranchCode, GLCode, Caption, RelatedAccount, Amount, RunDate, CompanyCode)
SELECT    @TeamCode, @AccountOfficerCode, @Narrative, @BranchCode, @GLCode, @Caption, @RelatedAccount, @Amount, @RunDate, @CompanyCode;


Insert into mpr_pl_income_report(TeamCode, AccountOfficerCode, Narrative, BranchCode, GLCode, Caption, RelatedAccount, Amount, RunDate, martdate, CompanyCode,
Period, Year, GLAccount, AccountTitle, CustCode, ProductCode, EntryStatus,active,deleted,createdby,createdon,updatedby,updatedon)
SELECT    @TeamCode, @AccountOfficerCode, @Narrative, @BranchCode, @GLCode, @Caption, @RelatedAccount, @Amount, @RunDate,@MartDate, @CompanyCode,
Month(@Rundate) , Year(@Rundate), @GLCode, @accounttitle, @custcode, @product, 'Manual Entry'  entrystatus,'true','false',@username,getdate(),@username,getdate();






