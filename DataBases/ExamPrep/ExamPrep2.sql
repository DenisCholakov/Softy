DROP DATABASE School

CREATE DATABASE School

GO

USE School

GO

CREATE TABLE Students (
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(30) NOT NULL,
MiddleName NVARCHAR(25),
LastName NVARCHAR(30) NOT NULL,
Age INT CHECK(Age > 0) NOT NULL,
[Address] NVARCHAR(50),
Phone NVARCHAR(10)
)

CREATE TABLE Subjects (
Id INT PRIMARY KEY IDENTITY,
[Name] NVARCHAR(20) NOT NULL,
Lessons INT CHECK(Lessons > 0) NOT NULL
)

CREATE TABLE StudentsSubjects (
Id INT PRIMARY KEY IDENTITY,
StudentId INT REFERENCES Students(Id) NOT NULL,
SubjectId INT REFERENCES Subjects(Id) NOT NULL,
Grade DECIMAL(3, 2) CHECK(Grade BETWEEN 2 AND 6) NOT NULL
)

CREATE TABLE Exams (
Id INT PRIMARY KEY IDENTITY,
[Date] DATETIME2,
SubjectId INT REFERENCES Subjects(Id) NOT NULL
)

CREATE TABLE StudentsExams (
StudentId INT REFERENCES Students(Id) NOT NULL,
ExamId INT REFERENCES Exams(Id) NOT NULL,
Grade DECIMAL(3, 2) CHECK(Grade BETWEEN 2 AND 6),

CONSTRAINT PK_StudentsExams PRIMARY KEY (StudentId, ExamId)
)

CREATE TABLE Teachers (
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(20) NOT NULL,
LastName NVARCHAR(20) NOT NULL,
[Address] NVARCHAR(20) NOT NULL,
Phone VARCHAR(10),
SubjectId INT REFERENCES Subjects(Id) NOT NULL
)

CREATE TABLE StudentsTeachers (
StudentId INT REFERENCES Students(Id) NOT NULL,
TeacherId INT REFERENCES Teachers(Id) NOT NULL,

CONSTRAINT PK_StudentsTeachers PRIMARY KEY (StudentId, TeacherId)
)

GO

INSERT INTO Teachers (FirstName, LastName, [Address], Phone, SubjectId)
VALUES ('Ruthanne', 'Bamb',	'84948 Mesta Junction',	3105500146,	6),
		('Gerrard',	'Lowin', '370 Talisman Plaza', 3324874824,	2),
		('Merrile',	'Lambdin', '81 Dahle Plaza', 4373065154, 5),
		('Bert', 'Ivie', '2 Gateway Circle', 4409584510, 4)

INSERT INTO Subjects ([Name], Lessons)
VALUES ('Geometry', 12),
		('Health', 10),
		('Drama', 7),
		('Sports', 9)

UPDATE StudentsSubjects SET Grade = 6.00
	WHERE SubjectId IN (1, 2) AND Grade >= 5.50

DELETE FROM StudentsTeachers
WHERE TeacherId IN (SELECT Id FROM Teachers
					WHERE Phone LIKE '%72%')

DELETE FROM Teachers
WHERE Phone LIKE '%72%'

SELECT FirstName, LastName, Age FROM Students
WHERE Age >= 12
ORDER BY FirstName, LastName

SELECT FirstName, LastName, COUNT(*) FROM Students AS s
LEFT JOIN StudentsTeachers AS st ON st.StudentId = s.Id
GROUP BY s.FirstName, s.LastName

SELECT CONCAT(s.FirstName, ' ', s.LastName) AS [Full Name] FROM Students AS s
LEFT JOIN StudentsExams AS se ON se.StudentId = s.Id
WHERE se.Grade IS NULL
ORDER BY [Full Name]

SELECT TOP(10) s.FirstName, s.LastName, CAST(AVG(se.Grade) AS DECIMAL(3, 2)) AS Grade FROM Students AS s
JOIN StudentsExams AS se ON se.StudentId = s.Id
GROUP BY s.FirstName, s.LastName
ORDER BY Grade DESC, s.FirstName, s.LastName

SELECT CONCAT(s.FirstName + ' ', s.MiddleName + ' ', s.LastName) AS [Full Name] FROM Students AS s
LEFT JOIN StudentsSubjects AS sj ON sj.StudentId = s.Id
WHERE sj.SubjectId IS NULL
ORDER BY [Full Name]

SELECT sbj.[Name], AVG(sj.Grade) AS AverageGrade FROM Subjects AS sbj
JOIN StudentsSubjects AS sj ON sj.SubjectId = sbj.Id
GROUP BY sbj.[Name], sbj.Id
ORDER BY sbj.Id

GO

CREATE OR ALTER FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL (3, 2))
RETURNS VARCHAR(800)
AS
BEGIN
	DECLARE @gradesCount INT = (SELECT COUNT(se.Grade) FROM Students AS s
								JOIN StudentsExams AS se ON se.StudentId = s.Id
								WHERE s.Id = @studentId AND se.Grade BETWEEN @grade AND (@grade + 0.5)
								GROUP BY s.Id)

	IF (@grade > 6.00)
	BEGIN
		RETURN 'Grade cannot be above 6.00!'
	END

	IF (@gradesCount IS NULL)
	BEGIN
		RETURN 'The student with provided id does not exist in the school!'
	END

	DECLARE @firstName NVARCHAR(50) = (SELECT FirstName FROM Students
										WHERE Id = @studentId)

	RETURN CONCAT('You have to update ', @gradesCount, ' grades for the student ', @firstName)
END

GO

SELECT dbo.udf_ExamGradesToUpdate (121, 6.20)

GO

CREATE OR ALTER PROCEDURE usp_ExcludeFromSchool(@StudentId INT)
AS
BEGIN
	IF ((SELECT FirstName FROM Students
		WHERE Id = @StudentId) IS NULL)
	BEGIN
		RAISERROR('This school has no student with the provided id!', 16, 1)
		RETURN
	END

	DELETE FROM StudentsExams
	WHERE StudentId = @StudentId

	DELETE FROM StudentsTeachers
	WHERE StudentId = @StudentId

	DELETE FROM StudentsSubjects
	WHERE StudentId = @StudentId

	DELETE FROM Students
	WHERE Id = @StudentId

END


EXEC usp_ExcludeFromSchool 301

SELECT COUNT(*) FROM Students