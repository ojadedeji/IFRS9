Create proc spp_mpr_balancesheet_invalid_mis_fix
as
declare @EXCOMIS varchar(20) = (Select a.ExcoTeamCode from mpr_setup a)
Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR')

IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'mpr_team_structure') DROP TABLE mpr_team_structure;

Select b.Code MIS, b.ParentCode, d.Code ParentMIS into mpr_team_structure from mpr_team b left outer join mpr_team d on 
b.ParentCode = d.Code
and d.Active = 1 and d.Deleted = 0 and b.Year = d.Year 
where  b.Active = 1 and b.Deleted = 0 and b.Year = Year(@Rundate);

--Fix Blank MIS
Update mpr_balancesheet
Set TeamCode = @EXCOMIS
where TeamCode is null;

--Fix Invalid MIS
Update mpr_balancesheet
Set TeamCode = @EXCOMIS
where TeamCode not in (Select a.MIS from mpr_team_structure a);

--Fix AOCode of EXCO MIS
Update mpr_balancesheet
Set AccountOfficerCode = (Select top 1 a.MIS from mpr_team_structure a where a.ParentMIS = @EXCOMIS)
where TeamCode = @EXCOMIS;



