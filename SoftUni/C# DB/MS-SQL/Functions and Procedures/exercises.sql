--1. Employees with Salary Above 35000
CREATE PROC usp_GetEmployeesSalaryAbove35000
AS
SELECT 
FirstName AS 'First Name',
LastName AS 'Last Name'
FROM Employees
WHERE Salary > 35000

--02. Employees with Salary Above Number
CREATE PROC usp_GetEmployeesSalaryAboveNumber (@Number DECIMAL(18, 4))
AS 
SELECT 
FirstName AS 'First Name',
LastName AS 'Last Name'
FROM Employees
WHERE Salary >= @Number

EXEC usp_GetEmployeesSalaryAboveNumber @Number = 48100

--03. Town Names Starting With
CREATE PROC usp_GetTownsStartingWith (@Word NVARCHAR(30))
AS
SELECT
[Name]
FROM Towns
WHERE [Name] LIKE @Word + '%'

EXEC usp_GetTownsStartingWith @Word = 'b'

--04. Employees from Town
CREATE PROC usp_GetEmployeesFromTown (@TownName NVARCHAR(30))
AS
SELECT
e.FirstName AS 'First Name',
e.LastName AS 'Last Name'
FROM Employees AS e
JOIN Addresses AS a ON e.AddressID = a.AddressID
JOIN Towns AS t ON a.TownID = t.TownID
WHERE t.Name = @TownName

EXEC usp_GetEmployeesFromTown @TownName = 'Sofia'

--05. Salary Level Function
CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(10) 
AS
BEGIN
DECLARE @salaryLevel NVARCHAR(10)
IF (@salary < 30000)
	BEGIN
		SET @salaryLevel = 'Low'
	END
ELSE IF (@salary >= 30000 AND @salary <= 50000)
	BEGIN
		SET @salaryLevel = 'Average'
	END
ELSE
	BEGIN
		SET @salaryLevel = 'High'
	END
RETURN @salaryLevel
END

SELECT Salary, dbo.ufn_GetSalaryLevel(Salary) AS 'Salary Level'
FROM Employees

--06. Employees by Salary Level
CREATE PROC usp_EmployeesBySalaryLevel (@salaryLevel NVARCHAR(10))
AS
SELECT 
FirstName AS 'First Name',
LastName AS 'Last Name'
FROM Employees
WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel

EXEC usp_EmployeesBySalaryLevel @salaryLevel = 'High'

--07. Define Function
CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(30), @word NVARCHAR(30))
RETURNS BIT
AS
BEGIN
	DECLARE @result BIT = 1;
	DECLARE @counter INT = 1;
	DECLARE @wordLen INT = LEN(@word);
	WHILE (@counter <= @wordLen)
	 BEGIN
	 DECLARE @currentLetter CHAR(1) = SUBSTRING(@word, @counter, 1);
	 IF(@setOfLetters NOT LIKE '%' + @currentLetter + '%')
	 BEGIN
		SET @result = 0
	 END
	 SET @counter += 1
	END
RETURN @result
END

SELECT dbo.ufn_IsWordComprised('dawdawdwadsodwfi', 'Sofia') AS 'Result'

--09. Find Full Name
CREATE PROC usp_GetHoldersFullName 
AS
SELECT 
FirstName + ' ' + LastName AS 'Full Name'
FROM AccountHolders

--10. People with Balance Higher Than
CREATE PROC usp_GetHoldersWithBalanceHigherThan (@number DECIMAL(10, 2))
AS
SELECT 
ah.FirstName AS 'First Name',
ah.LastName AS 'Last Name'
FROM AccountHolders AS ah
JOIN Accounts AS a ON ah.Id = a.AccountHolderId
GROUP BY ah.FirstName, ah.LastName
HAVING SUM(a.Balance) > @number
ORDER BY ah.FirstName, ah.LastName

--11. Future Value Function
CREATE FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(18, 4), @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS DECIMAL (18, 4)
AS
BEGIN
	RETURN @sum * POWER((1 + @yearlyInterestRate), @numberOfYears)
END

--12. Calculating Interest
CREATE PROC usp_CalculateFutureValueForAccount (@accountId INT, @interestRate FLOAT)
AS 
SELECT 
a.Id AS 'Account Id',
ah.FirstName AS 'First Name', 
ah.LastName AS 'Last Name',
a.Balance AS 'Current Balance',
dbo.ufn_CalculateFutureValue(a.Balance, 0.1, 5) AS 'Balance in 5 years'
FROM Accounts AS a
JOIN AccountHolders AS ah ON a.AccountHolderId = ah.Id
WHERE @accountId = a.Id

