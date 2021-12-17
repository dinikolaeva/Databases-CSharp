CREATE TABLE Students 
(
	StudentID INT PRIMARY KEY NOT NULL IDENTITY,
	[Name] NVARCHAR (50) NOT NULL
)

INSERT INTO Students ([Name]) VALUES
('Mila'),
('Toni'),
('Ron')

CREATE TABLE Exams
(
	ExamID INT PRIMARY KEY NOT NULL IDENTITY(101, 1),
	[Name] NVARCHAR(50)
)

INSERT INTO Exams([Name]) VALUES
('SpringMVC'),
('Neo4j'),
('Oracle 11g')

CREATE TABLE StudentsExams
(
	StudentID INT NOT NULL FOREIGN KEY REFERENCES Students(StudentID),
	ExamID INT NOT NULL FOREIGN KEY REFERENCES Exams(ExamID),
	PRIMARY KEY (StudentID, ExamID)
)
INSERT INTO StudentsExams(StudentID, ExamID) VALUES
(1, 101),
(1, 102),
(2, 101),
(3, 103),
(2, 102),
(2, 103)