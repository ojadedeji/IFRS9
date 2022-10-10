Create proc spp_cor_fiscalperiod_autocreate
(
	@YEAR INT
)
as
declare @Count int = (select count(*) from cor_fiscalperiod a where year(enddate) = @YEAR)
declare @Loop int = 1
declare @month varchar(20), @Yearid int

if @Count < 12
Begin
	delete from cor_fiscalperiod where year(enddate) = @YEAR

	While @Loop <= 12
	Begin

		Set @month  = (Select Case @Loop when 1 Then 'JAN'
					When 2 Then 'FEB' When 3 Then 'MAR'			
					When 4 Then 'APR' When 5 Then 'MAY'
					When 6 Then 'JUN' When 7 Then 'JUL'
					When 8 Then 'AUG' When 9 Then 'SEP'
					When 10 Then 'OCT' When 11 Then 'NOV'
					When 12 Then 'DEC'	End);
		Set @Yearid = (Select a.FiscalYearId from cor_fiscalyear a where DATEADD(mm, DATEDIFF(mm,0,'01-'+@month+ cast(@year as varchar(4))) + 1, -1)
				between StartDate and EndDate and active = 1 and Deleted = 0)

		Insert into cor_fiscalperiod(Name, StartDate, EndDate, FiscalYearId, Closed, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		Select @month Name,DATEADD(mm, DATEDIFF(mm,0,'01-'+@month+ cast(@year as varchar(4))), 0) AS StartDate, 
		DATEADD(mm, DATEDIFF(mm,0,'01-'+@month+ cast(@year as varchar(4))) + 1, -1) AS EndDate,@Yearid ,
		0 Closed,1 Active,0 Deleted,'System' CreatedBy,	GETDATE() CreatedOn,'System' UpdatedBy, GETDATE() UpdatedOn;

		Set @Loop = @Loop + 1
	End
End








