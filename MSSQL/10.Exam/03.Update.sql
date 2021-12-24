UPDATE Cigars
	SET PriceForSingleCigar += (PriceForSingleCigar * 0.2)
FROM Cigars AS c
		JOIN Tastes AS t ON c.TastId = t.Id
	WHERE TasteType = 'Spicy'

UPDATE Brands
	SET BrandDescription = 'New description'
	WHERE BrandDescription IS NULL

SELECT * FROM Cigars AS c
JOIN Tastes AS t
        ON c.TastId = t.Id
WHERE TasteType = 'Spicy'

SELECT * FROM Brands
WHERE BrandDescription IS NULL