CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@number DECIMAL(10, 2))
AS
BEGIN
	SELECT FirstName AS [First Name],
	       LastName AS [Last Name]
	FROM Accounts AS a
			JOIN AccountHolders AS ah ON a.AccountHolderId = ah.Id
		GROUP BY FirstName, LastName
		HAVING SUM(Balance) > @number
		ORDER BY FirstName ASC, 
				 LastName ASC
END

EXEC usp_GetHoldersWithBalanceHigherThan 50000