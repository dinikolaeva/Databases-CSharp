CREATE FUNCTION udf_ClientWithCigars(@name NVARCHAR(50))
RETURNS INT
AS
BEGIN
RETURN (SELECT COUNT(*) FROM Cigars AS c 
				JOIN ClientsCigars AS cc ON c.Id = cc.CigarId
				JOIN Clients AS cl ON cc.ClientId = cl.Id
			WHERE cl.FirstName = @name)
END

SELECT dbo.udf_ClientWithCigars('Betty')