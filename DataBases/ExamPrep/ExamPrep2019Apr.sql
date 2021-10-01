CREATE DATABASE Airport

GO

USE Airport

GO

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
PlaneId INT NOT NULL REFERENCES Planes(Id)
)

CREATE TABLE Passengers (
Id INT PRIMARY KEY IDENTITY,
FirstName VARCHAR(30) NOT NULL,
LastName VARCHAR(30) NOT NULL,
Age INT NOT NULL,
[Address] VARCHAR(30) NOT NULL,
PassportId CHAR(11) NOT NULL
)

CREATE TABLE LuggageTypes (
Id INT PRIMARY KEY IDENTITY,
[Type] VARCHAR(30) NOT NULL
)

CREATE TABLE Luggages (
Id INT PRIMARY KEY IDENTITY,
LuggageTypeId INT NOT NULL REFERENCES LuggageTypes(Id),
PassengerId INT NOT NULL REFERENCES Passengers(Id)
)

CREATE TABLE Tickets (
Id INT PRIMARY KEY IDENTITY,
PassengerId INT NOT NULL REFERENCES Passengers(Id),
FlightId INT NOT NULL REFERENCES Flights(Id),
LuggageId INT NOT NULL REFERENCES Luggages(Id),
Price DECIMAL(18, 2) NOT NULL
)

INSERT INTO Planes ([Name], Seats, [Range])
VALUES  ('Airbus 336', 112, 5132),
		('Airbus 330', 432, 5325),
		('Boeing 369', 231, 2355),
		('Stelt 297', 254, 2143),
		('Boeing 338', 165, 5111),
		('Airbus 558', 387, 1342),
		('Boeing 128', 345, 5541)

INSERT INTO LuggageTypes ([Type])
VALUES  ('Crossbody Bag'),
		('School Backpack'),
		('Shoulder Bag')

UPDATE Tickets SET Price += Price * 0.13
WHERE FlightId IN (SELECT Id FROM Flights
					WHERE Destination = 'Carlsbad')

DELETE FROM Tickets
WHERE FlightId IN (SELECT Id FROM Flights
					WHERE Destination = 'Ayn Halagim')

DELETE FROM Flights
WHERE Destination = 'Ayn Halagim'


SELECT Origin, Destination FROM Flights
ORDER BY Origin, Destination

SELECT Id, [Name], Seats, [Range] FROM Planes
WHERE [Name] LIKE '%tr%'
ORDER BY Id, [Name], Seats, [Range]

SELECT FlightId, SUM(Price) AS Price FROM Tickets
GROUP BY FlightId
ORDER BY Price DESC, FlightId

SELECT TOP(10) p.FirstName, p.LastName, t.Price FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
ORDER BY t.Price DESC, FirstName, LastName

SELECT lt.[Type], COUNT(p.Id) AS MostUsedLuggage FROM LuggageTypes AS lt
LEFT JOIN Luggages AS l ON l.LuggageTypeId = lt.Id
LEFT JOIN Passengers AS p ON p.Id = l.PassengerId
GROUP BY lt.[Type]
ORDER BY MostUsedLuggage DESC, lt.[Type]

SELECT p.FirstName + ' ' + p.LastName AS [Full Name],
		f.Origin, f.Destination FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON f.Id = t.FlightId
ORDER BY [Full Name], f.Origin, f.Destination

SELECT p.FirstName, p.LastName, p.Age FROM Passengers AS p
LEFT JOIN Tickets AS t ON t.PassengerId = p.Id
WHERE t.FlightId IS NULL
ORDER BY p.Age DESC, p.FirstName, p.LastName

SELECT p.PassportId, p.[Address] FROM Passengers AS p
LEFT JOIN Luggages AS l ON l.PassengerId = p.Id
WHERE l.Id IS NULL
ORDER BY p.PassportId, p.[Address]

SELECT p.FirstName, p.LastName, COUNT(t.FlightId) AS [Total Trips] FROM Passengers AS p
LEFT JOIN Tickets AS t ON t.PassengerId = p.Id
GROUP BY p.Id, p.FirstName, p.LastName
ORDER BY [Total Trips] DESC, p.FirstName, p.LastName

SELECT p.FirstName + ' ' + p.LastName AS [Full Name],
		pl.[Name] AS [Plane Name],
		f.Origin + ' - ' + f.Destination AS Trip,
		lt.[Type] AS [Luggage Type] FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON f.Id = t.FlightId
JOIN Planes AS pl ON pl.Id = f.PlaneId
JOIN Luggages AS l ON l.Id = t.LuggageId
JOIN LuggageTypes AS lt ON lt.Id = l.LuggageTypeId
ORDER BY [Full Name], pl.[Name], f.Origin, f.Destination, [Luggage Type]

SELECT FirstName, 
		LastName, 
		Destination,
		Price FROM(SELECT p.FirstName, p.LastName, f.Destination, t.Price,
							DENSE_RANK() OVER (PARTITION BY p.Id ORDER BY t.Price DESC) AS PRank FROM Passengers AS p
					JOIN Tickets AS t ON t.PassengerId = p.Id
					JOIN Flights AS f ON f.Id = t.FlightId) AS PriceRanking
WHERE PRank = 1
ORDER BY Price DESC, FirstName, LastName, Destination

SELECT Destination, COUNT(t.Id) AS FilesCount FROM Flights AS f
LEFT JOIN Tickets AS t ON t.FlightId = f.Id
GROUP BY Destination
ORDER BY FilesCount DESC, Destination

SELECT p.[Name], p.Seats, COUNT(t.Id) AS [Passengers Count] FROM Planes AS p
LEFT JOIN Flights AS f ON f.PlaneId = p.Id
LEFT JOIN Tickets AS t ON t.FlightId = f.Id
GROUP BY p.Id, p.[Name], p.Seats
ORDER BY [Passengers Count] DESC, p.[Name], p.Seats

--programability

GO

CREATE FUNCTION udf_CalculateTickets(@origin VARCHAR(50), @destination VARCHAR(50), @peopleCount INT)
RETURNS VARCHAR(MAX)
AS
BEGIN
	IF (@peopleCount <= 0)
		RETURN 'Invalid people count!'
	DECLARE @flightId INT = (SELECT TOP(1) Id FROM Flights
							WHERE Origin = @origin AND Destination = @destination)
	IF (@flightId IS NULL)
		RETURN 'Invalid flight!'
	DECLARE @ticketPrice DECIMAL(18, 2) = (SELECT Price FROM Tickets AS t
											WHERE FlightId = @flightId)
	DECLARE @totalPrice DECIMAL(24, 2) = @ticketPrice * @peopleCount
	RETURN CONCAT('Total price ', @totalPrice)
END

GO

SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', 33)