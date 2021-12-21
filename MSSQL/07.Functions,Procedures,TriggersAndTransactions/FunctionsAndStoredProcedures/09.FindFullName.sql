CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
	SELECT (ah.FirstName + ' ' + ah.LastName) AS [Full Name] FROM Accounts AS a
	JOIN AccountHolders AS ah ON a.AccountHolderId = ah.Id	
	GROUP BY ah.FirstName, ah.LastName, AccountHolderId
END

EXEC usp_GetHoldersFullName