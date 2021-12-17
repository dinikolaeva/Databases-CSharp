SELECT MountainRange, PeakName, Elevation 
	FROM Peaks AS p
	JOIN Mountains AS m
		 ON m.MountainRange = 'Rila'
		 AND m.Id = p.MountainId
	ORDER BY Elevation DESC