
Create proc [dbo].[spp_mpr_balancesheet_transferprice_process]
as

Declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');
declare @loop int = 1,@looplevel int,@CountLevel int, @count int = 0, @Levelcode varchar(10), @Code varchar(20), @rate decimal(18,6) = 0,
 @product varchar(20), @caption varchar(100) ;



declare @NDY int = case when year(@rundate)  % 4 = 0 then 366 else 365 end;

exec spp_MPR_Team_Breakdown;  --break Down Teams into their Parents


 -- Do for Product and Account ratings
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'level_exception_temp') DROP TABLE level_exception_temp;
IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'account_transfer_temp') DROP TABLE account_transfer_temp;


--Get Account Officer levelcode for exception
select top 1 a.DefinitionCode, a.Code into level_exception_temp from mpr_team a where a.Year = Year(@rundate) and a.DefinitionCode in ('ACCT');

IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'team_definition_temp') DROP TABLE team_definition_temp
select ROW_NUMBER() OVER(order by Position desc) AS ID,Code, Position into team_definition_temp from
 mpr_team_definition a where a.Year = Year (@Rundate) 
and a.Active = 1 and a.Deleted = 0 and a.Code not in (select 'ACCT');


set @loop  = 1

Set @Count =  (Select count(*) from team_definition_temp);

While @Count >= @loop 
begin
	Select @Levelcode = a.Code from team_definition_temp a where a.ID = @loop;
	IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'level_MIS_loop') DROP TABLE level_MIS_loop;
	if Exists  (select * from dbo.vw_mpr_general_transfer_price a where year = Year(@rundate)
	and Period = Month(@rundate) and misDefinition = @Levelcode )
	Begin
		
		Select ROW_NUMBER() OVER(order by MisCode desc) AS ID ,g.MISCode,g.Rate,g.ProductCode, g.CaptionName  into level_MIS_loop from (
		select distinct a.MisCode, cast(0 as float) Rate, cast(null as varchar(50)) ProductCode, 
		cast(null as varchar(50)) CaptionName  	from dbo.vw_mpr_general_transfer_price a		
		 where misDefinition = @Levelcode and a.[Year] =  year(@Rundate)  
		and Period = month(@Rundate)) g;

		--level_loop
		set @looplevel = 1
		Set @CountLevel =  (Select count(*) from level_MIS_loop);
		While @CountLevel >= @looplevel
		begin
			select @Code = b.MisCode from level_MIS_loop b where ID = @looplevel;

			Update mpr_balancesheet
					set PoolRate = a.rate,
					Pool = AverageBal * (a.rate/100) * ( cast(day(@Rundate) as decimal(2,0))/ 
					case when CurrencyType = 'LCY' then @ndy else 360 end)
			from dbo.vw_mpr_general_transfer_price a
			where a.year = year(@rundate) and a.period = month(@rundate) and
				a.category = mpr_balancesheet.Category and a.Currency = mpr_balancesheet.CurrencyType
				and a.MISCode = @Code
				and TeamCode in (Select b.mis from distinct_team_mapping b where b.parentMIS = @Code );

			set @looplevel = @looplevel + 1
		End		
	End
	set @loop = @loop + 1
end


	




set @loop  = 1

Set @Count =  (Select count(*) from team_definition_temp);

While @Count >= @loop 
begin
	Select @Levelcode = a.Code from team_definition_temp a where a.ID = @loop;
	IF  EXISTS (SELECT * FROM sys.tables WHERE  name = N'level_MIS_loop') DROP TABLE level_MIS_loop;
	if Exists  (select * from mpr_transfer_price a inner join cor_solution c
	on a.SolutionId = c.SolutionId and c.alias = 'MPR' where DefinitionCode = @Levelcode )
	Begin
	
		Select ROW_NUMBER() OVER(order by MisCode desc) AS ID, a.MisCode, a.Rate, a.ProductCode, 
		b.CaptionName into		level_MIS_loop 	from mpr_transfer_price a
		inner join mpr_bs_caption b on a.CaptionCode = b.CaptionCode and a.CompanyCode = b.CompanyCode 
		inner join cor_solution c
		on a.SolutionId = c.SolutionId and c.alias = 'MPR'
		 where DefinitionCode = @Levelcode and a.[Year] =  year(@Rundate)  
		and Period = month(@Rundate)
		and a.Active = 1 and a.Deleted = 0 and b.Active = 1 and b.Deleted = 0;

		--level_loop
		set @looplevel = 1
		Set @CountLevel =  (Select count(*) from level_MIS_loop);
		While @CountLevel >= @looplevel
		begin
			select @Code = a.MisCode,@rate = a.Rate,@product = a.ProductCode,@caption = a.CaptionName from level_MIS_loop a where ID = @looplevel;

			Update mpr_balancesheet
		set PoolRate = @rate,
		Pool = AverageBal * (@rate/100) * ( cast(day(@Rundate) as decimal(2,0))/ case when CurrencyType = 'LCY' then @ndy else 360 end)
		
		where TeamCode in (Select b.mis from distinct_team_mapping b where b.parentMIS = @Code )
		and CaptionName = @caption and ProductCode = @product;

			set @looplevel = @looplevel + 1
		End		
	End
	set @loop = @loop + 1
end

-- Account transfer pricing
SELECT        AccountNo,b.name Category, Rate, Year, Period, c.Name solution into account_transfer_temp
FROM            mpr_account_transfer_price a inner join cor_accounttype b on a.Category = b.id inner join cor_solution c
on a.SolutionId = c.SolutionId where  a.[Year] =  year(@Rundate)  and Period = month(@Rundate) and a.Active = 1 and a.Deleted = 0
and c.Active = 1 and c.Deleted = 0 and c.Alias = 'MPR';

declare @CountAccount int = (Select count(*) from account_transfer_temp);

if @CountAccount > 0
Begin
	Update mpr_balancesheet
	set PoolRate = a.Rate
	from account_transfer_temp a where a.AccountNo = mpr_balancesheet.AccountNo
	and a.Category = mpr_balancesheet.Category;
End




	