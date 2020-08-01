USE SoftUni
--1 Find Names of All Employees by First Name
SELECT FirstName, LastName FROM Employees
WHERE FirstName LIKE 'SA%'

--2 Find Names of All employees by Last Name 
SELECT Firstname, LastName FROM Employees
WHERE LastName LIKE '%ei%'

--3 Find First Names of All Employees
SELECT FirstName FROM Employees
WHERE DepartmentID = 3 OR DepartmentID = 10 
AND year(HireDate) BETWEEN 1995 AND 2005

--4 Find all employees except engineers

SELECT FirstName, LastName FROM Employees
WHERE JobTitle NOT LIKE '%engineer%'

--05. Find Towns with Name Length

SELECT [Name] FROM Towns
WHERE LEN([Name]) = 5 OR LEN([Name]) = 6
ORDER BY [Name]

--06. Find Towns Starting With

SELECT TownId, [Name] FROM Towns
WHERE 
[Name] LIKE 'm%' 
OR [Name] LIKE 'k%' 
OR [Name] LIKE 'b%' 
OR [Name] LIKE 'e%'
ORDER BY [Name]

--07. Find Towns Not Starting With

SELECT TownId, [Name] FROM Towns
WHERE 
[Name] NOT LIKE 'r%' 
AND [Name] NOT LIKE 'b%' 
AND [Name] NOT LIKE 'd%' 
ORDER BY [Name]

--08. Create View Employees Hired After

CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName 
FROM Employees
WHERE year(HireDate) > 2000

SELECT * FROM [V_EmployeesHiredAfter2000]

-- 09. Length of Last Name

SELECT FirstName, LastName FROM Employees
WHERE LEN(LastName) = 5

--10. Rank Employees by Salary

SELECT * FROM 
(
	SELECT 
	EmployeeID, 
	FirstName,
	LastName, 
	Salary,
	DENSE_RANK() OVER (
		PARTITION BY Salary ORDER BY EmployeeID
	) AS [Rank]
	FROM Employees
	WHERE Salary BETWEEN 10000 AND 50000) 
AS MyTable
WHERE [Rank] = 2
ORDER BY Salary DESC

--12. Countries Holding 'A'
USE Geography

SELECT CountryName, IsoCode FROM Countries
WHERE CountryName LIKE '%a%a%a%'
ORDER BY IsoCode

--13 Mix of Peak and River Names
USE Geography

SELECT 
p.PeakName, 
r.RiverName, 
LOWER(CONCAT(SUBSTRING(p.PeakName, 1, LEN(p.PeakName) - 1), r.RiverName)) AS Mix 
FROM Peaks 
AS p, 
Rivers AS r
WHERE SUBSTRING(p.PeakName, LEN(p.PeakName), 1) = SUBSTRING(r.RiverName, 1, 1)
ORDER BY Mix

--14.Games from 2011 and 2012 year
USE Diablo

SELECT TOP 50 
[Name], 
CONVERT(varchar, [Start], 23) AS [Start] 
FROM Games
WHERE YEAR([Start]) BETWEEN 2011 AND 2012
ORDER BY [Start]

--15. User Email Providers

SELECT 
Username, 
SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email)) AS 'Email Provider' 
FROM Users
ORDER BY [Email Provider], Username

--16. Get Users with IPAddress Like Pattern

SELECT Username, IpAddress FROM Users
WHERE IpAddress LIKE '___.1_%._%.___'
ORDER BY Username

--17. Show All Games with Duration
SELECT * FROM Games

SELECT [Name] AS 'Game',
CASE
	WHEN DATEPART(HOUR, [Start]) >= 0 AND DATEPART(HOUR, [Start]) < 12 THEN 'Morning'
	WHEN DATEPART(HOUR, [Start]) >= 12 AND DATEPART(HOUR, [Start]) < 18 THEN 'Afternoon'
	WHEN DATEPART(HOUR, [Start]) >= 18 AND DATEPART(HOUR, [Start]) < 24 THEN 'Evening'
END AS 'Part of the Day',
CASE
	WHEN Duration <= 3 THEN 'Extra Short'
	WHEN Duration > 3 AND Duration <= 6 THEN 'Short'
	WHEN Duration > 6 THEN 'Long'
	ELSE 'Extra Long'
END AS 'Duration'
FROM Games
ORDER BY [Name], [Duration], [Part of the Day]

--18.Orders table
USE Orders
SELECT * FROM Orders
SELECT 
	ProductName, 
	OrderDate, 
	DATEADD(day, 3, OrderDate) AS 'Pay Due' ,
	DATEADD(month, 1, OrderDate) AS 'Deliver Due' FROM Orders