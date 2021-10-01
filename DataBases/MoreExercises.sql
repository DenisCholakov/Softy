USE Diablo

SELECT SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email) - CHARINDEX('@', Email)) AS [Email Provider],
		COUNT(*) AS [Number Of Users] FROM Users
GROUP BY SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email) - CHARINDEX('@', Email))
ORDER BY [Number Of Users] DESC, [Email Provider]

SELECT g.[Name] AS Game, 
		gt.[Name] AS [Game Type],
		u.Username,
		ug.[Level],
		ug.Cash, 
		c.[Name] AS [Character] FROM UsersGames AS ug
JOIN Games AS g ON ug.GameId = g.Id
JOIN GameTypes AS gt ON g.GameTypeId = gt.Id
JOIN Users AS u ON ug.UserId = u.Id
JOIN Characters AS c ON ug.CharacterId = c.Id
ORDER BY ug.[Level] DESC, u.Username, g.[Name]

SELECT u.Username,
		g.[Name] AS Game,
		COUNT(i.[Name]) AS [Items Count],
		SUM(i.Price) AS [Items Price] FROM UsersGames AS ug
JOIN Games AS g ON ug.GameId = g.Id
JOIN Users AS u ON ug.UserId = u.Id
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON ugi.ItemId = i.Id
GROUP BY u.Username, g.[Name]
HAVING COUNT(i.[Name]) >= 10
ORDER BY [Items Count] DESC, [Items Price] DESC, u.Username

SELECT u.Username, g.[Name] AS Game, MAX(ch.[Name]) AS [Character],
		(MAX(i.Strength) + MAX(ch.Strength) + MAX(gt.Strength)) AS Strength,
		(MAX(i.Defence) + MAX(ch.Defence) + MAX(gt.Defence)) AS Defence,
		(MAX(i.Speed) + MAX(ch.Speed) + MAX(gt.Speed)) AS Speed,
		(MAX(i.Mind) + MAX(ch.Mind) + MAX(gt.Mind)) AS Mind,
		(MAX(i.Luck) + MAX(ch.Luck) + MAX(gt.Luck)) AS Luck FROM Users AS u
JOIN UsersGames AS ug ON ug.UserId = u.Id
JOIN Games AS g ON g.Id = ug.GameId
JOIN (SELECT ch.Id AS Id, 
				ch.[Name] AS [Name],
				st.Strength AS Strength,
				st.Defence AS [Defence],
				st.Speed AS Speed,
				st.Mind AS Mind,
				st.Luck AS Luck FROM Characters ch
		JOIN [Statistics] AS st ON st.Id = ch.StatisticId) AS ch ON ch.Id = ug.CharacterId
JOIN (SELECT gt.Id AS Id, 
				gt.[Name] AS [Name],
				st.Strength AS Strength,
				st.Defence AS [Defence],
				st.Speed AS Speed,
				st.Mind AS Mind,
				st.Luck AS Luck FROM GameTypes AS gt
		JOIN [Statistics] AS st ON st.Id = gt.BonusStatsId) AS gt ON gt.Id = g.GameTypeId
JOIN (SELECT ug.UserId,
                ug.GameId,
                SUM(s.Strength) AS Strength,
                SUM(s.Defence) AS Defence,
                SUM(s.Speed) AS Speed,
                SUM(s.Mind) AS Mind,
                SUM(s.Luck) AS Luck
        FROM UsersGames AS ug
        JOIN UserGameItems AS ugi
            ON ugi.UserGameId = ug.Id
        JOIN Items AS i
            ON ugi.ItemId = i.Id
        JOIN [Statistics] AS s
            ON s.Id = i.StatisticId
        GROUP BY ug.UserId, ug.GameId) AS i ON i.UserId = u.Id AND i.GameId = g.Id
GROUP BY u.Username, g.[Name]
ORDER BY Strength DESC, Defence DESC, Speed DESC, Mind DESC, Luck DESC

SELECT i.[Name], i.Price, i.MinLevel, s.Strength, s.Defence, s.Speed, s.Luck, s.Mind FROM Items AS i
JOIN [Statistics] AS s ON s.Id = i.StatisticId
WHERE s.Mind > (SELECT AVG(s.Mind) FROM [Statistics] s)
		AND s.Luck > (SELECT AVG(s.Luck) FROM [Statistics] s)
		AND s.Speed > (SELECT AVG(s.Speed) FROM [Statistics] s)
ORDER BY i.[Name]

SELECT i.[Name], i.Price, i.MinLevel, gt.[Name] FROM Items i
LEFT JOIN GameTypeForbiddenItems AS gtfi ON gtfi.ItemId = i.Id
LEFT JOIN GameTypes AS gt ON gt.Id = gtfi.GameTypeId
ORDER BY gt.[Name] DESC, i.[Name]

SELECT * FROM Users u
JOIN UsersGames AS ug ON ug.UserId = u.Id
JOIN Games AS g ON g.Id = ug.GameId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
WHERE u.[Username] = 'Alex' AND g.[Name] = 'Edinburgh'

SELECT * FROM Items
WHERE [Name] IN ('Blackguard', 'Bottomless Potion of Aaplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 
					'Golden Gorget of Leoric', 'Hellfire Amulet')

INSERT INTO UserGameItems(ItemId, UserGameId)
VALUES
		(51, 235),
		(157, 235),
		(184, 235),
		(197, 235),
		(223, 235)

UPDATE UsersGames
SET Cash = Cash - (SELECT SUM(Price) FROM Items 
					WHERE Items.Id IN (51, 157, 184, 197, 223))
WHERE UsersGames.Id = 235

SELECT u.Username, g.[Name], ug.Cash, i.[Name] AS [Item Name] FROM UsersGames ug
JOIN Users AS u ON u.Id = ug.UserId
JOIN Games AS g ON g.Id = ug.GameId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
WHERE g.[Name] = 'Edinburgh'
ORDER BY i.[Name]