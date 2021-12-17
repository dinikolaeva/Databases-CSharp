CREATE DATABASE TableRelations
USE TableRelations

CREATE TABLE Persons
(
	PersonID INT PRIMARY KEY NOT NULL IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	Salary DECIMAL(10, 2) NOT NULL,
	PassportID INT NOT NULL
)

CREATE TABLE Passports 
(
	PassportID INT PRIMARY KEY NOT NULL IDENTITY,
	PassportNumber VARCHAR(8)
)

ALTER TABLE Persons
ADD FOREIGN KEY (PassportID) REFERENCES Passports(PassportID)