SELECT FirstName + ' ' + LastName AS [Full Name],
	   pl.[Name] AS [Plane Name],
	   f.Origin + ' - ' + f.Destination AS Trip,
	   lt.[Type] AS [Luggage Type]
FROM Flights AS f
		JOIN Tickets AS t ON f.Id = t.FlightId
		JOIN Passengers AS p ON t.PassengerId = p.Id
		JOIN Planes AS pl ON f.PlaneId = pl.Id
		JOIN Luggages AS l ON t.LuggageId = l.Id
		JOIN LuggageTypes AS lt ON l.LuggageTypeId = lt.Id
	ORDER BY [Full Name] ASC,
			 [Plane Name] ASC,
			 Origin ASC,
			 Destination ASC,
			 [Luggage Type] ASC