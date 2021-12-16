CREATE DATABASE People

CREATE TABLE People(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	[Picture] VARBINARY (MAX),
	[Height] DECIMAL (10,2),
	[Weight] DECIMAL (10,2),
	[Gender] CHAR (1) NOT NULL,
	[Birthdate] DATE NOT NULL,
	[Biography] NVARCHAR (MAX)
)

INSERT INTO People.dbo.People ([Name],[Picture],
[Height],[Weight],[Gender],[Birthdate],[Biography])
VALUES
('Didi', NULL, 1.78, 60, 'f', '1990-10-19','Some biography'),
('Stamat', NULL, 1.90, 95, 'm', '1978-03-20',NULL),
('Vergil', NULL, 1.85, 80, 'm', '1988-05-09','...'),
('Viktoria', NULL, 1.65, 50, 'f', '1995-06-15',NULL),
('Hasan', NULL, 1.70, 80, 'm', '1985-02-14','Some biography')

DROP TABLE People

CREATE TABLE People(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(200) NOT NULL,
	[Picture] VARBINARY (MAX),
	[Height] DECIMAL (10,2),
	[Weight] DECIMAL (10,2),
	[Gender] CHAR (1) NOT NULL,
	[Birthdate] DATE NOT NULL,
	[Biography] NVARCHAR (MAX)
)

INSERT INTO People ([Name],[Picture],
[Height],[Weight],[Gender],[Birthdate],[Biography])
VALUES
('Didi', NULL, 1.78, 60, 'f', '1990-10-19','Some biography'),
('Stamat', NULL, 1.90, 95, 'm', '1978-03-20',NULL),
('Vergil', NULL, 1.85, 80, 'm', '1988-05-09','...'),
('Viktoria', NULL, 1.65, 50, 'f', '1995-06-15',NULL),
('Hasan', NULL, 1.70, 80, 'm', '1985-02-14','Some biography')

SELECT * FROM People