SELECT c.Id, 
	   c.FirstName + ' ' + c.LastName AS ClientName,
	   c.Email
	FROM Clients AS c
		LEFT JOIN ClientsCigars AS cc ON C.Id = CC.ClientId
		LEFT JOIN Cigars AS ci ON CI.Id = CC.CigarId
	WHERE CigarId IS NULL
	ORDER BY ClientName ASC