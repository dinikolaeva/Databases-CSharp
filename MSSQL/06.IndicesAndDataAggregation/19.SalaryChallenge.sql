SELECT TOP(10) FirstName,
	   LastName,
	   e.DepartmentID
FROM Employees AS e
	JOIN (SELECT DepartmentID, 
				 AVG(Salary) AS avSal 
		  FROM Employees 
			GROUP BY DepartmentID) AS avS ON e.DepartmentID = avS.DepartmentID
	WHERE e.Salary > avS.avSal
	ORDER BY e.DepartmentID