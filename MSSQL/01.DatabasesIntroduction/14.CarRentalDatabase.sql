CREATE DATABASE CarRental
USE CarRental
--Categories (Id, CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)

CREATE TABLE Categories
(
	Id INT NOT NULL PRIMARY KEY IDENTITY, 
	CategoryName NVARCHAR(30) NOT NULL, 
	DailyRate DECIMAL (5,2), 
	WeeklyRate DECIMAL (5,2), 
	MonthlyRate DECIMAL (5,2), 
	WeekendRate DECIMAL (5,2) 
)

--Cars (Id, PlateNumber, Manufacturer, Model, CarYear, CategoryId, 
--Doors, Picture, Condition, Available)

CREATE TABLE Cars
(
	Id INT NOT NULL PRIMARY KEY IDENTITY, 
	PlateNumber NVARCHAR (8) NOT NULL, 
	Manufacturer NVARCHAR (20) NOT NULL, 
	Model NVARCHAR (20) NOT NULL,
	CarYear DATE NOT NULL, 
	CategoryId INT NOT NULL, 
    Doors INT NOT NULL, 
	Picture NVARCHAR(MAX) NOT NULL, 
	Condition NVARCHAR(20) NOT NULL, 
	Available BIT NOT NULL
)

ALTER TABLE Cars
ADD FOREIGN KEY (CategoryId) REFERENCES Categories(Id)

--Employees (Id, FirstName, LastName, Title, Notes)

CREATE TABLE Employees
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Title NVARCHAR(30),
	Notes NVARCHAR(100)
)

--Customers (Id, DriverLicenceNumber, FullName, Address, City, ZIPCode, Notes)

CREATE TABLE Customers
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	DriverLicenceNumber NVARCHAR(10) NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR (30),
	City NVARCHAR(50) NOT NULL,
	ZIPCode NVARCHAR(20) NOT NULL ,
	Notes NVARCHAR(MAX)
)

--RentalOrders (Id, EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, 
--KilometrageEnd, TotalKilometrage, StartDate, 
--EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)

CREATE TABLE RentalOrders
(
	Id INT NOT NULL PRIMARY KEY IDENTITY, 
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id), 
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id), 
	CarId INT FOREIGN KEY REFERENCES Cars(Id), 
	TankLevel INT NOT NULL, 
	KilometrageStart INT NOT NULL, 
	KilometrageEnd INT NOT NULL, 
	TotalKilometrage AS KilometrageEnd - KilometrageStart, 
	StartDate DATE NOT NULL, 
	EndDate DATE NOT NULL, 
	TotalDays AS DATEDIFF(DAY, StartDate, EndDate), 
	RateApplied DECIMAL (5,2) NOT NULL, 
	TaxRate DECIMAL (6,2) NOT NULL, 
	OrderStatus NVARCHAR (20) NOT NULL, 
	Notes NVARCHAR(MAX)
)

INSERT INTO Categories(CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
('Sports car', 90.5, 25.0, NULL, NULL),
('Station wagon', 80.5, 15.0, 25.25, 30.25),
('Hatchback', 60.5, 10.0, 11.50, 25.30)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, 
Doors, Picture, Condition, Available) VALUES
('KF85264E', 'Volkswagen Golf', 'Mk7', '2012', 3, 5, 'SOMEPICTUREADRESS', 'NEW', 1),
('RF25OK25', 'Ford Mustang', 'Shelby GT500', '2019', 1, 2, 'SOMEPICTUREADRESS', 'NEW', 0),
('HGFHGF75', 'Audi A4', 'Allroad', '2019', 3, 5, 'SOMEPICTUREADRESS', 'NEW', 1)

INSERT INTO Employees (FirstName, LastName, Title, Notes) VALUES
('Dimitar', 'Petrov', 'Director', 'Some notes...'),
('Strahil', 'Popov', 'Manager', NULL),
('Emiliya', 'Ivanova', 'accountant', NULL)

INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes) VALUES
('6546846510', 'Stamat Ivanov', 'Some address', 'Plovdiv', '54545', 'Some notes...'),
('1556993152', 'Viktoria Nikolova', 'Some address', 'Sofia', '40132', NULL),
('0025458115', 'Emil Pavlov', 'Some address', 'Stara Zagora', '45899', 'Some notes...')

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, 
KilometrageEnd, StartDate, EndDate, RateApplied, TaxRate, OrderStatus, Notes) VALUES
(3, 1, 1, 250, 10, 20, '2021-01-01', '2021-01-03', 25.20, 25.00, 'Complete', 'Some notes...'),
(1, 2, 3, 150, 50, 100, '2021-01-01', '2021-01-30', 50.50, 100.00, 'Complete', NULL),
(2, 3, 2, 200, 30, 250, '2021-01-10', '2021-01-25', 60.60, 250.00, 'Complete', 'Some notes...')