create procedure [dbo].[DeleteEmployee]
(
	@Id int
)
as 
begin
	delete from Employees where Id= @Id
end