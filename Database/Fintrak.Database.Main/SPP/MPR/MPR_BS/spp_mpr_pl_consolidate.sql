
Create proc [dbo].[spp_mpr_pl_consolidate]
as

Declare @rundate date = (select top 1 a.RunDate from dbo.vw_cor_solutionrundate a where Alias = 'MPR');
declare @startdate date = (Select DateAdd(mm, DateDiff(mm, 0, @rundate) , 0)); --Get Start Date
declare @INTINCLCY varchar(200),@INTINCFCY varchar(200), @INTEXPLCY varchar(200), @INTEXPFCY varchar(200)
declare @POLINCLCY varchar(200),@POLINCFCY varchar(200), @POLEXPLCY varchar(200), @POLEXPFCY varchar(200)
declare @EXCOMIS varchar(20) = (Select a.ExcoTeamCode from mpr_setup a)
declare @excoacct varchar(20) = (Select top 1 a.Code from mpr_team a where a.[Year] = Year(@rundate) and a.ParentCode = @EXCOMIS)

Delete from mpr_pl_income_report where year =Year(@rundate) and period =month( @rundate);

set @INTINCLCY = (Select name from mpr_pl_caption where code = 'IIL' and active = 1 and deleted= 0)
set @INTINCFCY = (Select name from mpr_pl_caption where code = 'IIF' and active = 1 and deleted= 0)
set @INTEXPLCY = (Select name from mpr_pl_caption where code = 'IEL' and active = 1 and deleted= 0)
set @INTEXPFCY = (Select name from mpr_pl_caption where code = 'IEF' and active = 1 and deleted= 0)

--Pool Caption
set @POLINCLCY = (Select name from mpr_pl_caption where code = 'PIL' and active = 1 and deleted= 0)
set @POLINCFCY = (Select name from mpr_pl_caption where code = 'PIF' and active = 1 and deleted= 0)
set @POLEXPLCY = (Select name from mpr_pl_caption where code = 'PEL' and active = 1 and deleted= 0)
set @POLEXPFCY = (Select name from mpr_pl_caption where code = 'PEF' and active = 1 and deleted= 0)

Insert into mpr_pl_income_report(TransId, transdate, Narrative, TeamCode, AccountOfficerCode, BranchCode, GLCode, GLAccount, 
 Caption, RelatedAccount, AccountTitle, CustCode, ProductCode, 
                          Amount, Period, Year, EntryStatus, RunDate, StaffID, CompanyCode, MartDate)

Select TransId, transdate, Narrative, TeamCode, AccountOfficerCode, BranchCode, GLCode, GLAccount, 
 Caption, RelatedAccount, AccountTitle, CustCode, ProductCode, 
                         Amount, Period, Year, EntryStatus, RunDate, StaffID, CompanyCode, @rundate  from (
SELECT     TransId, transdate, Narrative, TeamCode, AccountOfficerCode, BranchCode, GLCode, GLAccount, 
 Caption, RelatedAccount, AccountTitle, CustCode, ProductCode, 
                         Amount_LCY Amount, Period, Year, EntryStatus, RunDate, StaffID, CompanyCode
FROM            mpr_revenue where year =Year( @rundate) and period =month( @rundate)

Union all
--Interest
Select 'INCINTLCY', @rundate transdate,'Interest Inc on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,a.InterestGL GL, a.InterestGL,@INTINCLCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.Interest,Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Asset' and a.CurrencyType ='LCY'

Union all

Select 'INCINTFCY', @rundate transdate,'Interest Inc on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,a.InterestGL GL, a.InterestGL,@INTINCFCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.Interest,Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Asset' and a.CurrencyType ='FCY'

Union all

Select 'EXPINTLCY', @rundate transdate,'Interest Exp on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,a.InterestGL GL, a.InterestGL,@INTEXPLCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.Interest,Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Liability' and a.CurrencyType ='LCY'

Union all

Select 'EXPINTFCY', @rundate transdate,'Interest Exp on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,a.InterestGL GL, a.InterestGL,@INTEXPFCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.Interest,Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Liability' and a.CurrencyType ='FCY'


--pool
Union all

Select 'EXPPOLLCY', @rundate transdate,'Pool Exp on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,'00-00' GL, '00' AcctGL,@POLEXPLCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.[pool],Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Asset' and a.CurrencyType ='LCY'

Union all

Select 'EXPPOLFCY', @rundate transdate,'Pool Exp on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,'00-00' GL, '00' AcctGL,@POLEXPFCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.[Pool],Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Asset' and a.CurrencyType ='FCY'

Union all

Select 'INCPOLLCY', @rundate transdate,'Pool Inc on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,'00-00' GL, '00' AcctGL,@POLINCLCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.[Pool],Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Liability' and a.CurrencyType ='LCY'

Union all

Select 'INCPOLFCY', @rundate transdate,'Pool Inc on ' + a.AccountName narrative,a.TeamCode ,a.AccountOfficerCode,
a.BranchCode,'00-00' GL, '00' AcctGL,@POLINCFCY Caption, a.AccountNo,a.AccountName,'00' custid, a.ProductCode , a.[Pool],Month(a.RunDate) Period,
year(a.rundate) year, a.EntryStatus ,a.RunDate ,a.StaffID ,a.CompanyCode  from mpr_balancesheet a 
where a.RunDate = @rundate and category = 'Liability' and a.CurrencyType ='FCY'

UNION all

SELECT     '' TransId, '' transdate, ''  Narrative, TeamCode, AccountOfficerCode, '' BranchCode, '' GLCode, '' GLAccount, 
 Description Caption, '' RelatedAccount, '' AccountTitle, '' CustCode, '' ProductCode, 
                         Amount, Period, Year, '' EntryStatus, @rundate RunDate, AccountOfficerCode StaffID, 'BOI' CompanyCode
FROM            mpr_opex_detail_share_summary where year =Year( @rundate) and period =month( @rundate)

) pl

