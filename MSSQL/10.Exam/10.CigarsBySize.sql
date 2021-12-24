SELECT LastName, 
	   AVG([Length]) AS CiagrLength, 
	   CEILING(AVG(RingRange)) AS CiagrRingRange
	FROM Cigars AS c 
		JOIN ClientsCigars AS cc ON c.Id = cc.CigarId
		JOIN Clients AS cl ON cc.ClientId = cl.Id
		JOIN Sizes AS s ON C.SizeId = s.Id
	GROUP BY LastName
	ORDER BY CiagrLength DESC