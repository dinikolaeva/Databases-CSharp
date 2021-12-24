SELECT FirstName + ' ' + LastName AS FullName,
	   Country,
	   ZIP,
	   CONCAT('$', (MAX(ci.PriceForSingleCigar))) AS CigarPrice
	FROM Clients AS c
		JOIN Addresses AS a ON a.Id = c.AddressId
		JOIN ClientsCigars AS cc ON c.Id = cc.ClientId
		JOIN Cigars AS ci ON cc.CigarId = ci.Id
	WHERE ZIP NOT LIKE '%[^0-9]%'
	GROUP BY FirstName, LastName, Country, a.ZIP
	ORDER BY FullName