EXEC usp_CalculateFutureValueForAccount @accountId = 1, @interestRate = 0.1

--13. *Cash in User Games Odd Rows
CREATE TABLE Logs (
	LogId INT PRIMARY KEY,
	AccountId INT,
	OldSum DECIMAL(10, 2),
	NewSum DECIMAL(10, 2)
)

CREATE TRIGGER InsertNewEntryIntoLogs
  ON Accounts
  AFTER UPDATE
AS
  INSERT INTO Logs
  VALUES (
    (SELECT Id
     FROM inserted),
    (SELECT Balance
     FROM deleted),
    (SELECT Balance
     FROM inserted)
  )

--15. Create Table Emails
CREATE TABLE NotificationEmails (
  Id INT PRIMARY KEY IDENTITY,
  Recipient INT,
  Subject NVARCHAR(MAX),
  Body NVARCHAR(MAX)
)

CREATE TRIGGER CreateNewNotificationEmail
  ON Logs
  AFTER INSERT
AS
  BEGIN
    INSERT INTO NotificationEmails
    VALUES (
      (SELECT AccountId
       FROM inserted),
      (CONCAT('Balance change for account: ', (SELECT AccountId
                                               FROM inserted))),
      (CONCAT('On ', (SELECT GETDATE()
                      FROM inserted), 'your balance was changed from ', (SELECT OldSum
                                                                         FROM inserted), 'to ', (SELECT NewSum
                                                                                                 FROM inserted), '.'))
    )
  END

--16. Deposit Money
CREATE PROC usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(18, 4))
AS
BEGIN TRANSACTION
UPDATE Accounts
SET Balance += @MoneyAmount
WHERE Id = @AccountId
COMMIT

EXEC usp_DepositMoney @AccountId = 1, @MoneyAmount = 10

--17. Withdraw Money Procedure
CREATE PROC usp_WithdrawMoney (@AccountId INT, @MoneyAmount DECIMAL(18, 4))
AS 
BEGIN TRANSACTION
UPDATE Accounts
SET Balance -= @MoneyAmount
WHERE Id = @AccountId
COMMIT

EXEC usp_WithdrawMoney @AccountId = 5, @MoneyAmount = 25

--18. Money Transfer
CREATE PROC usp_TransferMoney(
@SenderId INT, 
@ReceiverId INT, 
@Amount DECIMAL(18, 4))
AS
EXEC usp_WithdrawMoney @AccountId = @SenderId, @MoneyAmount = @Amount
EXEC usp_DepositMoney @AccountId = @ReceiverId, @MoneyAmount = @Amount

EXEC usp_TransferMoney 5, 1, 5000
SELECT * FROM Accounts
WHERE Id = 5

--21. Employees with Three Projects
USE SoftUni

CREATE PROC usp_AssignProject(@employeeId INT, @projectID INT)
AS
 BEGIN	
  BEGIN TRAN
  INSERT INTO EmployeesProjects
  VALUES (@employeeId, @projectID)
  IF (SELECT COUNT(ProjectId)
	FROM EmployeesProjects
	WHERE EmployeeID = @employeeId) > 3
   BEGIN
	RAISERROR ('The employee has too many projects!', 16, 1)
	ROLLBACK
	RETURN
   END
  COMMIT
 END

--22. Delete Employees
CREATE TABLE Deleted_Employees(
	EmployeeId INT PRIMARY KEY,
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	MiddleName NVARCHAR(50),
	JobTitle NVARCHAR(50),
	DepartmentId INT,
	Salary DECIMAL(10, 4)
)

CREATE TRIGGER InsertNewEntryIntoDeletedEmployees
  ON Employees
  AFTER DELETE
AS
  BEGIN
   INSERT INTO Deleted_Employees
      SELECT
        FirstName,
        LastName,
        MiddleName,
        JobTitle,
        DepartmentID,
        Salary
      FROM deleted
   END

DELETE FROM EmployeesProjects WHERE EmployeeID = 1
DELETE FROM Employees WHERE EmployeeID = 1

  SELECT * FROM Deleted_Employees