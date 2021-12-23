SELECT FirstName + ' ' + LastName AS [Full Name],
	   Origin,
	   Destination
FROM Flights AS f
		JOIN Tickets AS t ON f.Id = t.FlightId
		JOIN Passengers AS p ON t.PassengerId = p.Id
ORDER BY [Full Name] ASC,
		 Origin ASC,
		 Destination ASC