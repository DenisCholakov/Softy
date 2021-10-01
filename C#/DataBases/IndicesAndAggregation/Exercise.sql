USE Gringotts

SELECT COUNT(*) AS [Count] FROM WizzardDeposits

SELECT MAX(MagicWandSize) AS LongestMagicWand FROM WizzardDeposits

SELECT DepositGroup, MAX(MagicWandSize) AS LongestMAgicWand FROM WizzardDeposits
	GROUP BY DepositGroup
		
SELECT DepositGroup FROM (SELECT DepositGroup, DENSE_RANK() OVER( ORDER BY AvgSize ) AS AvgRank
							FROM (SELECT DepositGroup, AVG(MagicWandSize) AS AvgSize FROM WizzardDeposits
									GROUP BY DepositGroup) AS FindAvg
						) AS RankingAvg
	WHERE AvgRank = 1

SELECT DepositGroup, SUM(DepositAmount) AS TotalSum FROM WizzardDeposits
	GROUP BY DepositGroup

SELECT DepositGroup, SUM(DepositAmount) AS TotalSum FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup

SELECT DepositGroup, SUM(DepositAmount) AS TotalSum FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup
	HAVING SUM(DepositAmount) < 150000
	ORDER BY TotalSum DESC

SELECT DepositGroup, MagicWandCreator, DepositCharge AS MinDepositCharge 
	FROM (SELECT DepositGroup, MagicWandCreator, DepositCharge,
					DENSE_RANK() OVER( PARTITION BY DepositGroup ORDER BY DepositCharge) AS DCRank 
			FROM WizzardDeposits) AS RankingDepositCharge
	WHERE DCRank = 1
	ORDER BY DepositGroup, MagicWandCreator

SELECT DepositGroup, MagicWandCreator, MIN(DepositCharge) AS MinDepositCharge FROM WizzardDeposits
	GROUP BY DepositGroup, MagicWandCreator
	ORDER BY MagicWandCreator, DepositGroup

SELECT AgeGroup, COUNT(*) AS WizzardCount 
	FROM (SELECT *, CASE 
					WHEN Age BETWEEN 0 AND 10 THEN '[0-10]' 
					WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
					WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
					WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
					WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
					WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
					ELSE '[61+]'
					END AS AgeGroup
			FROM WizzardDeposits) AS CreatingAgeGroups
	GROUP BY AgeGroup

SELECT LEFT(FirstName, 1) AS FirstLetter FROM WizzardDeposits
	WHERE DepositGroup = 'Troll Chest'
	GROUP BY LEFT(FirstName, 1)

SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest) AS AverageInterest FROM WizzardDeposits
	WHERE DepositStartDate > '01-01-1985'
	GROUP BY DepositGroup, IsDepositExpired
	ORDER BY DepositGroup DESC, IsDepositExpired
	
DECLARE @cnt INT = 0;

SELECT SUM ([Difference]) AS SumDifference
	FROM (SELECT host.FirstName AS [Host Wizzaard],
					host.DepositAmount AS [Host Wizzard Deposit],
					guest.FirstName AS [Guest Wizzard],
					guest.DepositAmount AS [Guest Wizzard Deposit],
					host.DepositAmount - guest.DepositAmount AS [Difference] FROM WizzardDeposits AS host
			JOIN WizzardDeposits AS guest ON host.Id = guest.Id - 1) AS GameTable

USE SoftUni

SELECT DepartmentID, SUM(Salary) AS TotalSalary FROM Employees
	GROUP BY DepartmentID\

SELECT DepartmentID, MIN(Salary) AS MinimumSalary FROM Employees
	WHERE DepartmentID IN (2, 5, 7) AND HireDate > '01-01-2000'
	GROUP BY DepartmentID

SELECT * INTO TempEmployees FROM Employees
	WHERE Salary > 30000

DELETE FROM TempEmployees WHERE ManagerID = 42

UPDATE TempEmployees SET Salary += 5000
	WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSAlary FROM TempEmployees
	GROUP BY DepartmentID

DROP TABLE TempEmployees

SELECT DepartmentID, MAX(Salary) AS MaxSalary FROM Employees
	GROUP BY DepartmentID
	HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

SELECT COUNT(Salary) AS [Count] FROM Employees
	WHERE ManagerID IS NULL

SELECT DepartmentID, Salary AS ThirdHighestSalary 
	FROM (SELECT DepartmentID, Salary,
					DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS SalaryRank 
			FROM Employees) AS RankingSalaries
	WHERE SalaryRank = 3
	GROUP BY DepartmentID, Salary

SELECT TOP(10) e.FirstName, e.LastName, e.DepartmentID FROM Employees AS e
	WHERE e.Salary > (SELECT AVG(avgEmp.Salary) FROM Employees AS avgEmp
						WHERE e.DepartmentID = avgEmp.DepartmentID
						GROUP BY avgEmp.DepartmentID)
	ORDER BY e.DepartmentID
