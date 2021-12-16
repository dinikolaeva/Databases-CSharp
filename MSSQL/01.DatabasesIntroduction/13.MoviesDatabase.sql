CREATE DATABASE Movies

CREATE TABLE Directors
(
	--•	Directors (Id, DirectorName, Notes)
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR (50) NOT NULL,
	Notes NVARCHAR (MAX)
)

CREATE TABLE Genres
(
	--•	Genres (Id, GenreName, Notes)
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	GenreName NVARCHAR (50) NOT NULL,
	Notes NVARCHAR (MAX)
)

CREATE TABLE Categories
(
	--•	Categories (Id, CategoryName, Notes)
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR (50) NOT NULL,
	Notes NVARCHAR (MAX)
)

CREATE TABLE Movies
(
	--•	Movies (Id, Title, DirectorId, 
	--CopyrightYear, Length, GenreId, CategoryId, Rating, Notes)
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Title NVARCHAR (100) NOT NULL,
	DirectorId INT NOT NULL,
	CopyrightYear DATE,
	[Length] SMALLDATETIME,
	GenreId INT NOT NULL,
	CategoryId INT NOT NULL,
	Rating DECIMAL (4,2),
	Notes NVARCHAR (MAX)
)

ALTER TABLE Movies
ADD FOREIGN KEY (DirectorId) REFERENCES Directors(Id)

ALTER TABLE Movies
ADD FOREIGN KEY (GenreId) REFERENCES Genres(Id)

ALTER TABLE Movies
ADD FOREIGN KEY (CategoryId) REFERENCES Categories(Id)

INSERT INTO Directors(DirectorName, Notes) VALUES
	('Peter Jackson','He is best known as the director, writer, and producer 
	of the Lord of the Rings trilogy (2001–03) and 
	the Hobbit trilogy (2012–14)'),
	('George Lucas', 'Lucas is considered one of the most significant 
	figures of the 20th-century New Hollywood movement, 
	and a pioneer of the modern blockbuster.'),
	('Martin Scorsese','He is widely regarded as one of the greatest and 
	most influential directors in film history'),
	('James Cameron','Best known for making science fiction and epic films, 
	he first gained recognition for directing The Terminator (1984). '),
	('Quentin Tarantino',' American film director, 
	screenwriter, producer, author, film critic, and actor')

INSERT INTO Genres(GenreName, Notes) VALUES

	('action', NULL),
	('high fantasy', NULL),
	('fantasy', NULL),
	('western', NULL),
	('crime', NULL)

INSERT INTO Categories(CategoryName, Notes) VALUES 
	('A', 'First rating of votes'),
	('B', 'Second rating of votes'),
	('C', 'Third rating of votes'),
	('D', 'Fourth rating of votes'),
	('F', 'Fifth rating of votes')

INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length],
GenreId, CategoryId, Rating, Notes) VALUES

	('The Lord of the Rings: The Fellowship of the Ring',
	1, '2001-12-19', 180, 2, 1, 99.99, 'The Lord of the Rings is a series of three epic 
	fantasy adventure films directed by Peter Jackson,
	based on the novel written by J. R. R. Tolkien.'),
	('Star Wars', 2, '1977-05-25', 133, 3, 2, 99.90, 'Star Wars is an American epic space opera
	multimedia franchise created by George Lucas'),
	('The Terminator', 4, '1984-10-26', 107, 1, 3, 98.90, 'The Terminator is a 1984 science 
	fiction action film directed by James Cameron.'),
	('The Irishman', 3, '2019-09-27', 209, 5, 4, 97.00, 'The Irishman is a 2019 American epic crime film 
	directed and produced by Martin Scorsese and written by Steven Zaillian'),
	('Django Unchained', 5, '2012-12-11', 165, 4, 5, 96.50, 'Development of Django Unchained began in 
	2007 when Tarantino was writing a book on Corbucci.')