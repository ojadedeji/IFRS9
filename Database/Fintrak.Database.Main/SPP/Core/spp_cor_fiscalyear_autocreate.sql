Create proc spp_cor_fiscalyear_autocreate
(
	@YEAR INT
)
as
If Not Exists (select * from cor_fiscalyear a where year(enddate) = @YEAR )
Begin
	If Not Exists (select * from cor_fiscalyear a where Name = cast(@YEAR as varchar(100)) )
	Begin
		insert into cor_fiscalyear(Name, StartDate, EndDate, Closed, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
		SELECT @YEAR, DATEADD(yy, DATEDIFF(yy,0,'01-JAN-' + cast(@YEAR as varchar(10))), 0) AS StartOfYear, DATEADD(yy, DATEDIFF(yy,0,'01-JAN-' + cast(@YEAR as varchar(10))) + 1, -1) AS EndOfYear,0 Closed,
		1 Active,0 Deleted,'auto' CreatedBy,
		GETDATE() CreatedOn,'auto' UpdatedBy, GETDATE() UpdatedOn
	End
End



