CREATE DATABASE University
USE University

CREATE TABLE Subjects
(
	SubjectID INT PRIMARY KEY IDENTITY NOT NULL,
	SubjectName NVARCHAR(50) NOT NULL
)

CREATE TABLE Students
(
	StudentID INT PRIMARY KEY IDENTITY NOT NULL,
	StudentNumber VARCHAR(10) NOT NULL,
	StudentName NVARCHAR(50),
	MajorID INT NOT NULL
)

CREATE TABLE Agenda
(
	StudentID INT REFERENCES Subjects(SubjectID),
	SubjectID INT REFERENCES Students(StudentID),
	PRIMARY KEY (StudentID, SubjectID)
)

CREATE TABLE Majors
(
	MajorID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR (50) NOT NULL
)

ALTER TABLE Students
ADD FOREIGN KEY (MajorID) REFERENCES Majors(MajorID)

CREATE TABLE Payments
(
	PaymentID INT PRIMARY KEY IDENTITY NOT NULL,
	PaymentDate DATE NOT NULL,
	PaymentAmount DECIMAL (10, 2) NOT NULL,
	StudentID INT REFERENCES Students(StudentID)
)