--16.Create SoftUni Database
CREATE DATABASE SoftUni

USE SoftUni

--Towns (Id, Name)
CREATE TABLE Towns
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL
)

--Addresses (Id, AddressText, TownId)

CREATE TABLE Addresses
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	AddressText NVARCHAR(100) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

--Departments (Id, Name)

CREATE TABLE Departments
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

--Employees (Id, FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, 
--Salary, AddressId)

CREATE TABLE Employees
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(20) NOT NULL,
	MiddleName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	JobTitle NVARCHAR(50) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATE NOT NULL,
	Salary DECIMAL (10,2) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)

--17.Backup Database

--18.Basic Insert

--Use the SoftUni database and insert some data using SQL queries.
--	Towns: Sofia, Plovdiv, Varna, Burgas
--	Departments: Engineering, Sales, Marketing, Software Development, Quality Assurance
--	Employees:
--   Name	                   Job Title	  Department	          Hire Date	    Salary
--Ivan Ivanov Ivanov	    .NET Developer	 Software Development	  01/02/2013	3500.00
--Petar Petrov Petrov	    Senior Engineer	 Engineering	          02/03/2004	4000.00
--Maria Petrova Ivanova	    Intern	         Quality  Assurance	      28/08/2016	525.25
--Georgi Teziev Ivanov	    CEO	         Sales	                              09/12/2007	3000.00
--Peter Pan Pan	Intern	        Marketing	                              28/08/2016	599.88

INSERT INTO Towns([Name]) VALUES
('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas')

INSERT INTO Departments([Name]) VALUES
('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, 
Salary) VALUES
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25),
('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00),
('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88)

--19.Basic Select All Fields

SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

--20.Basic Select All Fields and Order Them

SELECT * FROM Towns
ORDER BY [Name] ASC

SELECT * FROM Departments
ORDER BY [Name] ASC

SELECT * FROM Employees
ORDER BY Salary DESC

--21.Basic Select Some Fields

SELECT [Name] FROM Towns
ORDER BY [Name] ASC

SELECT [Name] FROM Departments
ORDER BY [Name] ASC

SELECT FirstName, LastName, JobTitle, Salary  FROM Employees
ORDER BY Salary DESC

--22. Increase Employees Salary

UPDATE Employees
SET Salary = Salary * 1.10

SELECT Salary FROM Employees

--23. Decrease Tax Rate

USE Hotel

UPDATE Payments
SET TaxRate = TaxRate * 0.97

SELECT TaxRate FROM Payments

TRUNCATE TABLE Occupancies

SELECT * FROM Occupancies