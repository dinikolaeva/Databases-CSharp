CREATE PROCEDURE usp_SearchByTaste(@taste VARCHAR(20))
AS
SELECT CigarName,
	   CONCAT('$', PriceForSingleCigar) AS Price,
	   TasteType,
	   BrandName,
	   CONCAT(s.[Length], ' cm') AS CigarLength,
	   CONCAT(s.RingRange, ' cm') AS CigarRingRange
	FROM Cigars AS c
		JOIN Brands AS b ON b.Id = c.BrandId
		JOIN Tastes AS t ON c.TastId = t.Id
		JOIN Sizes AS s ON c.SizeId = S.Id
	WHERE t.TasteType = @taste
	ORDER BY CigarLength ASC,
		 CigarRingRange DESC

EXEC usp_SearchByTaste 'Woody'