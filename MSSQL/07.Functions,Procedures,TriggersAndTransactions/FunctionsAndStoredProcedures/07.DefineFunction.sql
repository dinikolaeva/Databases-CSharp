CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(20), @word VARCHAR(20))
RETURNS BIT
AS
BEGIN
	DECLARE @i INT = 1

	WHILE @i <= LEN(@word)
		BEGIN
			IF (CHARINDEX(SUBSTRING(@word, @i, 1), @setOfLetters) = 0)
				BEGIN
					RETURN 0
				END
			SET @i += 1
		END
	RETURN 1
END

CREATE TABLE LettersInWord
(
	SetOfLetters VARCHAR(20) NOT NULL,
	Word VARCHAR(20) NOT NULL
)

INSERT INTO LettersInWord (SetOfLetters, Word) VALUES
('oistmiahf', 'Sofia'),
('oistmiahf', 'halves'),
('bobr', 'Rob'),
('pppp', 'Guy')

SELECT lw.SetOfLetters, 
	   lw. Word, 
	   ([dbo].[ufn_IsWordComprised](lw.SetOfLetters, lw. Word)) AS Result 
FROM LettersInWord AS lw