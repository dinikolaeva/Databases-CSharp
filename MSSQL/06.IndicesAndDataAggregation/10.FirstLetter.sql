SELECT FirstLetter
	FROM (
		SELECT LEFT(w.FirstName, 1) AS FirstLetter
		FROM WizzardDeposits AS w
		WHERE DepositGroup = 'Troll Chest'
		  ) AS flQuery
GROUP BY FirstLetter
ORDER BY FirstLetter ASC