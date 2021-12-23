CREATE FUNCTION udf_CalculateTickets(
@origin NVARCHAR(50), @destination NVARCHAR(50), @peopleCount INT)
RETURNS NVARCHAR(50)
AS
BEGIN
	IF @peopleCount <=0
		RETURN 'Invalid people count!'
	
	DECLARE @isNull INT = (SELECT Id 
							   FROM Flights 
							   WHERE Origin = @origin AND Destination = @destination)
	IF (@isNull IS NULL)
		RETURN 'Invalid flight!'

	DECLARE @ticketPrice DECIMAL(10, 2) = (SELECT Price 
							FROM Tickets
							WHERE FlightId = @isNull)
	DECLARE @totalPrice DECIMAL (10, 2) = @ticketPrice * @peopleCount

	RETURN 'Total price ' + CAST(@totalPrice AS NVARCHAR(50))
END

SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', 33);
SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', -1);
SELECT dbo.udf_CalculateTickets('Invalid','Rancabolang', 33)