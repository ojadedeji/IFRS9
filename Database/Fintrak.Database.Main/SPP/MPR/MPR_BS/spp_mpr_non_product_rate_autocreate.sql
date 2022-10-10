CREATE proc [dbo].[spp_mpr_non_product_rate_autocreate]
(
	@Year int,
	@SolutionID int
)
as
declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');
declare @maxYear int, @maxPeriod int

--Product based pool setup
If Not Exists ( select * from mpr_non_product_rate  where Year = @Year and Period = Month(@Rundate) )
Begin
	select @maxYear = Max([Year]) from mpr_non_product_rate;
	select @maxPeriod = Max(Period) from mpr_non_product_rate where [year] = @maxYear;

	Insert into mpr_non_product_rate(ProductCode, Year, Period, Rate, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn, CompanyCode)

	SELECT        ProductCode,@Year Year,Month(@Rundate) Period, Rate, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn, CompanyCode
	FROM            mpr_non_product_rate 
	where Year = @maxYear and Period = @maxPeriod  and Active = 1 and Deleted = 0
End





