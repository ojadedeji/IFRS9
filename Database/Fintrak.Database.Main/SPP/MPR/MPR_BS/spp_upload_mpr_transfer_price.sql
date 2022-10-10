Create proc spp_upload_mpr_transfer_price
(   @Product varchar(50), 
	@Caption varchar(200), 
	@MIS varchar(50), 
	@Rate decimal(18,2), 
	@CompanyCode varchar(50),
	@Solution varchar(20),
	@Year int, 
	@Period int,
	@username varchar(50)
)

as
declare @Captioncode varchar(20),@SolutionId int, @TeamDef varchar(20)
declare @Rundate date = (Select a.RunDate from dbo.vw_cor_solutionrundate a where a.Alias = 'MPR');


Set @TeamDef = (Select b.DefinitionCode from mpr_team b where b.Code = @MIS and b.[Year] = year(@rundate))

Set @Captioncode = (Select b.CaptionCode from mpr_bs_caption b where b.CaptionName = @Caption)
Set @SolutionId = (Select b.SolutionId from cor_solution b where b.alias = @Solution)


If @CompanyCode is null or @CompanyCode = ''
Begin
	Set @CompanyCode = (Select top 1 code from cor_company order by CompanyId  )
End

If Not Exists (Select * from mpr_transfer_price b where b.Year = @Year and b.Period = @Period 
and b.ProductCode = @Product and b.CaptionCode = @Captioncode and CompanyCode = @CompanyCode and SolutionId = @SolutionId 
and MisCode = @MIS )
Begin
	Insert into mpr_transfer_price(ProductCode, CaptionCode, Rate, Year, Period, DefinitionCode, MisCode, CompanyCode, SolutionId, CreatedOn, CreatedBy)
	Select @Product, @Captioncode,@Rate,@Year,@Period,@TeamDef,@MIS,@CompanyCode,@SolutionId, GETDATE(), @username
End
Else
Begin
	Update mpr_transfer_price
	set Rate = @Rate,
	UpdatedOn = GETDATE(),
	UpdatedBy = @username,
	DefinitionCode = @TeamDef
	where [Year] = @Year and Period = @Period 
	and ProductCode = @Product and CaptionCode = @Captioncode and CompanyCode = @CompanyCode and SolutionId = @SolutionId 
	and MisCode = @MIS
End

