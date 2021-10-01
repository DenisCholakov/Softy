CREATE DATABASE Minions

USE Minions

CREATE TABLE Minions 
(
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(60) NOT NULL,
	Age INT
)


CREATE TABLE Towns
(
	Id INT PRIMARY KEY,
	[Name] NVARCHAR(60) NOT NULL
)

ALTER TABLE Minions ADD 
TownId INT

ALTER TABLE Minions ADD
FOREIGN KEY (TownId) REFERENCES Towns(Id)

INSERT INTO Towns (Id, [Name]) VALUES
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')


INSERT INTO Minions (Id, [Name], Age, TownId) VALUES
(1, 'Kevin', 22, 1),
(2, 'Bob', 15, 3),
(3, 'Steward', NULL, 2)

DELETE FROM Minions

DROP TABLE Minions

DROP TABLE Towns

CREATE TABLE People
(
	Id INT IDENTITY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height FLOAT(2),
	[Weight] FLOAT(2),
	Gender BIT NOT NULL,
	Birthdate DATETIME2 NOT NULL,
	Biography NVARCHAR(MAX)
)

ALTER TABLE People 
ADD CONSTRAINT PK_Person PRIMARY KEY (Id)

INSERT INTO People ([Name], Gender, Birthdate) VALUES
('Denis', 1, '01-08-1994'),
('Denis', 1, '01-08-1994'),
('Denis', 1, '01-08-1994'),
('Denis', 1, '01-08-1994'),
('Denis', 1, '01-08-1994')

CREATE TABLE Users
(
	Id INT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARCHAR(MAX),
	LastLoginTime DATETIME,
	IsDeleted BIT
)

INSERT INTO Users (Username, [Password], ProfilePicture, LastLoginTime, IsDeleted) VALUES
('kdjfgshsdf', 'asdfcdsaf', 'https://images.askmen.com/1080x540/2016/01/25-021526-facebook_profile_picture_affects_chances_of_getting_hired.jpg',
			'1/12/2021', 0),
('kdjfgshsdf', 'asdfcdsaf', 'https://images.askmen.com/1080x540/2016/01/25-021526-facebook_profile_picture_affects_chances_of_getting_hired.jpg',
			'1/12/2021', 0),
('kdjfgshsdf', 'asdfcdsaf', 'https://images.askmen.com/1080x540/2016/01/25-021526-facebook_profile_picture_affects_chances_of_getting_hired.jpg',
			'1/12/2021', 0),
('kdjfgshsdf', 'asdfcdsaf', 'https://images.askmen.com/1080x540/2016/01/25-021526-facebook_profile_picture_affects_chances_of_getting_hired.jpg',
			'1/12/2021', 0),
('kdjfgshsdf', 'asdfcdsaf', 'https://images.askmen.com/1080x540/2016/01/25-021526-facebook_profile_picture_affects_chances_of_getting_hired.jpg',
			'1/12/2021', 0)

ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC0784DE5F71

ALTER TABLE Users
ADD CONSTRAINT PK_IdUsername PRIMARY KEY (Id, Username)

ALTER TABLE Users
DROP CONSTRAINT PK_IdUsername

ALTER TABLE Users
ADD CONSTRAINT PK_Id PRIMARY KEY (Id)

ALTER TABLE Users
ADD CONSTRAINT CH_PasswordIsAtLeastFiveSymbols CHECK (LEN([Password]) > 5)

ALTER TABLE Users
ADD CONSTRAINT DF_LastLoginTime DEFAULT GETDATE() FOR LastLoginTime

ALTER TABLE Users
ADD CONSTRAINT CH_UsernameIsAtLeastThreeSymbols CHECK (LEN(Username) > 3)

CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors
(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(60) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Genres
(
	Id INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(60) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Categories
(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(60) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Movies
(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(60),
	DirectorId INT NOT NULL REFERENCES Directors(Id),
	CopyRightYear DATETIME2 NOT NULL,
	[Length] TIME NOT NULL,
	GenreId INT NOT NULL REFERENCES Genres(Id),
	CategoryId INT NOT NULL REFERENCES Categories(Id),
	Rating FLOAT(1),
	Notes NVARCHAR(MAX)
)

INSERT INTO Directors (DirectorName) VALUES
('Denis Cholakoc'),
('Martin Simov'),
('Pavel Dimov'),
('dencho'),
('sdsad')

INSERT INTO Genres (GenreName) VALUES
('Action'),
('Comedy'),
('Romatic'),
('Drama'),
('Scary')

INSERT INTO Categories(CategoryName) VALUES
('Long'),
('Short'),
('Interesting'),
('Stupid'),
('sakd')

INSERT INTO Movies (Title, DirectorId, CopyRightYear, [Length], GenreId, CategoryId, Rating) VALUES
('Fast and Furious', 1, '1994', '2:20:00', 2, 3, 5.5),
('Scary movie', 2, '2000', '5:20:00', 2, 3, 5.5),
('Need for Speed', 3, '2010', '1:20:00', 2, 3, 5.5),
('Suits', 4, '2020', '0:40:00', 2, 3, 5.5),
('Arrow', 5, '1734', '0:20:00', 2, 3, 5.5)

CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories
(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(60) NOT NULL,
	DailyRate DECIMAL(8, 2) NOT NULL,
	WeeklyRate DECIMAL(12, 2) NOT NULL,
	MonthlyRate DECIMAL(18, 2) NOT NULL,
	WeekendRate DECIMAL(10, 2) NOT NULL
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
('Luxury', 42, 260, 700, 70),
('Normal', 22, 120, 250, 30),
('Sport', 32, 220, 400, 50)

CREATE TABLE Cars
(
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber NVARCHAR(10) NOT NULL,
	Manufacturer NVARCHAR(30) NOT NULL,
	Model NVARCHAR(30) NOT NULL,
	CarYear DATETIME2 NOT NULL,
	CategoryId INT NOT NULL REFERENCES Categories(Id),
	Doors INT,
	Picture VARCHAR(MAX),
	Condition NVARCHAR(60),
	Avaliable BIT NOT NULL
)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Avaliable) VALUES
('CB8901KX', 'SEAT', 'Toledo', '2003', 3, 1),
('CB7292KX', 'AUDI', 'A6', '2007', 3, 1),
('CB6666KX', 'AUDI', 'RS6', '2020', 3, 1)

CREATE TABLE Employees
(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Employees (FirstName, LastName, Title) VALUES
('Denis', 'Cholakov', 'CEO'),
('Martin', 'Simov', 'CEO2'),
('Pavel', 'Dimov', 'CEO3')

CREATE TABLE Customers
(
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenceNumber NVARCHAR(50) NOT NULL,
	FullName NVARCHAR(300) NOT NULL,
	[Address] NVARCHAR(100),
	City NVARCHAR(50),
	ZIPCode NVARCHAR(10),
	Notes NVARCHAR(MAX)
)

INSERT INTO Customers (DriverLicenceNumber, FullName) VALUES
('safasf', 'dsadas'),
('jhlkl', 'nbmn'),
('erwtr', 'yrujh')

CREATE TABLE RentalOrders
(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT NOT NULL REFERENCES Employees(Id),
	CustomerId INT NOT NULL REFERENCES Customers(Id),
	CarId INT NOT NULL REFERENCES Cars(Id),
	TankLevel FLOAT(2) NOT NULL,
	KilometrageStart DECIMAL(18, 3) NOT NULL,
	KilometrageEnd DECIMAL(18, 3),
	TotalKilometrage DECIMAL(18, 3),
	StartDate DATETIME2 NOT NULL,
	EndDate DATETIME2,
	TotalDays DATETIME2,
	RateApplied DECIMAL(18, 2),
	TaxRate DECIMAL(18, 2),
	OrderStatus NVARCHAR(20),
	Notes NVARCHAR(MAX)
)

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, 
				KilometrageStart, StartDate) VALUES
(1, 2, 3, 52.35, 65465.321, '1-2-2020'),
(2, 3, 1, 52.35, 65465.321, '1-2-2020'),
(3, 1, 2, 52.35, 65465.321, '1-2-2020')

CREATE DATABASE Hotel

USE Hotel


