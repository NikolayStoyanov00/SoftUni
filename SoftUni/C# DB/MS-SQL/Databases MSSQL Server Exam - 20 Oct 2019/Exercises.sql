CREATE DATABASE [Service]
USE [Service]

--Section 1. DDL (30 pts)
CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY,
	Username NVARCHAR(30) NOT NULL UNIQUE,
	[Password] NVARCHAR(50) NOT NULL,
	[Name] NVARCHAR(50),
	Birthdate DATETIME,
	Age INT CHECK (Age >= 14 AND Age <= 110),
	Email NVARCHAR(50) NOT NULL
)

CREATE TABLE Departments (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(25),
	LastName NVARCHAR(25),
	Birthdate DATETIME,
	Age INT CHECK(Age >= 18 AND Age <= 110),
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE Categories (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	DepartmentId INT NOT NULL FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE [Status] (
	Id INT PRIMARY KEY IDENTITY,
	[Label] NVARCHAR(30) NOT NULL
)

CREATE TABLE Reports (
	Id INT PRIMARY KEY IDENTITY,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	StatusId INT NOT NULL FOREIGN KEY REFERENCES [Status](Id),
	OpenDate DATETIME NOT NULL,
	CloseDate DATETIME,
	[Description] NVARCHAR(200) NOT NULL,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
)

--Section 2. DML (10 pts)
INSERT INTO Employees (FirstName, LastName, Birthdate, DepartmentId)
VALUES
('Marlo', 'O''Malley', '1958-9-21', 1),
('Niki', 'Stanaghan', '1969-11-26', 4),
('Ayrton', 'Senna', '1960-03-21', 9),
('Ronnie', 'Peterson', '1944-02-14', 9),
('Giovanna', 'Amati', '1959-07-20', 5)

INSERT INTO Reports (CategoryId, StatusId, OpenDate, CloseDate, 
[Description], UserId, EmployeeId)
VALUES
(1, 1, '2017-04-13', NULL, 'Stuck Road on Str.133', 6, 2),
(6, 3, '2015-09-05', '2015-12-06', 'Charity trail running', 3, 5),
(14, 2, '2015-09-07', NULL, 'Falling bricks on Str.58', 5, 2),
(4, 3, '2017-07-03', '2017-07-06', 'Cut off streetlight on Str.11', 1, 1)

--03. Update
UPDATE Reports
SET CloseDate = GETDATE()
WHERE CloseDate IS NULL

--04. Delete
DELETE FROM Reports
WHERE StatusId = 4

--Section 3. Querying (40 pts)

--05. Unassigned Reports
SELECT 
r.[Description], 
FORMAT(r.OpenDate, 'dd-MM-yyyy') AS 'OpenDate' 
FROM Reports AS r
WHERE EmployeeId IS NULL
ORDER BY  r.OpenDate, [Description]

--06. Reports & Categories
SELECT
r.[Description],
C.Name
FROM Reports AS r
JOIN Categories AS c ON r.CategoryId = c.Id
WHERE r.CategoryId IS NOT NULL
ORDER BY r.Description, c.Name

--07. Most Reported Category
SELECT TOP 5
c.Name,
COUNT(r.Id) AS 'ReportsNumber'
FROM Categories AS c
JOIN Reports AS r ON r.CategoryId = c.Id
GROUP BY c.Name
ORDER BY COUNT(r.Id) DESC, c.Name 

--08. Birthday Report
SELECT
u.Username AS 'Username',
c.Name AS 'CategoryName'
FROM Reports AS r
JOIN Users AS u ON r.UserId = u.Id
JOIN Categories AS c ON r.CategoryId = c.Id
WHERE MONTH(u.Birthdate) = MONTH(r.OpenDate) 
AND DAY(u.Birthdate) = DAY(r.Opendate)
ORDER BY u.Username, c.Name

--09. User per Employee
SELECT 
FirstName + ' '  + LastName AS 'FullName',
COUNT(u.Id) AS 'UsersCount'
FROM 
Employees AS e
LEFT JOIN Reports AS r ON r.EmployeeId = e.Id
LEFT JOIN Users AS u ON r.UserId = u.Id
GROUP BY e.FirstName, e.LastName
ORDER BY [UsersCount] DESC, [FullName]

--10. Full Info
SELECT 
IIF(e.FirstName + ' ' + e.LastName IS NOT NULL, e.FirstName + ' ' + e.LastName, 'None') AS 'Employee',
IIF(d.Name IS NOT NULL, d.Name, 'None') AS 'Department',
IIF(c.Name IS NOT NULL, c.Name, 'None') AS 'Category',
IIF(r.Description IS NOT NULL, r.Description, 'None') AS 'Description',
IIF(r.OpenDate IS NOT NULL, FORMAT(r.OpenDate, 'dd.MM.yyyy'), 'None') AS 'OpenDate',
IIF(s.Label IS NOT NULL, s.Label, 'None') AS 'Status',
IIF(u.Name IS NOT NULL, u.Name, 'None') AS 'User'
FROM Reports AS r
LEFT JOIN Employees AS e ON r.EmployeeId = e.Id
LEFT JOIN Departments AS d ON d.Id = e.DepartmentId
LEFT JOIN Categories AS c ON c.Id = r.CategoryId
LEFT JOIN Status AS s ON s.Id = r.StatusId
LEFT JOIN Users AS u ON u.Id = r.UserId
ORDER BY 
e.FirstName DESC,
e.LastName DESC, 
d.Name, c.Name, 
r.Description,
r.OpenDate,
s.Label,
u.Name

--11. Hours to complete
CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT AS
 BEGIN
	IF (@StartDate IS NULL OR @EndDate IS NULL)
	BEGIN
	 RETURN 0
	END
 RETURN DATEDIFF(HOUR, @StartDate, @EndDate)
 END

--12. Assign Employee
CREATE PROC usp_AssignEmployeeToReport(@EmployeeId INT, @ReportId INT)
AS
DECLARE @Valid BIT
DECLARE @employeeDepartmentId INT
DECLARE @categoryDepartmentId INT

SELECT @employeeDepartmentId = (SELECT 
e.DepartmentId
FROM Employees AS e
WHERE e.Id = @EmployeeId)

SELECT @categoryDepartmentId = (SELECT
c.DepartmentId
FROM Reports AS r
JOIN Categories AS c ON c.Id = r.CategoryId
WHERE r.Id = @ReportId)

 IF (@employeeDepartmentId = @categoryDepartmentId)
  BEGIN
   UPDATE Reports
   SET EmployeeId = @EmployeeId
   WHERE  Id = @ReportId
  END
 ELSE
  BEGIN
   THROW 51000, 'Employee doesn''t belong to the appropriate department!', 1;
  END

 EXEC usp_AssignEmployeeToReport 17, 2 
