Create proc [dbo].[spp_mpr_close_period]
as
declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');
declare @Period int = (Select Month(@Rundate));
declare @Year int = (Select Year(@Rundate));
declare @MPRID int = 0;
Set @MPRID =  (Select a.SolutionId from cor_solution a  where Alias = 'MPR');

exec spp_mpr_close_balancesheet @Rundate; --archive BS


if @Period > 11
Begin
	Set @Period = 1;
	Set @Year = @Year + 1;
	exec spp_mpr_team_autocreate @year; --generate team structure for new year
	exec spp_cor_fiscalyear_autocreate @year; --generate fiscal year
	exec spp_cor_fiscalperiod_autocreate @year; --generate fiscal period 
End
Else
Begin
	Set @Period = @Period + 1;
End

update cor_solutionrundate 
set RunDate = (select DateAdd("Month",1,dbo.fn_getfirstday(@Rundate))),
	UpdatedOn = GETDATE(),
	Deleted = 0,
	Active = 1,
	UpdatedBy = 'auto' where SolutionId = @MPRID;


exec spp_mpr_transfer_price_autocreate @Year,@MPRID; --transfer pricing generation
exec spp_mpr_non_product_rate_autocreate @Year,@MPRID; --Virtual Products' rate

--Set Close to the old rundate
If Exists (Select * from cor_closedperiod where Year([date]) = Year(@Rundate)  and month([date]) = month(@Rundate) and solutionid = @MPRID)
Begin
	Update cor_closedperiod
	set status = 0,
	[date] = @Rundate
	where Year(date) = Year(@Rundate)  and
	 month(date) = month(@Rundate) and solutionid = @MPRID
End
Else
Begin
	Insert into cor_closedperiod(solutionid, date, status,active, deleted, createdby, createdon, updatedby, updatedon)
	values (@MPRID , @Rundate,0,1,0,'auto', getdate(),'auto',getdate())
End


--Push New Run Date and set to Open
set @Rundate = (Select  EOMONTH(a.RunDate,0) from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR')
if Not Exists (Select * from cor_closedperiod where Year([date]) = Year(@Rundate)  and month([date]) = month(@Rundate) and solutionid = @MPRID)
Begin
	Insert into cor_closedperiod(solutionid, date, status,active, deleted, createdby, createdon, updatedby, updatedon)
	values (@MPRID , @Rundate,1,1,0,'auto', getdate(),'auto',getdate())
End
Else
Begin
	Update cor_closedperiod
	set status = 1
	where Year(date) = Year(@Rundate)  and
	month(date) = month(@Rundate) and solutionid = @MPRID
End






