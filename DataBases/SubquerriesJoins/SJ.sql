USE SoftUni

SELECT TOP(5) e.EmployeeID, e.JobTitle, e.AddressID, a.AddressText FROM Employees AS e
	JOIN Addresses  AS a ON a.AddressID = e.AddressID
	ORDER BY AddressID

SELECT TOP(50) e.FirstName, e.LastName, t.[Name] AS Town, a.AddressText FROM Employees AS e
	JOIN Addresses  AS a ON a.AddressID = e.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
	ORDER BY e.FirstName, e.LastName
	
SELECT e.EmployeeID, e.FirstName, e.LastName, d.[Name] AS DepartmentName 
	FROM Employees AS e
	JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
	WHERE d.[Name] = 'Sales'
	ORDER BY e.EmployeeID

SELECT TOP(5) e.EmployeeID, e.FirstName, e.Salary, d.[Name] AS DepartmentName 
	FROM Employees AS e
	JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
	WHERE e.Salary > 15000
	ORDER BY e.DepartmentID

SELECT TOP(3) e.EmployeeID, e.FirstName FROM Employees AS e
	LEFT JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
	WHERE ep.ProjectID IS NULL
	ORDER BY e.EmployeeID

SELECT e.FirstName, e.LastName, e.HireDate, d.[Name] AS DeptName FROM Employees e
	JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
	WHERE YEAR(e.HireDate) >= 1999 AND d.[Name] IN ('Sales', 'Finance')
	ORDER BY e.HireDate

SELECT TOP(5) e.EmployeeID, e.FirstName, p.[Name] AS ProjectName FROM Employees AS e
	JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
	JOIN Projects AS p ON ep.ProjectID = p.ProjectID
	WHERE p.StartDate > '08-13-2002' AND p.EndDate IS NULL
	ORDER BY e.EmployeeID

SELECT e.EmployeeID, e.FirstName, 
			CASE WHEN YEAR(p.StartDate) >= 2005 THEN NULL 
				 ELSE p.[Name] END AS ProjectName 
	FROM Employees AS e
	JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID
	JOIN Projects AS p ON p.ProjectID = ep.ProjectID
	WHERE e.EmployeeID = 24

SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName AS ManagerName FROM Employees AS e
	JOIN Employees AS m ON e.ManagerID = m.EmployeeID
	WHERE e.ManagerID IN (3, 7)
	ORDER BY e.EmployeeID

SELECT TOP(50) e.EmployeeID, e.FirstName + ' ' + e.LastName AS EmployeeName,
				m.FirstName + ' ' + m.LastName AS ManagerName, d.[Name] AS DepartmentName FROM Employees AS e
	JOIN Employees AS m ON e.ManagerID = m.EmployeeID
	JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
	ORDER BY e.EmployeeID

SELECT MIN(FindAvg.AvgSalary) AS MinAverageSalary FROM 
	(SELECT AVG(e.Salary) AS AvgSalary FROM Employees AS e
		GROUP BY e.DepartmentID) AS FindAvg

USE [Geography]

SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation FROM Countries AS c
	JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	JOIN Mountains AS m ON mc.MountainId = m.Id
	JOIN Peaks AS p ON m.Id = p.MountainId
	WHERE c.CountryCode = 'BG' AND p.Elevation > 2835
	ORDER BY p.Elevation DESC

SELECT c.CountryCode, COUNT(m.MountainRange) AS MountainRanges FROM Countries AS c
	JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
	JOIN Mountains AS m ON mc.MountainId = m.Id
	WHERE c.CountryCode IN ('US', 'RU', 'BG')
	GROUP BY c.CountryCode

SELECT TOP(5) c.CountryName, r.RiverName FROM Countries AS c
	LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
	LEFT JOIN Rivers AS r ON cr.RiverId = r.Id
	WHERE c.ContinentCode = 'AF'
	ORDER BY c.CountryName

SELECT ContinentCode, CurrencyCode, CurrencyCount AS CurrencyUsage
	FROM (SELECT ContinentCode, CurrencyCode,CurrencyCount, 
						DENSE_RANK() OVER(PARTITION BY ContinentCode ORDER BY CurrencyCount DESC) AS CurrencyRank
				FROM (SELECT ContinentCode, 
								CurrencyCode, COUNT(*) AS [CurrencyCount] FROM Countries
						GROUP BY ContinentCode, CurrencyCode
						) AS CurrencyCountQuerry
				) AS RankingQuerry
	WHERE CurrencyRank = 1 AND CurrencyCount > 1
	ORDER BY ContinentCode, CurrencyCode

SELECT COUNT(*) AS [Count] FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
	WHERE m.Id IS NULL
	GROUP BY m.Id

SELECT TOP(5) c.CountryName, MAX(p.Elevation) AS HighestPeakElevation,
		MAX(r.[Length]) AS LongestRiverLength FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
	LEFT JOIN Peaks AS p ON P.MountainId = M.Id
	LEFT JOIN CountriesRivers AS rc ON rc.CountryCode = c.CountryCode
	LEFT JOIN Rivers AS r ON rc.RiverId = r.Id
	GROUP BY c.CountryName
	ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, c.CountryName ASC

SELECT TOP(5) Country, ISNULL(pName, '(no highest peak)') AS [Highest Peak Name],
		ISNULL(pElevation, 0) AS [Highest Peak Elevation],
		ISNULL(MountainName, '(no mountain)') AS Mountain FROM 
				(SELECT c.CountryName AS Country,
						p.PeakName AS pName,
						p.Elevation AS pElevation, 
						m.MountainRange AS MountainName,
						DENSE_RANK() OVER(PARTITION BY c.CountryName ORDER BY p.Elevation DESC)
						AS PeakRank
				FROM Countries AS c
				LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
				LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
				LEFT JOIN Peaks AS p ON p.MountainId = m.Id
				) AS RankingQuerry
	WHERE PeakRank = 1
	ORDER BY Country, pName