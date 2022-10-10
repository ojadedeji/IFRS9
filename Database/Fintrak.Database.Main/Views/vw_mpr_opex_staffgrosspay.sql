Create VIEW [dbo].[vw_mpr_opex_staffgrosspay]
AS
SELECT     a.MISCode, SUM(Amount) AS Gross_Pay, COUNT(a.EmployeeCode) AS COUNT
FROM         dbo.mpr_staffcost a
WHERE     (MISCode IS NOT NULL)
GROUP BY MISCode