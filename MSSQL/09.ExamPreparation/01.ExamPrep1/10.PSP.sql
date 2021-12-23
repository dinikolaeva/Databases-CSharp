SELECT pl.[Name], 
	   pl.Seats, 
	   ISNULL(COUNT(t.PassengerId), 0) AS [Passengers Count]
FROM Planes AS pl
			LEFT JOIN Flights AS f ON f.PlaneId = pl.Id
			LEFT JOIN Tickets AS t ON f.Id = t.FlightId
		GROUP BY pl.[Name], 
				 pl.Seats
		ORDER BY [Passengers Count] DESC,
				 pl.[Name],  
				 pl.Seats