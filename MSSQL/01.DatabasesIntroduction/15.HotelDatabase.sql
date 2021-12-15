CREATE DATABASE Hotel
USE Hotel

--Employees (Id, FirstName, LastName, Title, Notes)
CREATE TABLE Employees
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName NVARCHAR (20) NOT NULL,
	LastName NVARCHAR (20) NOT NULL,
	Title NVARCHAR (20) NOT NULL,
	Notes NVARCHAR (MAX)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes) VALUES
('Pesho', 'Ivanov', 'Manager', NULL),
('Ema', 'Stoyanova', 'Accountant', 'some notes...'),
('Vasil', 'Petrov', 'Piccolo', 'some notes...')

--Customers (AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, 
--EmergencyNumber, Notes)

CREATE TABLE Customers
(
	AccountNumber INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName NVARCHAR (20) NOT NULL,
	LastName NVARCHAR (20) NOT NULL,
	PhoneNumber NVARCHAR (10) NOT NULL,
	EmergencyName NVARCHAR (50) NOT NULL,
	EmergencyNumber NVARCHAR (10) NOT NULL,
	Notes NVARCHAR (MAX)
)

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyName, 
EmergencyNumber, Notes) VALUES

('Stamat', 'Pavlov', '0886665871', 'some name', '0785555222', NULL),
('Evgeni', 'Ivanov', '0846675461', 'some name', '5484651546', NULL),
('Ana', 'Sokolova', '0886545646', 'some name', '0584161659', NULL)

--RoomStatus (RoomStatus, Notes)
CREATE TABLE RoomStatus
(
	RoomStatus NVARCHAR(20) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO RoomStatus (RoomStatus, Notes) VALUES 
('Not clean', NULL),
('Clean', NULL),
('For cleaning', NULL)

--RoomTypes (RoomType, Notes)

CREATE TABLE RoomTypes
(
	RoomType NVARCHAR(20) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO RoomTypes (RoomType, Notes) VALUES
('Apartment', NULL),
('Double deluxe', NULL),
('Studio', NULL)

--BedTypes (BedType, Notes)

CREATE TABLE BedTypes
(
	BedType NVARCHAR(20) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO BedTypes (BedType, Notes) VALUES
('Single', NULL),
('Double', NULL),
('Triple', NULL)

--Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes)

CREATE TABLE Rooms
(
	RoomNumber INT NOT NULL PRIMARY KEY IDENTITY,
	RoomType NVARCHAR(20) FOREIGN KEY REFERENCES RoomTypes(RoomType),
	BedType NVARCHAR(20) FOREIGN KEY REFERENCES BedTypes(BedType),
	Rate DECIMAL(5,2) NOT NULL,
	RoomStatus NVARCHAR(20) FOREIGN KEY REFERENCES RoomStatus(RoomStatus),
	Notes NVARCHAR(MAX)
)

INSERT INTO Rooms (RoomType, BedType, Rate, Notes) VALUES
('Apartment', 'Triple', 25.50, NULL),
('Double deluxe', 'Double', 50, NULL),
('Studio', 'Single', 15.25, NULL)

--Payments (Id, EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, 
--TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal, Notes)

CREATE TABLE Payments
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	PaymentDate DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
	AmountCharged DECIMAL (10, 2) NOT NULL,
	TaxRate DECIMAL (5, 2) NOT NULL,
	TaxAmount AS AmountCharged * TaxRate,
	PaymentTotal DECIMAL (10, 2) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, 
LastDateOccupied, AmountCharged, TaxRate, PaymentTotal, Notes) VALUES
(3, '2021-01-02', 2, '2021-01-01', '2021-01-05', 100, 20, 120, NULL),
(2, '2021-05-22', 1, '2021-03-20', '2021-03-25', 250, 20, 1250, NULL),
(1, '2021-03-20', 3, '2021-05-01', '2021-05-10', 50, 20, 450, NULL)

--Occupancies (Id, EmployeeId, DateOccupied, AccountNumber, RoomNumber, 
--RateApplied, PhoneCharge, Notes)

CREATE TABLE Occupancies
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	DateOccupied DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber),
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber),
	RateApplied DECIMAL (5,2),
	PhoneCharge BIT NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber, 
RateApplied, PhoneCharge, Notes) VALUES
(2, '2021-01-05', 2, 3, 5.25, 1, NULL),
(3, '2021-05-24', 1, 2, 3.20, 0, NULL),
(1, '2021-03-15', 3, 1, 6.00, 1, NULL)