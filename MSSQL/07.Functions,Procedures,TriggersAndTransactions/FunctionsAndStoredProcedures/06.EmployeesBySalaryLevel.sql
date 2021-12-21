CREATE PROCEDURE usp_EmployeesBySalaryLevel (@salaryLevel VARCHAR(30))
AS
	SELECT FirstName AS [First Name],
		   LastName AS [Last Name]
	FROM Employees
	WHERE @salaryLevel = [dbo].[ufn_GetSalaryLevel](Salary)
GO
EXEC usp_EmployeesBySalaryLevel 'High'