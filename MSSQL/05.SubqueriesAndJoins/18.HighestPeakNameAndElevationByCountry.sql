SELECT TOP(5)
	Peaks.CountryName AS Country,
	ISNULL(Peaks.PeakName,'(no highest peak)') AS [Highest Peak Name],
	ISNULL(Peaks.Elevation, 0) AS HighestPeakElevation,
	ISNULL(Peaks.MountainRange, '(no mountain)') AS Mountain
FROM
	  (SELECT c.CountryName, 
		p.PeakName,
		p.Elevation,
		m.MountainRange,
		DENSE_RANK() OVER (PARTITION BY C.CountryName ORDER BY P.Elevation DESC) AS Ranked
		FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains AS m ON mc.MountainId = m.Id
	LEFT JOIN Peaks AS p ON m.Id = p.MountainId) AS Peaks
WHERE Peaks.Ranked = 1
ORDER BY Country, [Highest Peak Name]