
Create view vw_cor_solutionrundate
as
Select b.SolutionId, b.Name,b.Alias, a.RunDate from cor_solutionrundate a inner join cor_solution b on a.SolutionId = b.SolutionId
where a.Active = 1 and b.Active = 1 and a.Deleted = 0 and b.Deleted = 0



