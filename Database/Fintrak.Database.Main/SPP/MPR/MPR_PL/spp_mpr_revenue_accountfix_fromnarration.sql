
Create proc spp_mpr_revenue_accountfix_fromnarration
as

declare @AcctLen int = ( Select AccountLenght  from mpr_setup)

Update mpr_revenue
set RelatedAccount = null
where GLCode = RelatedAccount ;

Update mpr_revenue
set RelatedAccount = LEFT(Narrative, @acctlen)
where RelatedAccount is null and LEFT(Narrative, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,2, @acctlen)
where RelatedAccount is null and Substring(Narrative,2, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,3, @acctlen)
where RelatedAccount is null and Substring(Narrative,3, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,4, @acctlen)
where RelatedAccount is null and Substring(Narrative,4, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,5, @acctlen)
where RelatedAccount is null and Substring(Narrative,5, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,6, @acctlen)
where RelatedAccount is null and Substring(Narrative,6, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,7, @acctlen)
where RelatedAccount is null and Substring(Narrative,7, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,8, @acctlen)
where RelatedAccount is null and Substring(Narrative,8, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,9, @acctlen)
where RelatedAccount is null and Substring(Narrative,9, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,10, @acctlen)
where RelatedAccount is null and Substring(Narrative,10, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,11, @acctlen)
where RelatedAccount is null and Substring(Narrative,11, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,12, @acctlen)
where RelatedAccount is null and Substring(Narrative,12, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,13, @acctlen)
where RelatedAccount is null and Substring(Narrative,13, @acctlen) in (select a.AccountNo from cor_cust_account a);

Update mpr_revenue
set RelatedAccount = Substring(Narrative,14, @acctlen)
where RelatedAccount is null and Substring(Narrative,14, @acctlen) in (select a.AccountNo from cor_cust_account a);