USE SoftUni

SELECT CONCAT(FirstName, '.', LastName, '@', 'softuni.bg') FROM Employees

--за да избегнем double space когато нямаме второ име MiddleName + ' ' = NULL, защото NULL + ' ' = String.Empty
SELECT CONCAT(FirstName, ' ', MiddleName + ' ', LastName) AS [Full Name] FROM Employees
	WHERE Salary IN (25000, 14000, 12500, 23600)

SELECT FirstName, LastName, JobTitle FROM Employees
	WHERE Salary BETWEEN 20000 AND 30000

SELECT TOP(5) FirstName , LastName FROM Employees
	WHERE Salary > 50000 ORDER BY Salary DESC


SELECT FirstName , LastName FROM Employees
	WHERE DepartmentID != 4

SELECT * FROM Employees
	ORDER BY Salary DESC, FirstName, LastName DESC, MiddleName

--Select from [View Name]
CREATE VIEW V_EmployeesSalaries AS
	SELECT FirstName, LastName, Salary FROM Employees;

CREATE VIEW V_EmployeeNameJobTitle AS
	SELECT CONCAT(FirstName, ' ', MiddleName + ' ', LastName) AS [Full Name], JobTitle FROM Employees

--CREATE VIEW V_EmployeeNameJobTitle AS
--SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name], JobTitle 
--FROM Employees

SELECT DISTINCT JobTitle FROM Employees

SELECT TOP(10) * FROM Projects
	ORDER BY StartDate, [Name]

SELECT TOP(7) FirstName,LastName, HireDate FROM Employees
	ORDER BY HireDate DESC

UPDATE Employees SET Salary += Salary * 0.12
	WHERE DepartmentID IN (1, 2, 4, 11) 

SELECT Salary FROM Employees

SELECT * FROM Departments

DROP DATABASE SoftUni


USE Geography

SELECT PeakName FROM Peaks
	ORDER BY PeakName

SELECT TOP(30) CountryName, [Population] FROM Countries
	WHERE ContinentCode = (SELECT ContinentCode FROM Continents
								WHERE ContinentName = 'Europe')
	ORDER BY [Population] DESC, CountryName

SELECT CountryName, CountryCode,
	CASE WHEN CurrencyCode = 'EUR' THEN 'Euro'
		ELSE 'Not Euro' END AS [Currency]
	FROM Countries
	ORDER BY CountryName

USE Diablo

SELECT Name FROM Characters
	ORDER BY Name

