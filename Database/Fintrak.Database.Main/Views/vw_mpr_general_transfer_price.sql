Create View vw_mpr_general_transfer_price
as


select a.MISCode,a.DefinitionCode MISDefinition, c.name Category, b.Name Currency, a.Rate, a.Period, a.year from mpr_general_transfer_price a inner join cor_currencytype b
on a.currencytype= b.id  inner join cor_accounttype c
on a.category = c.id