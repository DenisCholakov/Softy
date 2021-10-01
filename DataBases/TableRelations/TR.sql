CREATE DATABASE Training

USE Training
/*
CREATE TABLE Passports(
	PassportID INT PRIMARY KEY IDENTITY(100, 1) NOT NULL,
	PassportNumber NVARCHAR(50) NOT NULL,
)
*/

DROP TABLE Passports
DROP TABLE Persons

CREATE TABLE Passports(
	PassportID INT IDENTITY(101, 1) NOT NULL,
	PassportNumber NVARCHAR(30),
	PRIMARY KEY(PassportID)
)

INSERT INTO Passports(PassportNumber)
	VALUES 
			('N34FG21B'),
			('K65LO4R7'),
			('ZE657QP2')

CREATE TABLE Persons(
	PersonID INT PRIMARY KEY IDENTITY NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	Salary DECIMAL(15, 2) NOT NULL,
	PassportID INT FOREIGN KEY REFERENCES Passports(PassportID)  UNIQUE NOT NULL
)

INSERT INTO Persons(FirstName, Salary, PassportID)
	VALUES
			('Roberto', 43300.00, 102),
			('Tom', 56100.00, 103),
			('Yana', 60200.00, 101)

DROP TABLE Manufacturers

CREATE TABLE Manufacturers(
	ManufacturerID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(20) NOT NULL,
	EstablishedOn DATE NOT NULL,
	PRIMARY KEY (ManufacturerID)
)

INSERT INTO Manufacturers ([Name], EstablishedOn)
	VALUES
			('BMW', '03-07-1916'),
			('Tesla', '01-01-2003'),
			('Lada', '05-01-1966')

CREATE TABLE Models(
	ModelID INT IDENTITY(101, 1) NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	ManufacturerID INT FOREIGN KEY REFERENCES Manufacturers(ManufacturerID),
	CONSTRAINT PK_ModelID PRIMARY KEY (ModelID)
)

INSERT INTO Models([Name], ManufacturerID)
	VALUES
		('X1', 1),
		('i6', 1),
		('Model S', 2),
		('Model X', 2),
		('Model 3', 2),
		('Nova', 3)

DROP TABLE Students

CREATE TABLE Students(
	StudentID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	CONSTRAINT PK_StudentsID PRIMARY KEY (StudentID)
)

INSERT INTO Students([Name])
	VALUES
			('Mila'),                                
			('Toni'),
			('Ron')


CREATE TABLE Exams(
	ExamID INT IDENTITY(101, 1) NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	CONSTRAINT PK_ExamID PRIMARY KEY (ExamID)
)

SELECT * FROM Students
INSERT INTO Exams([Name])
	VALUES
			('SpringMVC'),                                
			('Neo4j'),
			('Oracle 11g')

CREATE TABLE StudentsExams(
	StudentID INT NOT NULL,
	ExamID INT NOT NULL,
	CONSTRAINT FK_StudentsID FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
	CONSTRAINT FK_ExamID FOREIGN KEY (ExamID) REFERENCES Exams(ExamID),
	CONSTRAINT PK_StudentIDExamID PRIMARY KEY (StudentID, ExamID)
)

INSERT INTO StudentsExams(StudentID, ExamID)
	VALUES
			(1, 101),
			(1, 102),
			(2, 101),
			(3, 103),
			(2, 102),
			(2, 103)

CREATE TABLE Teachers(
	TeacherID INT IDENTITY(101, 1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	ManagerID INT,
	CONSTRAINT PK_TeacherID PRIMARY KEY (TeacherID),
	CONSTRAINT FK_ManagerIDTeacherID FOREIGN KEY (ManagerID) 
			REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers([Name], ManagerID)
	VALUES
			('John', NULL),
			('Maya', 106),
			('Silvia', 106),
			('Ted', 105),
			('Mark', 101),
			('Greta', 101)

CREATE DATABASE Store

USE Store

CREATE TABLE Cities(
	CityID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Customers(
	CustomerID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	[BirthDay] DATE,
	[CityID] INT FOREIGN KEY REFERENCES Cities(CityID)
)

CREATE TABLE Orders(
	OrderID INT PRIMARY KEY IDENTITY(101, 1) NOT NULL,
	CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes(
	ItemTypeID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Items(
	ItemID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	IdetTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems(
	OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
	ItemID INT FOREIGN KEY REFERENCES Items(ItemID),
	CONSTRAINT PK_OrderIDItemID PRIMARY KEY (OrderID, ItemID)
)

CREATE DATABASE University

USE University

CREATE TABLE Majors(
	MajorID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50)
);

CREATE TABLE Students(
	StudentID INT PRIMARY KEY IDENTITY NOT NULL,
	StudentNumber NVARCHAR(30),
	StudentName NVARCHAR(50),
	MajorID INT FOREIGN KEY REFERENCES Majors(MajorID)
);

CREATE TABLE Payments(
	PaymentID INT PRIMARY KEY IDENTITY NOT NULL,
	PaymentDate DATETIME2 NOT NULL,
	PaymentAmount DECIMAL(15, 2) NOT NULL,
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID)
);

CREATE TABLE Subjects(
	SubjectID INT PRIMARY KEY IDENTITY NOT NULL,
	SubjectName NVARCHAR(50)
);

CREATE TABLE Agenda(
	StudentID INT NOT NULL,
	SubjectID INT NOT NULL,
	CONSTRAINT PK_Agenda_StudentID_SubjectID PRIMARY KEY (StudentID, SubjectID),
	CONSTRAINT FK_Agenda_Students FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
	CONSTRAINT FK_Agenda_Subjects FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
);

USE Geography;

SELECT * FROM Peaks;


SELECT m.MountainRange, p.PeakName, p.Elevation FROM Mountains AS m
	JOIN Peaks AS p ON m.Id = p.MountainId
	WHERE m.MountainRange = 'Rila'
	ORDER BY p.Elevation DESC;