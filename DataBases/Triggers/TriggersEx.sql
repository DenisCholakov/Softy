USE Bank

GO

CREATE TABLE Logs (
LogId INT PRIMARY KEY IDENTITY,
AccountId INT NOT NULL REFERENCES Accounts(Id),
OldSum MONEY NOT NULL,
NewSum MONEY NOT NULL
)

GO

CREATE TRIGGER InsertNewEntryIntoLogs
ON Accounts
AFTER UPDATE 
AS
	INSERT INTO Logs (AccountId, OldSum, NewSum)
	VALUES ((SELECT id FROM inserted),
			(SELECT Balance FROM deleted),
			(SELECT Balance FROM inserted))

CREATE TABLE NotificationEmails (
Id INT PRIMARY KEY IDENTITY,
Recipient INT NOT NULL REFERENCES Accounts(Id),
[Subject] VARCHAR(200) NOT NULL,
Body VARCHAR(800) NOT NULL
)

GO

CREATE TRIGGER EmailNotification
ON Logs
AFTER INSERT
AS
	INSERT INTO NotificationEmails (Recipient, [Subject], Body)
	VALUES (inserted.AccountId, CONCAT('Balance change for account: ', inserted.AccountId),
	CONCAT('On ', FORMAT(inserted)))