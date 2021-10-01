CREATE DATABASE TripService

GO

USE TripService

GO

CREATE TABLE Cities (
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(20) NOT NULL,
CountryCode CHAR(2) NOT NULL
)

CREATE TABLE Hotels (
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(30) NOT NULL,
CityId INT NOT NULL REFERENCES Cities(Id),
EmployeeCount INT NOT NULL,
BaseRate DECIMAL (18, 2)
)

CREATE TABLE Rooms (
Id INT PRIMARY KEY IDENTITY,
Price DECIMAL(18, 2) NOT NULL,
[Type] NVARCHAR(20) NOT NULL,
Beds INT NOT NULL,
HotelId INT NOT NULL REFERENCES Hotels(Id)
)

CREATE TABLE Trips (
Id INT PRIMARY KEY IDENTITY,
RoomId INT NOT NULL REFERENCES Rooms(Id),
BookDate DATE NOT NULL,
ArrivalDate DATE NOT NULL,
ReturnDate DATE NOT NULL,
CancelDate DATE,

CHECK (BookDate < ArrivalDate),
CHECK (ArrivalDate < ReturnDate)
)

CREATE TABLE Accounts (
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(50) NOT NULL,
MiddleName NVARCHAR(20),
LastName NVARCHAR(50) NOT NULL,
CityId INT NOT NULL REFERENCES Cities(Id),
BirthDate DATE NOT NULL,
Email VARCHAR(100) NOT NULL UNIQUE
)

CREATE TABLE AccountsTrips (
AccountId INT NOT NULL REFERENCES Accounts(Id),
TripId INT NOT NULL REFERENCES Trips(Id),
Luggage INT NOT NULL CHECK (Luggage >= 0),

CONSTRAINT PK_AccountsTrips PRIMARY KEY (AccountId, TripId)
)

INSERT INTO Accounts (FirstName, MiddleName, LastName, CityId, BirthDate, Email)
VALUES  ('John', 'Smith', 'Smith', 34, '1975-07-21', 'j_smith@gmail.com'),
		('Gosho', NULL, 'Petrov', 11, '1978-05-16', 'g_petrov@gmail.com'),
		('Ivan', 'Petrovich', 'Pavlov', 59, '1849-09-26', 'i_pavlov@softuni.bg'),
		('Friedrich', 'Wilhelm', 'Nietzsche', 2, '1844-10-15', 'f_nietzsche@softuni.bg')

SET IDENTITY_INSERT Rooms OFF

INSERT INTO Trips (RoomId, BookDate, ArrivalDate, ReturnDate, CancelDate)
VALUES  (101, '2015-04-12', '2015-04-14', '2015-04-20', '2015-02-02'),
		(102, '2015-07-07', '2015-07-15', '2015-07-22', '2015-04-29'),
		(103, '2013-07-17', '2013-07-23', '2013-07-24', NULL),
		(104, '2012-03-17', '2012-03-31', '2012-04-01', '2012-01-10'),
		(109, '2017-08-07', '2017-08-28', '2017-08-29', NULL)

SET IDENTITY_INSERT Rooms ON

UPDATE Rooms SET Price += Price * 0.14
WHERE HotelId IN (5, 7, 9)

DELETE AccountsTrips WHERE AccountId = 47


SELECT FirstName, LastName, FORMAT(BirthDate, 'MM-dd-yyyy'), c.[Name] AS Hometown,
		Email FROM Accounts AS a
JOIN Cities AS c ON c.Id = a.CityId
WHERE Email LIKE 'e%'
ORDER BY c.[Name]

SELECT c.[Name] AS City, COUNT(h.Id) AS Hotels FROM Cities AS c
JOIN Hotels AS h ON h.CityId = c.Id
GROUP BY c.Id, c.[Name]
ORDER BY Hotels DESC, c.[Name]

SELECT a.Id AS AccountId, 
		a.FirstName + ' ' + a.LastName AS FullName,
		MAX(DATEDIFF(day, t.ArrivalDate, t.ReturnDate)) AS LongestTrip,
		MIN(DATEDIFF(day, t.ArrivalDate, t.ReturnDate)) AS ShortestTrip FROM Accounts AS a
JOIN AccountsTrips AS atr ON atr.AccountId = a.Id
JOIN Trips AS t ON t.Id = atr.TripId
WHERE t.CancelDate IS NULL AND a.MiddleName IS NULL
GROUP BY a.Id, a.FirstName, a.LastName
ORDER BY LongestTrip DESC, ShortestTrip

SELECT TOP(10) c.Id, 
		c.[Name] AS City, 
		c.CountryCode AS Country,
		COUNT(a.Id) AS Accounts FROM Cities AS c
JOIN Accounts AS a ON a.CityId = c.Id
GROUP BY c.Id, c.[Name], c.CountryCode
ORDER BY Accounts DESC

SELECT a.Id, a.Email, c1.[Name], COUNT(t.Id) AS Trips FROM Accounts AS a
JOIN Cities AS c1 ON c1.Id = a.CityId
JOIN AccountsTrips AS atr ON atr.AccountId = a.Id
JOIN Trips AS t ON t.Id = atr.TripId
JOIN Rooms AS r ON t.RoomId = r.Id
JOIN Hotels AS h ON h.Id = r.HotelId
JOIN Cities AS c2 ON c2.Id = h.CityId
WHERE c1.Id = c2.Id
GROUP BY a.Id, a.Email, c1.[Name]
ORDER BY Trips DESC, a.Id

