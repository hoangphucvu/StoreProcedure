create procedure [dbo].[UpdateEmployeeDetail]
(
	@Id int,
	@Name varchar(50),
	@City varchar(50),
	@Address varchar(50)
)
as 
begin
	update Employees
	set Name = @Name,
	City=@City,
	Address=@Address
	where Id=@Id
end