Create proc [dbo].[spp_mpr_revenue_invalid_mis_fix]
as
declare @EXCOMIS varchar(20) = (Select a.ExcoTeamCode from mpr_setup a)
Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR')
Declare @Year varchar(10) = (Select a.[Year] from dbo.mpr_setup a )



--Fix Blank MIS
Update mpr_revenue
Set TeamCode = @EXCOMIS
where TeamCode is null;

--Fix Invalid MIS
Update mpr_revenue
Set TeamCode = @EXCOMIS
where TeamCode not in (Select a.Code from mpr_team a where a.[Year] = @Year );

--Fix AOCode of EXCO MIS
Update mpr_revenue
Set AccountOfficerCode = (Select top 1 a.Code from mpr_team a where a.[Year] = @Year and a.ParentCode = @EXCOMIS)
where TeamCode = @EXCOMIS;


