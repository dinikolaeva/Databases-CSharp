SELECT TOP(5) EmployeeId, JobTitle, e.AddressID AS AddressId, AddressText
FROM Employees AS e
	JOIN Addresses ON e.AddressID = Addresses.AddressID
ORDER BY e.AddressID ASC