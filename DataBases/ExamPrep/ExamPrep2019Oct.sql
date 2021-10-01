CREATE DATABASE [Service]

GO

USE [Service]

GO

CREATE TABLE Users (
Id INT PRIMARY KEY IDENTITY,
Username VARCHAR(30) UNIQUE NOT NULL,
[Password] VARCHAR(50) NOT NULL,
[Name] VARCHAR(50),
Birthdate DATETIME2,
Age INT CHECK(Age BETWEEN 14 AND 110),
Email VARCHAR(50) NOT NULL
)

CREATE TABLE Departments (
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Employees (
Id INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(25),
LastName VARCHAR(25),
Birthdate DATETIME2,
Age INT CHECK(Age >= 18 AND Age <= 110),
DepartmentId INT REFERENCES Departments(Id)
)

CREATE TABLE Categories (
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(50) NOT NULL,
DepartmentId INT NOT NULL REFERENCES Departments(Id)
)

CREATE TABLE [Status] (
Id INT PRIMARY KEY IDENTITY,
[Label] VARCHAR(30) NOT NULL
)

CREATE TABLE Reports (
Id INT PRIMARY KEY IDENTITY,
CategoryId INT NOT NULL REFERENCES Categories(Id),
StatusId INT NOT NULL REFERENCES [Status](Id),
OpenDate DATETIME2 NOT NULL,
CloseDate DATETIME2,
[Description] VARCHAR(200) NOT NULL,
UserId INT NOT NULL REFERENCES Users(Id),
EmployeeId INT REFERENCES Employees(Id)
)

INSERT INTO Employees (FirstName, LastName, Birthdate, DepartmentId)
VALUES ('MarLo', 'O''Malley', '1958-9-21', 1),
		('Niki', 'Stanaghan', '1969-11-26', 4),
		('Ayrton', 'Senna', '1960-03-21', 9),
		('Ronnie', 'Peterson', '1944-02-14', 9),
		('Giovanna', 'Amati', '1959-07-20', 5)

INSERT INTO Reports (CategoryId, StatusId, OpenDate, CloseDate, [Description], UserId, EmployeeId)
VALUES (1, 1, '2017-04-13', NULL, 'Stuck Road on Str.133', 6, 2),
		(6, 3, '2015-09-05', '2015-12-06', 'Charity trail running', 3, 5),
		(14, 2, '2015-09-07', NULL, 'Falling bricks on Str.58', 5, 2),
		(4, 3, '2017-07-03', '2017-07-06', 'Cut off streetlight on Str.11', 1, 1)

UPDATE Reports SET CloseDate = GETDATE()
	WHERE CloseDate IS NULL

DELETE Reports WHERE StatusId = 4

--Querying

SELECT [Description], FORMAT(OpenDate, 'dd-MM-yyyy') FROM Reports
WHERE EmployeeId IS NULL
ORDER BY Reports.OpenDate, [Description]

SELECT r.[Description], cat.[Name] AS CategoryName FROM Reports AS r
JOIN Categories AS cat ON cat.Id = r.CategoryId
ORDER BY [Description], CategoryName

SELECT TOP(5) c.[Name] AS CategoryName, COUNT(*) AS ReportsNumber FROM Reports AS r
JOIN Categories AS c ON c.Id = r.CategoryId
GROUP BY r.CategoryId, c.[Name]
ORDER BY ReportsNumber DESC, CategoryName

SELECT u.Username, c.[Name] AS CategoryName FROM Users AS u
JOIN Reports AS r ON r.UserId = u.Id
JOIN Categories AS c ON c.Id = r.CategoryId
WHERE month(u.Birthdate) = month(r.OpenDate)
	and day(u.Birthdate) = day(r.OpenDate)
ORDER BY u.Username, c.[Name]

SELECT e.FirstName + ' ' + e.LastName AS FullName,
		COUNT(u.Id) AS UsersCount FROM Employees AS e
LEFT JOIN Reports AS r ON r.EmployeeId = e.Id
LEFT JOIN Users AS u ON u.Id = r.UserId
GROUP BY e.Id, e.FirstName, e.LastName
ORDER BY UsersCount DESC, FullName

SELECT ISNULL(es.FirstName + ' ' + es.LastName, 'None') AS Employee,
		ISNULL(d.[Name], 'None') AS Department,
		ISNULL(c.[Name], 'None') AS Category,
		r.[Description],
		FORMAT(r.OpenDate, 'dd.MM.yyyy'),
		s.[Label] AS [Status],
		ISNULL(u.[Name], 'None') AS [User] FROM Reports AS r
LEFT JOIN Employees AS es ON es.Id = r.EmployeeId
LEFT JOIN Categories AS c ON c.Id = r.CategoryId
LEFT JOIN Departments AS d ON d.Id = es.DepartmentId
LEFT JOIN [Status] AS s ON s.Id = r.StatusId
LEFT JOIN Users AS u ON u.Id = r.UserId
ORDER BY es.FirstName DESC,
		es.LastName DESC,
		Department,
		Category,
		[Description],
		r.OpenDate,
		[Status],
		[User]

--Programability

GO

CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME2, @EndDate DATETIME2)
RETURNS INT
BEGIN
	IF (@StartDate IS NULL)
		RETURN 0
	IF (@EndDate IS NULL)
		RETURN 0

	RETURN DATEDIFF(HOUR, @StartDate, @EndDate)
END

GO

SELECT dbo.udf_HoursToComplete(OpenDate, CloseDate) AS TotalHours
   FROM Reports

GO

CREATE PROCEDURE usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
BEGIN
	DECLARE @EmpDepId INT = (SELECT DepartmentId FROM Employees
						WHERE Id = @EmployeeId)

	DECLARE @RepDepId INT = (SELECT c.DepartmentId FROM Reports AS r
							JOIN Categories AS c ON c.Id = r.CategoryId 
							WHERE r.Id = @ReportId)

	IF (@EmpDepId != @RepDepId)
		THROW 50001, 'Employee doesn''t belong to the appropriate department!', 1

	UPDATE Reports SET EmployeeId = @EmployeeId
	WHERE Id = @ReportId

END