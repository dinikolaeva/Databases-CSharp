SELECT Username,
	   g.[Name] AS Game,
	   COUNT(i.Id) AS [Items Count],
	   SUM(i.Price) AS [Items Price]
	FROM Users AS u
		JOIN UsersGames AS ug ON ug.UserId = u.Id
		JOIN Games AS g ON ug.GameId = g.Id
		JOIN UserGameItems AS ugi ON ug.Id = ugi.UserGameId
		JOIN Items AS i ON ugi.ItemId = i.Id
	GROUP BY Username,
			 g.[Name]
		HAVING COUNT(i.Id) >= 10
	ORDER BY [Items Count] DESC,
			 [Items Price] DESC,
			 Username