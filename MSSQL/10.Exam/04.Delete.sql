DELETE Clients
      FROM Clients
      INNER JOIN Addresses AS a ON a.Id = AddressId
      WHERE a.Country LIKE 'C%'

DELETE Addresses
	WHERE Country LIKE 'C%'