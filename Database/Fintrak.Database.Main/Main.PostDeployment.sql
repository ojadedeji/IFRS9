/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--insert into cor_company(Code,Name,Email,Active,Deleted,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
--select 'CMP001','Company Name','example@fintraksoftware.com','true','false','Auto',getdate(),'Auto',getdate()

--declare @companyId int = (select top 1 companyId from cor_company)

--insert into cor_branch(Code,Name,Address,CompanyId,Active,Deleted,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
--select 'BRH001','Branch Name','Lagos',@companyId,'true','false','Auto',getdate(),'Auto',getdate()

--insert into cor_fiscalyear(Name,StartDate,EndDate,Closed,Active,Deleted,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
--select '2015',getdate(),getdate(),'false','true','false','Auto',getdate(),'Auto',getdate()

--insert into mpr_setup (ExcoDefinitionCode,ExcoTeamCode,AccountLenght,Year,Active,Deleted,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
--select '','',0,'2015','true','false','Auto',getdate(),'Auto',getdate()


--Account Type
Insert into cor_accounttype(id,name)
Select 1,	'View'
Union
Select 2,	'Asset'
Union
Select 3,	'Liability'
Union
Select 4,	'Income'
Union
Select 5,	'Expense'
Union
Select 6,	'ContigentAsset'
Union
Select 7,	'ContigentLiability'
Union
Select 8,	'Consolidation';

--Audit Action
Insert into cor_auditaction(id,name,description)
Select 1, 'C',	'Create'
Union
Select 2, 'U',	'Update'
union
Select 3, 'D',	'Delete'
Union 
Select 4, 'E',	'Extraction'

--Security
Insert into cor_securitymode(id,name,description)
Select 1, 'AD',	'Active Directory'
Union
Select 2, 'UP',	'Username & Password'

--IFRS Type
Insert into ifrs_adjustmenttype(id,name)
Select 1, 'GAAP'
Union
Select 2, 'IFRS'

--ON/OFF MPR BS Type
Insert into mpr_balancesheettype(id,name)
Select 1, 'ON'
Union
Select 2, 'OFF'

--Currency Type
Insert into cor_currencytype(id,name)
Select 1, 'LCY'
Union
Select 2, 'FCY'

--IFRS Instruments
Insert into ifrs_instrument(id,name)
select 1, 'Bonds'
Union
Select 2, 'Tbills'
Union
Select 3, 'Loans'
Union
Select 4, 'Equity'
Union
Select 5, 'FinancialLiability'

--DR CR
Insert into cor_indicator(id,name)
select 1, 'DR'
Union
Select 2, 'CR'


Insert into cor_packagestatus(id,name)
select 1, 'Done'
Union
Select 2, 'Pending'
Union
select 3, 'Running'
Union
Select 4, 'Cancel'
Union
Select 5, 'New'
Union
select 6, 'Fail'
Union
Select 7, 'Stop'
Union
Select 8, 'Removed'

Insert into ifrs_risk_rating_type(id,name)
select 1,'Individual'
union
select 2,'Product'
union 
select 3,'Sector'


Insert into cor_producttype(Name, Description, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
Values ('Customer', 'Customer Account',1 ,0 ,'System' ,	GETDATE() ,'System' , GETDATE() );


--Team Definition
Insert into mpr_team_definition(Code, Name, Position,CanClassified, Year,Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
Select 'ACCT', 'Account Officer',1,0, Year(GETDATE()),1 ,0 ,'System' ,	GETDATE() ,'System' , GETDATE()
Union
Select 'TEM', 'Team',2,0,Year(GETDATE()),1 ,0 ,'System' ,	GETDATE() ,'System' , GETDATE()

--Financial Type
INSERT cor_financial_type(Code,Name,ParentId) VALUES ('Asset','Asset',1)
GO
INSERT cor_financial_type (Code,Name,ParentId) VALUES ('BS','BS', NULL)
GO
INSERT cor_financial_type ( Code, Name, ParentId) VALUES ( N'Equity', N'Equity', 1)
GO
INSERT cor_financial_type ( Code, Name, ParentId) VALUES ( 'Expense', 'Expense',5)
GO
INSERT cor_financial_type ( Code, Name, ParentId) VALUES ( N'Income', N'Income', 5)
GO
INSERT cor_financial_type ( Code, Name, ParentId) VALUES (N'Liability', N'Liability', 1)
GO
INSERT cor_financial_type ( Code, Name, ParentId) VALUES ( N'PL', N'PL', NULL)
GO

USE [FintrakDB]

SET IDENTITY_INSERT [dbo].[cor_upload] ON 


INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Global Product', N'COR001', 1, N'spp_upload_global_product', N'spp_truncate_global_product', 1, N'Code,Name,AssetGL,LiabilityGL,IncomeGL,ExpenseGL')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Global Product Mapping', N'COR002', 1, N'spp_upload_global_product_mapping', NULL, 2, N'ProductCode,ProductType')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES (N'IFRS Loan Product', N'IFRS001', 2, N'spp_upload_frs_loan_product', NULL, 3, N'ProductCode,ScheduleTypeCode,MarketRate,PastDueRate')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Collateral Information', N'IFRS002', 2, N'spp_upload_collateral_information', NULL, 4, N'RefNo,AccountNo,Catery,Type,CustomerName,Amount')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'WatchListed Loans', N'IFRS003', 2, N'spp_upload_watchlisted_loans')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Fair Value Basis Exemption', N'IFRS005', 2, N'spp_fair_value_basis_exemption', NULL, 7, N'RefNo,BasisLevel,InstrumentType,')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Impairment Override', N'IFRS004', 2, N'ssp_impairment_override', NULL, 6, N'RefNo,AccountNo,Classification,Reason')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'IFRS GL Mapping', N'IFRS006', 2, N'spp_gl_mapping', NULL, 8, N'GLCode, GLDescription, GLParentCode, MainCaption, SubCaption, SubCaption1, SubCaption2, SubCaption3, SubCaption4')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'IFRS Instrument GL Mapping', N'IFRS007', 2, N'spp_instrument_type_gl_map', NULL, 9, N'InstrumentTypeId, GLTypeId, GLCode')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES (N'GAAP TrialBalance', N'IFRS008', 2, N'spp_gaap_trial_balance', NULL, 10, N'BranchCode, GLCode, Description, GLSubHeadCode, Currency, ExchangeRate, Debit, Credit, LCY_Debit, LCY_Credit, Balance, LCY_Balance, GLType, RevaluationDiff, TransDate')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'GAAP Adjustment', N'IFRS010', 2, N'spp_upload_gaap_adjustment', NULL, 11, N'GLCode, Narration, Indicator, Amount, BranchCode, Currency, RunDate,AdjustmentType')
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'IFRS Adjustment', N'IFRS011', 2, N'spp_ifrs_adjustment', NULL, 12, N'GLCode, Narration, Indicator, Amount, CompanyCode, Currency, RunDate,', 0, 0, N'fintrak', CAST(N'2015-10-09 11:39:34.840' AS DateTime), N'fintrak', CAST(N'2015-10-21 09:41:36.790' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES (N'Profit Centre Classification', N'MPR001', 3, N'spp_profit_centre_classification', NULL, 13, N'Code, Name, Description, Year', 0, 0, N'fintrak', CAST(N'2015-10-09 11:47:11.910' AS DateTime), N'fintrak', CAST(N'2015-10-09 11:47:28.877' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Profit Centre Team', N'MPR002', 3, N'spp_profit_centre_team', NULL, 14, N'Code, Name, ParentCode, DefinitionCode', 0, 0, N'fintrak', CAST(N'2015-10-09 11:49:24.013' AS DateTime), N'fintrak', CAST(N'2015-10-09 11:49:24.013' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Officer''s Profile', N'MPR003', 3, N'spp_officer_profile', NULL, 15, N'MisCode, StaffID, Email, Phone, Year', 0, 0, N'fintrak', CAST(N'2015-10-09 11:51:46.990' AS DateTime), N'fintrak', CAST(N'2015-10-09 11:51:46.990' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Branch Default MIS', N'MPR004', 3, N'spp_ branch_default_mis', NULL, 16, N'BranchCode, DefinitionCode, MisCode, Year', 0, 0, N'fintrak', CAST(N'2015-10-09 11:54:45.173' AS DateTime), N'fintrak', CAST(N'2015-10-09 11:54:45.173' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'MPR Management Tree', N'MPR005', 3, N'spp_mpr_management_tree', NULL, 17, N'AccountNo, TeamDefinitionCode, TeamCode, AccountOfficerDefinitionCode, AccountOfficerCode, Rate, Year', 0, 0, N'fintrak', CAST(N'2015-10-09 11:57:03.060' AS DateTime), N'fintrak', CAST(N'2015-10-09 11:57:03.060' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'MPR Account MIS', N'MPR006', 3, N'spp_mpr_account_mis', NULL, 18, N'AccountNo, TeamDefinitionCode, TeamCode, AccountOfficerDefinitionCode, AccountOfficerCode, Year', 0, 0, N'fintrak', CAST(N'2015-10-09 11:59:43.363' AS DateTime), N'fintrak', CAST(N'2015-10-09 11:59:43.363' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'MPR MIS Replacement', N'MPR007', 3, N'spp_mpr_mis_replacement', NULL, 19, N'OldMISCode, MISCode, DefinitionCode, Year', 0, 0, N'fintrak', CAST(N'2015-10-09 12:01:39.620' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:01:39.620' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'BalanceSheet Captions', N'MPR008', 3, N'spp_balancesheet_caption', NULL, 20, N'CaptionCode, CaptionName, Catery, CurrencyType, BalanceSheetType, Position, Parent', 0, 0, N'fintrak', CAST(N'2015-10-09 12:05:44.443' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:05:44.443' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'MPR Product', N'MPR009', 3, N'spp_mpr_product', NULL, 21, N'ProductCode, CaptionCode, VolumeGL, InterestGL, Budgetable, IsNotional', 0, 0, N'fintrak', CAST(N'2015-10-09 12:13:51.910' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:13:51.910' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Product MIS', N'MPR010', 3, N'spp_product_mis', NULL, 22, N'ProductCode, CaptionCode, TeamDefinitionCode, AccountOfficerDefinitionCode, TeamCode, AccountOfficerCode, Year', 0, 0, N'fintrak', CAST(N'2015-10-09 12:15:07.447' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:15:07.447' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Balancesheet Threshold', N'MPR011', 3, N'spp_balancesheet_threshold', NULL, 23, N'ProductCode, CaptionCode, Rate', 0, 0, N'fintrak', CAST(N'2015-10-09 12:16:37.913' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:16:37.913' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Product Based Transfer Pricing', N'MPR012', 3, N'spp_transfer_pricing', NULL, 24, N'ProductCode, CaptionCode, Rate, Year, Period, DefinitionCode, MisCode', 0, 0, N'fintrak', CAST(N'2015-10-09 12:19:33.227' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:19:33.227' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Account Transfer Price', N'MPR013', 3, N'spp_account_transfer_pricing', NULL, 25, N'AccountNo, Catery, Rate, Year, Period', 0, 0, N'fintrak', CAST(N'2015-10-09 12:21:42.410' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:21:42.410' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'MPR GL Mapping', N'MPR014', 3, N'spp_mpr_gl_mapping', NULL, 26, N'GLCode, CaptionCode', 0, 0, N'fintrak', CAST(N'2015-10-09 12:23:46.383' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:23:46.383' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'MPR Gl MIS', N'MPR015', 3, N'spp_mpr_gl_mis', NULL, 27, N'GLAccount, TeamDefinitionCode, AccountOfficerDefinitionCode, TeamCode, AccountOfficerCode', 0, 0, N'fintrak', CAST(N'2015-10-09 12:25:26.570' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:25:37.720' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'General Transfer Price', N'MPR016', 3, N'spp_upload_mpr_general_transfer_price', NULL, 24, N'Catery, CurrenyType, MISCode, Rate, CompanyCode, Year, Period', 0, 0, N'fintrak', CAST(N'2015-10-09 12:25:26.570' AS DateTime), N'fintrak', CAST(N'2015-10-09 12:25:37.720' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'P&L Income Adjustment', N'MPR017', 3, N'spp_upload_mpr_pl_income_report', NULL, 25, N'TeamCode, AccountOfficerCode, Narrative, BranchCode, GLCode, Caption, RelatedAccount, Amount, RunDate', 0, 0, N'fintrak', CAST(N'2015-10-21 17:32:27.077' AS DateTime), N'fintrak', CAST(N'2015-10-21 17:32:27.077' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'MPR Balancesheet Adjustment', N'MPR018', 3, N'spp_upload_mpr_balancesheet', NULL, 18, N'AccountNo, AccountName, TeamCode, AccountOfficerCode, ProductCode, Catery, CurrencyType, ActualBal, AverageBal, Interest, RunDate,CompanyCode', 0, 0, N'fintrak', CAST(N'2015-10-22 09:34:45.117' AS DateTime), N'fintrak', CAST(N'2015-10-22 09:34:45.117' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'CDQM Adress', NULL, 10, N'sp_address_upload', NULL, 1, N'StreetName,City,', 0, 0, N'fintrak', CAST(N'2015-10-27 11:56:05.127' AS DateTime), N'fintrak', CAST(N'2015-10-27 11:56:05.127' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Staff Data', NULL, 12, N'spp_upload_cor_staff', N'11', 1, N'StaffCode,Name,Email,Phone', 1, 0, N'fintrak', CAST(N'2015-11-25 11:05:58.490' AS DateTime), N'fintrak', CAST(N'2015-11-25 11:05:58.490' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Pay Grade Data', NULL, 12, N'spp_upload_cor_paygrade', N'11', 2, N'Code,Name,GrossPay,NetPay,ThirteenthMonth,Year', 1, 0, N'fintrak', CAST(N'2015-11-25 11:07:35.600' AS DateTime), N'fintrak', CAST(N'2015-11-25 11:07:35.600' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Metrics', NULL, 12, N'spp_upload_scd_kpi', N'1', 4, N'Code,Name,Description,PeriodType,Direction,CateryCode,IsKPICalculated,Formula,AggregateMethod,
IsTargetCalculated,ScoreFormula', 1, 0, N'fintrak', CAST(N'2015-11-25 15:45:30.383' AS DateTime), N'fintrak', CAST(N'2015-11-25 15:45:30.383' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Data Enrty', NULL, 12, N'spp_upload_scd_entry', N'1', 5, N'StaffCode,MISCode,KPICode,Actual,Target,Score,Date,Period,Year', 1, 0, N'fintrak', CAST(N'2015-11-26 08:28:26.157' AS DateTime), N'fintrak', CAST(N'2015-11-26 08:28:26.157' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Registry', N'CA000788JUI', 2, N'spp_', N'', 1, N'TRM,NMM', 1, 0, N'fintrak', CAST(N'2015-12-02 16:24:12.437' AS DateTime), N'fintrak', CAST(N'2015-12-02 16:24:12.437' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Confirm', NULL, 2, N'spp_confirmento', N'spp_truncate_confirm', 1, N'CD,TP,LGD,EEP,PD', 1, 0, N'fintrak', CAST(N'2015-12-03 14:11:03.747' AS DateTime), N'fintrak', CAST(N'2015-12-03 14:11:03.747' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Loans Primary Data', NULL, 2, N'spp_upload_ifrs_loan_primary_data', N'', 3, N'AccountNo, RefNo, ProductCatery, ProductCode, ProductName, ProductType,  BookingDate, Currency, ExchangeRate, Amount, Rate, ValueDate, PeriodicRepaymentAmount, FirstRepaymentdate, MaturityDate, Tenor, InterestRepayFreq, PrincipalRepayFreq, TenorMonth, LD, Schedule_Type, CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 08:53:25.680' AS DateTime), N'fintrak', CAST(N'2015-12-11 08:53:25.680' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Loans Details', NULL, 2, N'spp_upload_ifrs_loans_details', N'', 3, N'AccountNo, RefNo, ProductCatery, ProductCode, ProductName, ProductType, BookingDate, Currency, ExchangeRate, Amount, Rate, ValueDate, PrincipalOutstandingBal, PastDueAmount, Interest_Receiv_Pay_UnEarn,  PeriodicRepaymentAmount, FirstRepaymentdate, PeriodicPrincipalInstallment, PeriodicInterestRepayment, MaturityDate, TenorDays, InterestRepayFreq, PrincipalRepayFreq, InterestRepayFreqInWords, PrincipalRepayFreqInWords,  TenorMonth, Period, Year, RunDate,  CompanyCode, FinacleClassification, SubClassification', 1, 0, N'fintrak', CAST(N'2015-12-11 09:41:37.233' AS DateTime), N'fintrak', CAST(N'2015-12-11 09:41:37.233' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Integral Fee', NULL, 2, N'spp_upload_ifrs_integral_fee', N'', 7, N'AccountNo,RefNo,Date,FeeAmount,Description,CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 11:31:33.837' AS DateTime), N'fintrak', CAST(N'2015-12-11 11:31:33.837' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Customer Information', NULL, 2, N'spp_upload_ifrs_customer', N'', 9, N'CustomerNo,CustomerName,CustType,CreditRating,Country,CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 11:50:59.130' AS DateTime), N'fintrak', CAST(N'2015-12-11 11:50:59.130' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Customer Account', NULL, 2, N'spp_upload_ifrs_customer_account', N'', 10, N'CustomerNo,AccountNo,AccountName,Sector,SubSector,CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 12:04:56.180' AS DateTime), N'fintrak', CAST(N'2015-12-11 12:04:56.180' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Credit Risk Rating', NULL, 2, N'spp_upload_ifrs_credit_risk_rating', N'', 10, N'Code,EP,LGD,PD,Description,CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 12:28:59.667' AS DateTime), N'fintrak', CAST(N'2015-12-11 12:28:59.667' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Bonds', NULL, 2, N'spp_upload_ifrs_bonds', N'spp_truncate_ifrs_bonds', 10, N'RefNo,ValueDate,FaceValue,CleanPrice,IssueDate,PremiumDiscount,CouponRate,CurrentMarketYield,FirstCouponDate,AnualPaymentFreq,Price,Narration,Classification,EffectiveDate,MaturityDate,MrkPrice,Period,Year,Rundate,CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 13:10:56.410' AS DateTime), N'fintrak', CAST(N'2015-12-11 13:10:56.410' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'T-Bills', NULL, 2, N'spp_upload_ifrs_tbills', N'spp_truncate_ifrs_tbills', 12, N'RefNo,Description,EffectiveDate,MaturityDate,CurrentMarketYield,InterestRate,CleanPrice,FaceValue,Classification,Rundate,CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 13:35:41.747' AS DateTime), N'fintrak', CAST(N'2015-12-11 13:35:41.747' AS DateTime))
INSERT [dbo].[cor_upload] ( [Title], [Code], [SolutionId], [Action], [TruncateAction], [Position], [Template]) VALUES ( N'Equity Stocks', NULL, 2, N'spp_upload_ifrs_equity_stocks', N'spp_truncate_ifrs_equity_stocks', 14, N'RefNo,Stock,Cost,MarketQty,MarketPrice,Classification,Quoted,fairvaluebasis,Period,Year,RunDate,CompanyCode', 1, 0, N'fintrak', CAST(N'2015-12-11 14:09:27.643' AS DateTime), N'fintrak', CAST(N'2015-12-11 14:09:27.643' AS DateTime))
SET IDENTITY_INSERT [dbo].[cor_upload] OFF
SET IDENTITY_INSERT [dbo].[cor_uploadrole] ON 
INSERT [dbo].[cor_uploadrole] ( [RoleId],  [Active], [Deleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES ( 1, 1, 0, 0, N'fintrak', CAST(N'2015-09-03 17:17:42.360' AS DateTime), N'fintrak', CAST(N'2015-09-03 17:17:42.360' AS DateTime))
INSERT [dbo].[cor_uploadrole] ( [RoleId],  [Active], [Deleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES ( 6, 6, 0, 0, N'fintrak', CAST(N'2015-11-09 14:27:11.937' AS DateTime), N'fintrak', CAST(N'2015-11-09 14:27:11.937' AS DateTime))
SET IDENTITY_INSERT [dbo].[cor_uploadrole] OFF







