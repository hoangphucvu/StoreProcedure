create procedure [dbo].[AddNewEmpDetails]
	@Name varchar(50),
	@City varchar(50),
	@Address varchar(50)
as
begin
	insert into Employees values(@Name,@City,@Address)
end
