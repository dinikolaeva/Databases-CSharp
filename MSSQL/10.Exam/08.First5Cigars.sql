SELECT TOP(5) CigarName,
	   PriceForSingleCigar,
	   ImageURL
	FROM Cigars AS c
		JOIN Sizes AS s ON C.SizeId = S.Id
	WHERE s.[Length] >= 12 AND (CigarName LIKE '%ci%' OR
		  PriceForSingleCigar > 50) AND RingRange > 2.55
	ORDER BY CigarName ASC,
			 PriceForSingleCigar DESC