

Create proc [dbo].[spp_mpr_team_autocreate]
(
	@Year int
)
as
--delete mpr_team_definition where Year = @Year;
--delete mpr_team where Year = @Year;
declare @latestYear int = (Select max(a.Year) from mpr_team_definition a where [year] < @Year); 

If Not Exists (Select * from mpr_team_definition where Year = @Year)
Begin
	Insert into mpr_team_definition(Code, Name, Position, Year, CanClassified, CompanyCode, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)

	SELECT        Code, Name, Position,@Year Year, CanClassified, CompanyCode, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn
	FROM            mpr_team_definition where year = @latestYear
	ORDER BY Position
End


If Not Exists (Select * from mpr_team where Year = @Year)
Begin
	Insert into mpr_team(Code, Name, ParentCode, DefinitionCode, CompanyCode, Year, Active, Deleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn)
	SELECT         a.Code, a.Name, ParentCode, DefinitionCode, a.CompanyCode, @Year Year, a.Active, a.Deleted, a.CreatedBy, a.CreatedOn, a.UpdatedBy, a.UpdatedOn
	FROM            mpr_team a inner join mpr_team_definition b on a.DefinitionCode = b.Code and a.Year = b.Year 
	where a.Year = @latestYear order by b.Position desc
End
