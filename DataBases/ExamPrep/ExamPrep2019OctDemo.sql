CREATE DATABASE Bitbucket

GO

USE Bitbucket

GO

CREATE TABLE Users (
Id INT PRIMARY KEY IDENTITY,
Username VARCHAR(30) NOT NULL,
[Password] VARCHAR(30) NOT NULL,
Email VARCHAR(50) NOT NULL
)

CREATE TABLE Repositories (
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE RepositoriesContributors (
RepositoryId INT NOT NULL REFERENCES Repositories(Id),
ContributorId INT NOT NULL REFERENCES Users(Id),

CONSTRAINT PK_RepositoriesContributors PRIMARY KEY (RepositoryId, ContributorId)
)

CREATE TABLE Issues (
Id INT PRIMARY KEY IDENTITY,
Title VARCHAR(255) NOT NULL,
IssueStatus CHAR(6) NOT NULL,
RepositoryId INT NOT NULL REFERENCES Repositories(Id),
AssigneeId INT NOT NULL REFERENCES Users(Id)
)

CREATE TABLE Commits (
Id INT PRIMARY KEY IDENTITY,
[Message] VARCHAR(255) NOT NULL,
IssueId INT REFERENCES Issues(Id),
RepositoryId INT NOT NULL REFERENCES Repositories(Id),
ContributorId INT NOT NULL REFERENCES Users(Id)
)

CREATE TABLE Files (
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(100) NOT NULL,
Size DECIMAL(18, 2) NOT NULL,
ParentId INT REFERENCES Files(Id),
CommitId INT NOT NULL REFERENCES Commits(Id)
)

INSERT INTO Files ([Name], Size, ParentId, CommitId)
VALUES  ('Trade.idk', 2598.0, 1, 1),
		('menu.net', 9238.31, 2, 2),
		('Administrate.soshy', 1246.93, 3, 3),
		('Controller.php', 7353.15, 4, 4),
		('Find.java', 9957.86, 5, 5),
		('Controller.json', 14034.87, 3, 6),
		('Operate.xix', 7662.92, 7, 7)

INSERT INTO Issues (Title, IssueStatus, RepositoryId, AssigneeId)
VALUES  ('Critical Problem with HomeController.cs file', 'open', 1, 4),
		('Typo fix in Judge.html', 'open', 4, 3),
		('Implement documentation for UsersService.cs', 'closed', 8, 2),
		('Unreachable code in Index.cs', 'open', 9, 8)

UPDATE Issues SET IssueStatus = 'closed'
WHERE AssigneeId = 6

DELETE FROM Issues 
WHERE RepositoryId = (SELECT Id FROM Repositories
						WHERE [Name] = 'Softuni-Teamwork')

DELETE FROM RepositoriesContributors
WHERE RepositoryId = (SELECT Id FROM Repositories
						WHERE [Name] = 'Softuni-Teamwork')

SELECT Id, [Message], RepositoryId, ContributorId FROM Commits
ORDER BY Id, [Message], RepositoryId, ContributorId

SELECT Id, [Name], Size FROM Files
WHERE Size > 1000 AND [Name] LIKE '%html%'
ORDER BY Size DESC, Id, [Name]

SELECT i.Id, CONCAT(u.[Username], ' : ', i.Title) FROM Issues AS i
JOIN Users AS u ON u.Id = i.AssigneeId
ORDER BY i.Id DESC, i.AssigneeId

SELECT f2.Id, f2.[Name], CONCAT(f2.Size, 'KB') AS Size FROM Files AS f1
RIGHT JOIN Files AS f2 ON f2.Id = f1.ParentId
WHERE f1.Id IS NULL
ORDER BY f2.Id, f2.[Name], f2.Size

SELECT TOP(5) r.Id AS Id, r.[Name], COUNT(c.Id) AS Commits FROM Commits AS c
JOIN Issues AS i ON i.Id = c.IssueId
JOIN Users AS u ON u.Id = i.AssigneeId
JOIN RepositoriesContributors AS rc ON rc.ContributorId = u.Id
JOIN Repositories AS r ON r.Id = rc.RepositoryId
JOIN Repositories AS r2 ON r2.Id = c.RepositoryId
GROUP BY r.Id, r.[Name]
ORDER BY Commits DESC, r.Id, r.[Name]

SELECT RepositoryId, COUNT(*) FROM Commits
GROUP BY RepositoryId

-- kak i zashto
SELECT TOP(5) r.Id, r.[Name], COUNT(c.RepositoryId) AS [Commits] FROM Repositories AS r
JOIN Commits AS c
ON c.RepositoryId = r.Id
LEFT JOIN RepositoriesContributors AS rc
ON rc.RepositoryId = r.Id
GROUP BY r.Id, r.[Name]
ORDER BY [Commits] DESC, r.Id, r.[Name]

SELECT u.Username, AVG(f.Size) AS Size FROM Users AS u
JOIN Commits AS c ON c.ContributorId = u.Id
JOIN Files AS f ON f.CommitId = c.Id
GROUP BY u.Id, u.Username
ORDER BY Size DESC, U.Username

--programability

GO

CREATE FUNCTION udf_UserTotalCommits(@username VARCHAR(30))
RETURNS INT 
AS 
BEGIN 
	RETURN (SELECT COUNT(*) FROM Users AS u
			JOIN Commits AS c ON c.ContributorId = u.Id
			WHERE u.Username = @username)
END

GO

SELECT dbo.udf_UserTotalCommits('UnderSinduxrein')

GO

CREATE PROCEDURE usp_FindByExtension(@extension VARCHAR(50))
AS
BEGIN
	SELECT Id, [Name], CONCAT(Size, 'KB') AS Size FROM Files
	WHERE [Name] LIKE '%' + @extension
	ORDER BY Id, [Name], Files.Size DESC
END


exec usp_FindByExtension 'txt'