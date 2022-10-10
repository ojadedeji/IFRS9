Create proc [dbo].[spp_cor_Team_Breakdown]
(
	@Year int
)

as
--Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');

IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'parentmis') DROP TABLE parentmis
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'parent_mapping_temp') DROP TABLE parent_mapping_temp
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'distinct_team_mapping') DROP TABLE distinct_team_mapping
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'team_mapping') DROP TABLE team_mapping
Select top 0 cast(null as varchar(20)) MIS, cast(null as varchar(20)) parentMIS  into team_mapping;

declare @min int = 1;
declare @max int = 0;
declare @caption varchar(100);

--Get Parent Captions
select row_number() over (order by v.Position asc ) as row, v.Code,v.name into parentmis   from 
(select distinct a.Code, a.Name, b.Position    from mpr_team a inner join mpr_team_definition b on a.DefinitionCode = b.Code 
and a.Year = b.Year  where a.Year = @Year and a.Active = 1 and a.Deleted = 0 and b.Active = 1 and b.Deleted = 0 
and a.DefinitionCode not in (select 'ACCT' union select 'TEM')
and a.Code in 
(select c.ParentCode from mpr_team c where c.Year = @Year and c.Active = 1 and c.Deleted = 0 )) v;

set  @max = ( select count(*) from parentmis)

While @max >= @min
Begin
	select @caption = code from parentmis where row = @min

	insert into team_mapping(mis, parentMIS)
	select code , ParentCode  from mpr_team a where ParentCode = @caption and a.Year = @Year and a.Active = 1 and a.Deleted = 0 ;
	
	insert into team_mapping(mis, parentMIS)
	SELECT   a.MIS, b.ParentCode
FROM      team_mapping a inner join mpr_team b on a.parentMIS = b.Code 
where b.ParentCode = @caption and b.Year = @Year and b.Active = 1 and b.Deleted = 0 ;

	set @min = @min + 1
End;

--Break Parent MIS that also form components of other total lines
select * into parent_mapping_temp from team_mapping where MIS in (select c.parentMIS from team_mapping c);
Delete from team_mapping where mis in (select c.MIS from parent_mapping_temp c);

Insert into team_mapping(MIS,parentMIS )
select b.MIS,a.parentMIS    from parent_mapping_temp a left outer join team_mapping b
on a.MIS = b.parentMIS ;

Insert into team_mapping(MIS,parentMIS )
select distinct b.MIS,b.MIS s   from parent_mapping_temp a left outer join team_mapping b
on a.MIS = b.parentMIS ;


Select distinct * into distinct_team_mapping from team_mapping;


