UPDATE Tickets
	SET Price *= 1.13
	WHERE FlightId = 
		(SELECT Flights.Id
			FROM Flights
			WHERE Flights.Destination = 'Carlsbad')