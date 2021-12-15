CREATE TABLE Users
(
	Id BIGINT NOT NULL PRIMARY KEY IDENTITY,
	Username VARCHAR (30) NOT NULL,
	[Password] VARCHAR (26) NOT NULL,
	ProfilePicture VARCHAR (MAX),
	LastLoginTime DATETIME,
	IsDeleted BIT
)

INSERT INTO Users(Username,[Password],ProfilePicture,LastLoginTime,IsDeleted) VALUES
('Didi', '123456', 'somePictureAdress', 2021-09-28, 0),
('Niki', 'fffff', 'somePictureAdress', 2020-10-08, 1),
('Pipi', 'fkjkjf', 'somePictureAdress', 2021-01-20, 0),
('Valio', 'dfsdf456', 'somePictureAdress', 2021-05-16, 1),
('Pesho', 'ffrrffrr', 'somePictureAdress', 2021-07-30, 0)

SELECT * FROM Users

---9.Change Primary Key

--remove current primary key:
ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC07E79353A3

--create new primary key that would be 
--the combination of fields Id and Username:

ALTER TABLE Users
ADD CONSTRAINT PK_IdUsername PRIMARY KEY(Id, Username)

--10.Add Check Constraint

ALTER TABLE Users
ADD CONSTRAINT CH_PasswordLength CHECK (LEN([Password])>5)

--11.Set Default Value of a Field

ALTER TABLE Users
ADD CONSTRAINT DF_LastLoginTime DEFAULT GETDATE() FOR LastLoginTime

--12.Set Unique Field

--remove Username field from the primary key 
ALTER TABLE Users
DROP CONSTRAINT PK_IdUsername

--only the field Id would be primary key
ALTER TABLE Users
ADD CONSTRAINT PK_Id PRIMARY KEY (Id)

--add unique constraint to the Username field to 
--ensure that the values there are at least 3 symbols long

ALTER TABLE Users
ADD CONSTRAINT CH_Username CHECK (LEN(Username)>3)
