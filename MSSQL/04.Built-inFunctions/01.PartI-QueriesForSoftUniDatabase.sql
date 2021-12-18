--1.Find Names of All Employees by First Name

SELECT FirstName, LastName
FROM Employees
WHERE FirstName LIKE 'Sa%%'

--2.Find Names of All Employees by Last Name

SELECT FirstName, LastName
FROM Employees
WHERE LastName LIKE '%%ei%%'

--3.Find First Names of All Employess

SELECT FirstName FROM Employees
WHERE DepartmentID = 3 OR DepartmentID = 10
	AND HireDate BETWEEN '1995' AND '2005'

--4.Find All Employees Except Engineers

SELECT FirstName, LastName
FROM Employees
WHERE JobTitle NOT LIKE '%%engineer%%'

--5.Find Towns with Name Length

SELECT [Name] FROM Towns
WHERE LEN([Name]) = 5 OR LEN([Name]) = 6
ORDER BY [Name] ASC

--6.Find Towns Starting With

SELECT * FROM Towns
WHERE SUBSTRING([Name], 1, 1) = 'M'
OR SUBSTRING([Name], 1, 1) = 'K'
OR SUBSTRING([Name], 1, 1) = 'B'
OR SUBSTRING([Name], 1, 1) = 'E'
ORDER BY [Name] ASC

--7.Find Towns Not Starting With

SELECT * FROM Towns
WHERE [Name] NOT LIKE 'R%%'
AND [Name] NOT LIKE 'B%%'
AND [Name] NOT LIKE 'D%%'
ORDER BY [Name] ASC

--8.Create View Employees Hired After 2000 Year

CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName
FROM Employees
WHERE HireDate > '2001'

--9.Length of Last Name

SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5

--10.Rank Employees by Salary

SELECT EmployeeID, FirstName, LastName, Salary,
DENSE_RANK() OVER   
    (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000
ORDER BY Salary DESC

--11.Find All Employees with Rank 2

SELECT * FROM
(
SELECT EmployeeID, FirstName, LastName, Salary,
DENSE_RANK() OVER   
    (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000
) r
WHERE r.[Rank] = 2
ORDER BY Salary DESC