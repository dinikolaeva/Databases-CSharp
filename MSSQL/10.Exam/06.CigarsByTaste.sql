SELECT c.Id, 
	   CigarName,
	   PriceForSingleCigar, 
	   TasteType, 
	   TasteStrength
FROM Cigars AS c 
		JOIN Tastes AS t ON t.Id = c.TastId
	WHERE TasteType IN ('Earthy','Woody')
	ORDER BY PriceForSingleCigar DESC