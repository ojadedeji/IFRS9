/* Generates breakdown of MISs to their team components */

Create function [dbo].[fn_cor_Team_Breakdown]
(
	@Year int
)
RETURNS   @Output Table  
(
	MIS Varchar(50),
	ParentMIS Varchar(50)
)
as

begin
	Declare @parentmis TABLE 
			(
				row int, 
				Code Varchar(50), 
				name Varchar(200)
			)

	Declare @parent_mapping_temp TABLE 
			(
				MIS Varchar(50), 
				parentMIS Varchar(50)
			)

	Declare @distinct_team_mapping TABLE 
			(
				MIS Varchar(50), 
				parentMIS Varchar(50)
			)

	Declare @team_mapping TABLE 
			(
				MIS Varchar(50), 
				parentMIS Varchar(50),
				TeamDefinitionCode Varchar(50),
				Position int
			)

	declare @min int = 1;
	declare @max int = 0;
	declare @caption varchar(100);

		--Get Parent MIS
	Insert into @parentmis(row,Code,name)
	select row_number() over (order by v.Position asc ) as row, v.Code,v.name   from 
	(select distinct a.Code, a.Name, b.Position    from mpr_team a inner join mpr_team_definition b on a.DefinitionCode = b.Code 
	and a.Year = b.Year  where a.Year = @Year and a.Active = 1 and a.Deleted = 0 and b.Active = 1 and b.Deleted = 0 
	and a.DefinitionCode not in (select 'ACCT' union select 'TEM')
	and a.Code in 
	(select c.ParentCode from mpr_team c where c.Year = @Year and c.Active = 1 and c.Deleted = 0 )) v;

	set  @max = ( select count(*) from @parentmis)

	While @max >= @min
	Begin
		select @caption = code from @parentmis where row = @min

		insert into @team_mapping(mis, parentMIS  )
		select distinct a.code , ParentCode   from mpr_team a 
		where ParentCode = @caption and a.Year = @Year 
		and a.Active = 1 and a.Deleted = 0 ;
	
		insert into @team_mapping(mis, parentMIS)
		SELECT  distinct a.MIS, b.ParentCode
		FROM      @team_mapping a inner join mpr_team b on a.parentMIS = b.Code 
		where b.ParentCode = @caption and b.Year = @Year and b.Active = 1 and b.Deleted = 0 ;

		set @min = @min + 1
	End;

	--Break Parent MIS that also form components of MISs
	insert into @parent_mapping_temp(parentMIS,MIS ) 
	select parentMIS,MIS  from @team_mapping where MIS in (select c.parentMIS from @team_mapping c);
	Delete from @team_mapping where mis in (select c.MIS from @parent_mapping_temp c);

	Insert into @team_mapping(MIS,parentMIS )
	select b.MIS,a.parentMIS    from @parent_mapping_temp a left outer join @team_mapping b
	on a.MIS = b.parentMIS ;

	Insert into @team_mapping(MIS,parentMIS)
	select distinct b.MIS,b.MIS   from @parent_mapping_temp a left outer join @team_mapping b
	on a.MIS = b.parentMIS ;

	Insert into @Output(mis,ParentMIS )
	select distinct a.MIS, a.parentMIS from @team_mapping a

return 

end