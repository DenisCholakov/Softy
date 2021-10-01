USE SoftUni

SELECT FirstName, LastName FROM Employees
	WHERE LastName LIKE 'SA%'

SELECT FirstName, LastName FROM Employees
	WHERE SUBSTRING(FirstName, 1, 2) = 'SA'

SELECT FirstName, LastName FROM Employees
	WHERE LastName LIKE '%ei%'

SELECT FirstName, LastName FROM Employees
	WHERE SUBSTRING(LastName, LEN(LastName)-2, 2) = 'ei'

SELECT FirstName FROM Employees
	WHERE (DepartmentID = 3 OR DepartmentID = 10)
	AND DATEPART(YEAR, HireDate) BETWEEN 1995 AND 2005

SELECT FirstName, LastName FROM Employees
	WHERE JobTitle NOT LIKE '%ENGINEER%'

SELECT [Name] FROM Towns
	WHERE LEN([Name]) = 5 OR LEN([Name]) = 6
	ORDER BY [Name]

SELECT TownID, [Name] FROM Towns
	WHERE [Name] LIKE '[MKBE]%'
	ORDER BY [Name]

SELECT * FROM Towns
	WHERE SUBSTRING([Name], 1, 1) IN ('M', 'K', 'B', 'e')
	ORDER BY [Name]

SELECT * FROM Towns
	WHERE LEFT([Name], 1) IN ('M', 'K', 'B', 'e')
	ORDER BY [Name]

SELECT TownID, [Name] FROM Towns
	WHERE [Name] LIKE '[^RBD]%'
	ORDER BY [Name]

CREATE VIEW V_EmployeesHiredAfter2000 AS 
	SELECT FirstName, LastName FROM Employees
	WHERE YEAR(HireDate) > 2000

SELECT * FROM V_EmployeesHiredAfter2000

SELECT FirstName, LastName FROM Employees
	WHERE LEN(LastName) = 5

SELECT * FROM
	(SELECT EmployeeID, FirstName, LastName, Salary, 
		DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank]
		FROM Employees WHERE Salary BETWEEN 10000 AND 50000) AS FQ
	WHERE [Rank] = 2 ORDER BY Salary DESC

WITH Temp AS
	(SELECT EmployeeID, FirstName, LastName, Salary,
		DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank] FROM Employees
		WHERE Salary >= 10000 AND Salary <= 50000)
	SELECT * FROM Temp WHERE [Rank] = 2 ORDER BY Salary DESC
		
USE [Geography]

SELECT CountryName AS [Country Name], IsoCode AS [ISO Code] FROM Countries
	WHERE CountryName LIKE '%A%A%A%' ORDER BY IsoCode

SELECT CountryName AS [Country Name], IsoCode AS [ISO Code] FROM Countries
	WHERE LEN(CountryName)-LEN(REPLACE(CountryName, 'A', '')) >= 3
	ORDER BY IsoCode

SELECT * FROM Peaks

SELECT * FROM Rivers

SELECT p.PeakName, r.RiverName, LOWER(CONCAT(p.PeakName, SUBSTRING(r.RiverName, 2, LEN(r.RiverName)-1))) AS [Mix]
	FROM Peaks AS p, Rivers AS r
	WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
	ORDER BY [Mix]

SELECT p.PeakName, r.RiverName, LOWER(CONCAT(p.PeakName, SUBSTRING(r.RiverName, 2, LEN(r.RiverName)-1))) AS [Mix]
	FROM Peaks AS p
	JOIN Rivers AS r ON RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
	ORDER BY [Mix]

USE Diablo

--DATEPART(YEAR)
SELECT TOP(50) [Name], FORMAT([Start], 'yyyy-MM-dd') AS [Start] FROM Games
	WHERE YEAR([Start]) IN (2011, 2012)
	ORDER BY [Start], [Name]

SELECT Username, SUBSTRING(Email,CHARINDEX('@',Email)+1, 50) AS [Email Provider] FROM Users
	ORDER BY [Email Provider], Username

SELECT Username, IpAddress AS [IP Adress] FROM Users
	WHERE IpAddress LIKE '___.1_%._%.___'
	ORDER BY Username

SELECT [Name] AS [Game], 
	CASE 
		WHEN DATEPART(HOUR, g.[Start]) BETWEEN 0 AND 12 THEN 'Morning'
		WHEN DATEPART(HOUR, g.[Start]) BETWEEN 12 AND 17 THEN 'Afternoon'
		ELSE 'Evening'
	END AS [Part of the Day],
	CASE
		WHEN g.Duration <= 3 THEN 'Extra Short'
		WHEN g.Duration BETWEEN 4 AND 6 THEN 'Short'
		WHEN g.Duration IS NULL THEN 'Extra Long'
		ELSE 'Long'
	END AS [Duration]
	FROM Games AS g
	ORDER BY g.[Name], [Duration], [Part of the Day]

USE Orders

SELECT ProductName, OrderDate, 
	DATEADD(DAY, 3, OrderDate) AS [Pay Due],
	DATEADD(MONTH, 1, OrderDate) AS [Deliver Due] FROM Orders