CREATE DATABASE Airport

USE Airport

CREATE TABLE Planes (
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(30) NOT NULL,
Seats INT NOT NULL,
[Range] INT NOT NULL
)

CREATE TABLE Flights (
Id INT PRIMARY KEY IDENTITY,
DepartureTime DATETIME2,
ArrivalTime DATETIME2,
Origin VARCHAR(50) NOT NULL,
Destination VARCHAR(50) NOT NULL,
PlaneId INT REFERENCES Planes(Id) NOT NULL
)

CREATE TABLE Passengers (
Id INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(30) NOT NULL,
LastName VARCHAR(30) NOT NULL,
Age INT NOT NULL,
[Address] VARCHAR(30) NOT NULL,
PassportId VARCHAR(11) NOT NULL
)

CREATE TABLE LuggageTypes (
Id INT PRIMARY KEY IDENTITY,
[Type] VARCHAR(30) NOT NULL
)

CREATE TABLE Luggages (
Id INT PRIMARY KEY IDENTITY,
LuggageTypeId INT REFERENCES LuggageTypes(Id) NOT NULL,
PassengerId INT REFERENCES Passengers(Id) NOT NULL
)

CREATE TABLE Tickets (
Id INT PRIMARY KEY IDENTITY,
PassengerId INT REFERENCES Passengers(Id) NOT NULL,
FlightId INT REFERENCES Flights(Id) NOT NULL,
LuggageId INT FOREIGN KEY REFERENCES Luggages(Id) NOT NULL,
Price DECIMAL(15,2) NOT NULL
)


INSERT INTO Planes([Name], Seats, [Range])
VALUES ('Airbus 336', 112, 5132),
		('Airbus 330', 432, 5325),
		('Boeing 369', 231, 2355),
		('Stelt 297', 254, 2143),
		('Boeing 338', 165, 5111),
		('Airbus 558', 387, 1342),
		('Boeing 128', 345, 5541)

INSERT INTO LuggageTypes ([Type])
VALUES ('Crossbody Bag'),
		('School Backpack'),
		('Shoulder Bag')

UPDATE Tickets SET Price += Price * 0.13
WHERE FlightId = (
					SELECT TOP(1) Id FROM Flights
					WHERE Destination = 'Carlsbad'
					)

DELETE FROM Tickets
	WHERE FlightId IN (
						SELECT Id FROM Flights
						WHERE Destination = 'Ayn Halagim'
						)

DELETE FROM Flights
	WHERE Destination = 'Ayn Halagim'

SELECT * FROM Planes
WHERE [Name] LIKE '%tr%'
ORDER BY Id, [Name], Seats, [Range]

SELECT t.FlightId, SUM(t.Price) AS Price FROM Flights AS f
JOIN Tickets AS t ON t.FlightId = f.Id
GROUP BY t.FlightId
ORDER BY Price DESC, t.FlightId

SELECT CONCAT(FirstName, ' ', LastName) AS [Full Name],
		f.Origin, f.Destination
		FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON f.Id = t.FlightId
ORDER BY [Full Name], f.Origin, f.Destination

SELECT FirstName, LastName, Age FROM Passengers AS p
LEFT JOIN Tickets AS t ON t.PassengerId = p.Id
WHERE t.FlightId IS NULL
ORDER BY Age DESC, FirstName, LastName

SELECT CONCAT(p.FirstName, ' ', p.LastName) AS [Full Name],
		pl.[Name] AS [Plane Name],
		CONCAT(f.Origin, ' - ', f.Destination) AS Trip,
		lt.[Type] AS [Luggage Type] FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON f.Id = t.FlightId
JOIN Planes AS pl ON pl.Id = f.PlaneId
JOIN Luggages AS l ON t.LuggageId = l.Id
JOIN LuggageTypes AS lt ON lt.Id = l.LuggageTypeId
ORDER BY [Full Name], pl.[Name], f.Origin, f.Destination, lt.[Type]

SELECT p.[Name], p.Seats, COUNT(pgers.Id) AS [Passengers Count] FROM Planes AS p
LEFT JOIN Flights AS f ON f.PlaneId = p.Id
LEFT JOIN Tickets AS t ON t.FlightId = f.Id
LEFT JOIN Passengers AS pgers ON pgers.Id = t.PassengerId
GROUP BY p.[Name], p.Seats
ORDER BY [Passengers Count] DESC, p.[Name], p.Seats

GO

CREATE FUNCTION udf_CalculateTickets(@origin VARCHAR(50), @destination VARCHAR(50), @peopleCount INT)
RETURNS VARCHAR(50)
AS
BEGIN
	IF (@peopleCount <= 0)
	BEGIN
		RETURN 'Invalid people count!'
	END
	DECLARE @flightId INT = (SELECT TOP(1) Id FROM Flights
								WHERE Destination = @destination AND Origin = @origin)
	IF (@flightId IS NULL)
	BEGIN
		RETURN 'Invalid flight!'
	END
	DECLARE @ticketPrice DECIMAL(15, 2) = (SELECT T.Price FROM Tickets AS t
											WHERE t.FlightId = @flightId)
	DECLARE @totalPrice DECIMAL(24, 2) = @ticketPrice * @peopleCount
	RETURN CONCAT('Total price ', @totalPrice);
END

GO

SELECT dbo.udf_CalculateTickets ('Invalid', 'Rancabolang', 33)

GO

CREATE PROCEDURE usp_CancelFlights 
AS
BEGIN
	UPDATE Flights SET DepartureTime = NULL, ArrivalTime = NULL
	WHERE DepartureTime < ArrivalTime
END

EXEC usp_CancelFlights