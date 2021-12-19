SELECT
		mc.CountryCode,
		COUNT(m.MountainRange)
FROM MountainsCountries AS mc
	JOIN Mountains AS m ON m.Id = mc.MountainId
		WHERE CountryCode IN ('US', 'RU', 'BG')
		GROUP BY(mc.CountryCode)