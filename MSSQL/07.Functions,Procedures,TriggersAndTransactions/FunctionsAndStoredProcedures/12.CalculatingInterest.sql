CREATE PROCEDURE usp_CalculateFutureValueForAccount(@accountId INT, @interestRate FLOAT = 0.1)
AS
BEGIN
	SELECT ah.Id AS [Account Id],
		   FirstName AS [First Name],
		   LastName AS [Last Name],
		   Balance AS [Current Balance],
		   (SELECT TOP(1) [dbo].[ufn_CalculateFutureValue](Balance, @interestRate, 5))
		   AS [Balance in 5 years]
	FROM AccountHolders AS ah
	JOIN Accounts AS a ON ah.Id = a.AccountHolderId
	WHERE a.Id = @accountId
END

EXEC usp_CalculateFutureValueForAccount  1, 0.1