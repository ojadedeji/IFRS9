Create proc [dbo].[spp_mpr_transfer_price_autocreate]
(
	@Year int,
	@SolutionId int
)
as
declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');
declare @maxYear int, @maxPeriod int

--Product based pool setup
If Not Exists ( select * from  mpr_transfer_price  where Year = @Year and Period = Month(@Rundate) and SolutionId = @SolutionId)
Begin
	select @maxYear = Max([Year]) from mpr_transfer_price;
	select @maxPeriod = Max(Period) from mpr_transfer_price where [year] = @maxYear;

	Insert into mpr_transfer_price(ProductCode, CaptionCode, Rate, Year, Period, DefinitionCode, MisCode, CompanyCode, SolutionId, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
	SELECT    ProductCode, CaptionCode, Rate,@Year Year,Month(@Rundate) Period, DefinitionCode, MisCode, CompanyCode, SolutionId, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn
	FROM            mpr_transfer_price 
	where Year = @maxYear and Period = @maxPeriod and SolutionId = @SolutionId and Active = 1 and Deleted = 0
End



--General pool setup
If Not Exists ( select * from mpr_general_transfer_price  where Year = @Year and Period = Month(@Rundate))
Begin
	select @maxYear = Max([Year]) from mpr_general_transfer_price;
	select @maxPeriod = Max(Period) from mpr_general_transfer_price where [year] = @maxYear;

	Insert into mpr_general_transfer_price(MisCode, DefinitionCode,  Category, CurrencyType, Rate,Year, Period, CompanyCode, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
	SELECT MisCode, DefinitionCode,Category, CurrencyType, Rate, @Year Year,Month(@Rundate) Period, CompanyCode, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn
	FROM            mpr_general_transfer_price where Year = @maxYear and Period = @maxPeriod and Active = 1 and Deleted = 0
End



--account pool
If Not Exists ( select * from mpr_account_transfer_price  where Year = @Year and Period = Month(@Rundate))
Begin
	select @maxYear = Max([Year]) from mpr_account_transfer_price;
	select @maxPeriod = Max(Period) from mpr_account_transfer_price where [year] = @maxYear

	Insert into mpr_account_transfer_price(AccountNo, Category, Rate, Year, Period, SolutionId, Active, Deleted, CreatedBy, CreatedOn, UpdatedOn, CompanyCode)
	SELECT AccountNo, Category, Rate,@Year Year,Month(@Rundate) Period, SolutionId, Active, Deleted, CreatedBy, CreatedOn, UpdatedOn, CompanyCode
	FROM  mpr_account_transfer_price where Year = @maxYear and Period = @maxPeriod and Active = 1 and Deleted = 0
End