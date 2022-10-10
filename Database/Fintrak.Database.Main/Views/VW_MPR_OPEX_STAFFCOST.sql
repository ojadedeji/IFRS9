CREATE VIEW [dbo].[VW_MPR_OPEX_STAFFCOST]
AS
SELECT     distinct a.Gross_Pay, a.MISCode, b.[TYPE]
FROM         dbo.vw_mpr_opex_staffgrosspay a INNER JOIN
                      dbo.vw_mpr_opex_sbutype b ON a.MISCode = b.code