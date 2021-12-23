SELECT FirstName, 
	   LastName,
	   Age
FROM Flights AS f
			RIGHT JOIN Tickets AS t ON f.Id = t.FlightId
			RIGHT JOIN Passengers AS p ON t.PassengerId = p.Id
		WHERE f.Id IS NULL
		ORDER BY Age DESC,
				 FirstName ASC,
				 LastName ASC