SELECT t.Id, 
		CONCAT(a.FirstName + ' ', a.MiddleName + ' ', a.LastName) AS [Full Name],
		ca.[Name] AS [From],
		ch.[Name] AS [To],
		CASE 
			WHEN t.CancelDate IS NULL THEN CONCAT (DATEDIFF(day, t.ArrivalDate, t.ReturnDate), ' days')
			ELSE 'Canceled'
		END AS Duration FROM Trips AS t
LEFT JOIN AccountsTrips AS atr ON atr.TripId = t.Id
LEFT JOIN Accounts AS a ON a.Id = atr.AccountId
JOIN Cities AS ca ON ca.Id = a.CityId
JOIN Rooms AS r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
JOIN Cities AS ch ON ch.Id = h.CityId
ORDER BY [Full Name], t.Id

--programability

GO

CREATE OR ALTER FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @roomId INT = (SELECT Id FROM Rooms AS r
							WHERE r.HotelId = @HotelId AND 
							r.Price = (SELECT MAX(r.Price) FROM Rooms AS r
										WHERE r.HotelId = @HotelId AND 
										r.Id NOT IN (SELECT r.Id FROM Rooms AS r
														JOIN Trips AS t ON t.RoomId = r.Id
														WHERE  r.HotelId = @HotelId AND 
														(@Date BETWEEN t.ArrivalDate AND t.ReturnDate
														OR t.CancelDate IS NOT NULL) AND r.Beds >= @People)))
	IF (@roomId IS NULL)
		RETURN 'No rooms available'
	DECLARE @roomType NVARCHAR(20) = (SELECT [Type] FROM Rooms
										WHERE Id = @roomId)
	DECLARE @bedsCount INT = (SELECT Beds FROM Rooms WHERE Id = @roomId)
	DECLARE @roomPrice DECIMAL(18, 2) = (SELECT Price FROM Rooms WHERE Id = @roomId)
	DECLARE @hotelRate DECIMAL(18, 2) = (SELECT BaseRate FROM Hotels WHERE Id = @HotelId)


	DECLARE @totalPrice DECIMAL(18, 2) = (@roomPrice + @hotelRate) * @People

	RETURN CONCAT('Room ', @roomId, ': ', @roomType, ' (', @bedsCount, ' beds) - $', @totalPrice)

END

GO

SELECT dbo.udf_GetAvailableRoom(112, '2011-12-17', 2)

SELECT dbo.udf_GetAvailableRoom(94, '2015-07-26', 3)

SELECT * FROM Rooms AS r
JOIN Trips AS t ON t.RoomId = r.Id
WHERE r.HotelId = 94 AND r.Price = (SELECT MAX(r.Price) FROM Rooms AS r
JOIN Trips AS t ON t.RoomId = r.Id
WHERE r.HotelId = 94 AND NOT ( '2015-07-26' BETWEEN t.ArrivalDate AND t.ReturnDate)
AND t.CancelDate IS NULL AND r.Beds >= 3)

DECLARE @roomId INT = (SELECT Id FROM Rooms AS r
						WHERE r.HotelId = @HotelId AND r.Price = (SELECT MAX(r.Price) FROM Rooms AS r
						JOIN Trips AS t ON t.RoomId = r.Id
						WHERE r.HotelId = @HotelId AND NOT ( @Date BETWEEN t.ArrivalDate AND t.ReturnDate)
						AND t.CancelDate IS NULL AND r.Beds >= @People))

SELECT Id FROM Rooms AS r
						WHERE r.HotelId = 112 AND 
						r.Price = (SELECT MAX(r.Price) FROM Rooms AS r
									WHERE r.HotelId = 112 AND r.Id NOT IN (SELECT r.Id FROM Rooms AS r
														JOIN Trips AS t ON t.RoomId = r.Id
														WHERE  r.HotelId = 112 AND 
														('2011-12-17' BETWEEN t.ArrivalDate AND t.ReturnDate
														OR t.CancelDate IS NOT NULL) AND r.Beds >= 2))

GO

CREATE OR ALTER PROCEDURE usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
	DECLARE @currentHotelId INT = (SELECT h.Id FROM Hotels AS h 
									JOIN Rooms AS r ON r.HotelId = h.Id
									JOIN Trips AS t ON t.RoomId = r.Id
									WHERE t.Id = @TripId)

	DECLARE @targetHotelId INT = (SELECT h.Id FROM Hotels AS h 
									JOIN Rooms AS r ON r.HotelId = h.Id
									WHERE r.Id = @TargetRoomId)

	DECLARE @tripAccounts INT = (SELECT COUNT(*) FROM Trips WHERE Id = @TripId)

	DECLARE @targetRoomBeds INT = (SELECT Beds FROM Rooms WHERE Id = @TargetRoomId)

	IF (@currentHotelId != @targetHotelId)
		THROW 51000, 'Target room is in another hotel!', 1

	IF (@tripAccounts >= @targetRoomBeds)
		THROW 51001, 'Not enough beds in target room!', 1

	UPDATE Trips SET RoomId = @TargetRoomId WHERE Id = @TripId
END
	
EXEC usp_SwitchRoom 10, 11
SELECT RoomId FROM Trips WHERE Id = 10

EXEC usp_SwitchRoom 10, 7

EXEC usp_SwitchRoom 10, 8


SELECT a.FirstName, a.LastName, c.[Name] FROM Accounts AS a
JOIN Cities AS c ON c.Id = a.CityId