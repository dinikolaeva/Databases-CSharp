SELECT SUM([Difference]) AS SumDifference
	FROM (
		SELECT
		w.DepositAmount-(LEAD (w.DepositAmount) OVER (ORDER BY w.Id)) AS [Difference]
		FROM WizzardDeposits AS w) AS DiffQuery