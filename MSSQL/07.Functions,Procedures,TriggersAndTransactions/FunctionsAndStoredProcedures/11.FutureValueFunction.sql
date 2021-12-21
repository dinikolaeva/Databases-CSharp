CREATE FUNCTION ufn_CalculateFutureValue(@sum DECIMAL(18, 4), @rate FLOAT, @years INT)
RETURNS DECIMAL(18, 4)
AS
BEGIN
	DECLARE @result DECIMAL(18, 4)
	SELECT @result = @sum * (POWER((1 + @rate), @years))
	RETURN @result
END

SELECT dbo.ufn_CalculateFutureValue (1000, 0.1, 5)