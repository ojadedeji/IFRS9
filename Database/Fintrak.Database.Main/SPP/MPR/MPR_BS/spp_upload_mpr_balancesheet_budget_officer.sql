Create proc spp_upload_mpr_balancesheet_budget_officer
(
	@CompanyCode varchar(20), @Mis varchar(20), @Year int, @Caption varchar(100), 
	@Jan Decimal(18,2), @Feb Decimal(18,2), @Mar Decimal(18,2), @Apr Decimal(18,2), @May Decimal(18,2),
	@Jun Decimal(18,2), @Jul Decimal(18,2), @Aug Decimal(18,2), @Sep Decimal(18,2), @Oct Decimal(18,2),
	@Nov Decimal(18,2), @Dec Decimal(18,2), @username varchar(50)
)
as

If Exists (SELECT CompanyCode, AccountOfficerCode, Year, CaptionName, Mth1, Mth2, Mth3, Mth4, Mth5, Mth6, Mth7, Mth8, Mth9, Mth10, Mth11, Mth12, 
Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn
FROM            mpr_balancesheet_budget_officer
where year = @Year and CaptionName = @Caption and AccountOfficerCode = @Mis and CompanyCode = @CompanyCode)
Begin
	Update mpr_balancesheet_budget_officer 
	SET Mth1 = @Jan,
	Mth2 = @Feb,
	Mth3 = @Mar,
	Mth4 = @Apr,
	Mth5 = @May,
	Mth6 = @Jun,
	Mth7 = @Jul,
	Mth8 = @Aug,
	Mth9 = @Sep,
	Mth10 = @Oct,
	Mth11 = @Nov,
	Mth12 = @Dec,
	UpdatedBy = @username,
	UpdatedOn = GETDATE() 
	where year = @Year and CaptionName = @Caption and AccountOfficerCode = @Mis and CompanyCode = @CompanyCode
End
Else
Begin
	Insert into mpr_balancesheet_budget_officer(CompanyCode,CaptionName, AccountOfficerCode, Year, Mth1, Mth2, Mth3, Mth4, Mth5, Mth6, Mth7, Mth8,
 Mth9, Mth10, Mth11, Mth12,Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
VALUES
	(@CompanyCode, @caption, @Mis, @Year, @Jan , @Feb , @Mar , @Apr , @May , @Jun , @Jul , @Aug , @Sep , @Oct , @Nov , @Dec,1,0,
	@username, GETDATE(), @username , GETDATE() )
End