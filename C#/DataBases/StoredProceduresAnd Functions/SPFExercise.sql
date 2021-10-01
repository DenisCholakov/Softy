GO

CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000
AS
BEGIN
	SELECT FirstName AS [First Name], LastName [Last Name] FROM Employees
	WHERE Salary > 35000
END

EXEC usp_GetEmployeesSalaryAbove35000;

GO 

CREATE OR ALTER PROCEDURE usp_GetEmployeesSalaryAboveNumber(@minSalary MONEY)
AS
BEGIN
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary >= @minSalary
END

EXEC usp_GetEmployeesSalaryAboveNumber 48100

GO

CREATE OR ALTER PROCEDURE usp_GetTownsStartingWith(@startsWith VARCHAR(50))
AS
BEGIN
	SELECT [Name] FROM Towns
	WHERE LEFT([Name], LEN(@startsWith)) = @startsWith 
END

EXEC usp_GetTownsStartingWith 'B'

GO

CREATE OR ALTER PROCEDURE usp_GetEmployeesFromTown(@townName VARCHAR(50))
AS
BEGIN
	SELECT e.FirstName, e.LastName FROM Employees AS e
	JOIN Addresses AS a ON a.AddressID = e.AddressID
	JOIN Towns AS t ON t.TownID = a.TownID
	WHERE t.[Name] = @townName
END

EXEC usp_GetEmployeesFromTown 'Sofia'

GO

CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7)
AS
BEGIN
	DECLARE @salaryLevel VARCHAR(7)

	IF (@salary < 30000)
		SET @salaryLevel = 'Low'
	ELSE IF (@salary <= 50000)
		SET @salaryLevel = 'Average'
	ELSE
		SET @salaryLevel = 'High'

	RETURN @salaryLevel
		
END

GO

SELECT FirstName,
		LastName,
		Salary,
		dbo.ufn_GetSalaryLevel(Salary) AS [Salary Level] 
FROM Employees

GO

CREATE PROCEDURE usp_EmployeesBySalaryLevel (@salaryLevel VARCHAR(7))
AS
BEGIN
	SELECT FirstName, LastName FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel
END

EXEC usp_EmployeesBySalaryLevel 'High'

GO

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(100), @word NVARCHAR(50))
RETURNS BIT
AS
BEGIN
	DECLARE @counter INT = 1;
	WHILE(@counter <= LEN(@word))
	BEGIN
		DECLARE @character CHAR = SUBSTRING(@word, @counter, 1)
		IF(CHARINDEX(@character, @setOfLetters) = 0)
		BEGIN
			RETURN 0;
		END
		SET @counter = @counter + 1
	END
	RETURN 1;
END

GO

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves');

GO

CREATE OR ALTER PROCEDURE usp_DeleteEmployeesFromDepartment (@departmentId INT)
AS
BEGIN

	--remove all the projects the employees are working on 
	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (
						SELECT e.EmployeeID FROM Employees e
						WHERE e.DepartmentID = @departmentId
						)

	--remove all managers that need to be deleted
	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN (
						SELECT e.EmployeeID FROM Employees e
						WHERE e.DepartmentID = @departmentId
						)

	--set all the managers of departmants that need to be deleted
	ALTER TABLE Departments
	ALTER COLUMN ManagerID INT

	UPDATE Departments
	SET ManagerID = NULL
	WHERE ManagerID IN (
						SELECT e.EmployeeID FROM Employees e
						WHERE e.DepartmentID = @departmentId
						)

	--delete all employees
	DELETE FROM Employees
	WHERE EmployeeID IN (
							SELECT e.EmployeeID FROM Employees e
							WHERE e.DepartmentID = @departmentId
							)
	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	SELECT COUNT(*) FROM Employees	
	WHERE DepartmentID = @departmentId

END

EXEC usp_DeleteEmployeesFromDepartment 1

USE Bank

GO

CREATE PROCEDURE usp_GetHoldersFullName
AS
BEGIN
	SELECT (FirstName + ' ' + LastName) AS [Full Name] FROM AccountHolders
END

GO

CREATE OR ALTER PROCEDURE usp_GetHoldersWithBalanceHigherThan (@minTotalAmount MONEY)
AS
BEGIN
	SELECT ah.FirstName, ah.LastName FROM AccountHolders ah
	JOIN Accounts AS a ON a.AccountHolderId = ah.Id
	GROUP BY ah.Id, FirstName, LastName
	HAVING SUM(a.Balance) > @minTotalAmount
	ORDER BY FirstName, LastName
END

EXEC usp_GetHoldersWithBalanceHigherThan 50000

GO

CREATE FUNCTION ufn_CalculateFutureValue (@sum MONEY, @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS DECIMAL(18, 4)
AS
BEGIN
	DECLARE @futureValue DECIMAL(18, 4);

	SET @futureValue = @sum * POWER((1 + @yearlyInterestRate), @numberOfYears)

	RETURN @futureValue;
END

GO

select dbo.ufn_CalculateFutureValue(1000, 0.1, 5)

GO

CREATE PROCEDURE usp_CalculateFutureValueForAccount (@accountId INT, @intersetRate MONEY)
AS
BEGIN
	SELECT a.Id AS [Account Id],
			ah.FirstName AS [First Name],
			ah.LastName AS [Last Name],
			a.Balance AS [Current Balance],
			dbo.ufn_CalculateFutureValue(a.Balance, @intersetRate, 5) AS [Balance in 5 years] FROM AccountHolders AS ah
	JOIN Accounts AS a ON a.AccountHolderId = ah.Id
	WHERE a.Id = @accountId
END

EXEC usp_CalculateFutureValueForAccount 1, 0.1

USE Diablo

GO

CREATE FUNCTION ufn_CashInUsersGames (@gameName NVARCHAR(50))
RETURNS TABLE
AS
	RETURN SELECT (SELECT SUM(Cash) FROM (SELECT g.[Name],
									ug.Cash,
									ROW_NUMBER() OVER (PARTITION BY g.[Name] ORDER BY ug.Cash DESC) AS RN FROM UsersGames AS ug
							JOIN Games AS g ON g.Id = ug.GameId
							WHERE g.[Name] = @gameName) AS RowNumbering
					WHERE RN % 2 <> 0) AS FindOddSum

GO