SELECT g.[Name] AS Game,
	   gt.[Name] AS [Game Type],
	   u.Username,
	   ug.[Level],
	   ug.Cash,
	   ch.[Name] AS [Character]
FROM Users AS u
		JOIN UsersGames AS ug ON ug.UserId = u.Id
		JOIN Games AS g ON ug.GameId = g.Id
		JOIN GameTypes AS gt ON g.GameTypeId = gt.Id
		JOIN Characters AS ch ON ug.CharacterId = ch.Id
	ORDER BY [Level] DESC,
			 Username, 
			 Game