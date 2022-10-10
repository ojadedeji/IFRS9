Create proc spp_upload_mpr_account_transfer_price
(   @AccountNo varchar(50), 
	@Category varchar(50),  
	@Rate decimal(18,2), 
	@Solution varchar(20),
	@Year int, 
	@Period int,
	@username varchar(50)
)

as
declare @CategoryId int,@SolutionId int

Set @CategoryId = (Select b.id from cor_accounttype b where b.name = @Category)
Set @SolutionId = (Select b.SolutionId from cor_solution b where b.alias = @Solution)


If Not Exists (Select * from mpr_account_transfer_price b where b.Year = @Year and b.Period = @Period 
and b.AccountNo = @AccountNo and b.Category = @CategoryId)
Begin
	Insert into mpr_account_transfer_price(AccountNo, Category, Rate, Year, Period, SolutionId, CreatedOn, CreatedBy)
	Select @AccountNo, @CategoryId,@Rate,@Year,@Period,@SolutionId, GETDATE(), @username
End
Else
Begin
	Update mpr_account_transfer_price
	set Rate = @Rate,
	UpdatedOn = GETDATE(),
	UpdatedBy = @username
	where [Year] = @Year and Period = @Period 
	and AccountNo = @AccountNo and Category = @CategoryId
End


