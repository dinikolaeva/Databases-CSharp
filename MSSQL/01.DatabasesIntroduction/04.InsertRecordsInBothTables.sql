--1.Create Database

CREATE DATABASE Minions

--2.Create Tables
CREATE TABLE Minions
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50),
	[Age] INT NOT NULL
)

CREATE TABLE Towns
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(50)
)

--3.Alter Minions Table

ALTER TABLE Towns 
ALTER COLUMN Name NVARCHAR(50) NOT NULL;

ALTER TABLE Minions
ADD TownId INT NOT NULL

ALTER TABLE Minions
ADD FOREIGN KEY (TownId) REFERENCES Towns(Id)

ALTER TABLE Minions
ALTER COLUMN Age INT NULL

--4.Insert Records in Both Tables
INSERT INTO Towns ([Id], [Name]) VALUES
	(1, 'Sofia'),
	(2, 'Plovdiv'),
	(3, 'Varna')

INSERT INTO Minions ([Id], [Name], [Age], [TownId]) VALUES
	(1, 'Kevin', 22, 1),
	(2, 'Bob', 15, 3),
	(3, 'Steward', NULL, 3)

UPDATE Minions 
SET 
    [TownId] = 2
WHERE
    [Id] = 3

--5.Truncate Table Minions

TRUNCATE TABLE Minions

--6.Drop All Tables

DROP TABLE Minions

DROP TABLE Towns