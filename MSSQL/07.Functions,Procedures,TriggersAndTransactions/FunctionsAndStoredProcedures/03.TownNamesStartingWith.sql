CREATE PROCEDURE usp_GetTownsStartingWith  @town NVARCHAR(30)
AS
	SELECT [Name] AS Town FROM Towns
	WHERE [Name] LIKE @town + '%'

GO

EXEC usp_GetTownsStartingWith 'B'