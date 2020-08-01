--01. Employee Address

SELECT TOP 5 EmployeeID, JobTitle, e.AddressID, a.AddressText
FROM Employees AS e
INNER JOIN Addresses AS a
ON e.AddressID = a.AddressID 
ORDER BY e.AddressID

--02. Addresses with Towns
SELECT TOP 50
e.FirstName, e.LastName, t.Name AS 'Town', a.AddressText
FROM Employees AS e
INNER JOIN Addresses AS a ON e.AddressID = a.AddressID
INNER JOIN Towns AS t ON a.TownID = t.TownID
ORDER BY FirstName, LastName

--03. Sales Employees
SELECT 
e.EmployeeId,
e.FirstName,
e.LastName,
d.[Name]
FROM Employees AS e 
INNER JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE d.[Name] = 'Sales'
ORDER BY EmployeeID

--04. Employee Departments
SELECT TOP 5
e.EmployeeID,
e.FirstName,
e.Salary,
d.[Name] as 'DepartmentName'
FROM Employees as e
INNER JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > 15000
ORDER BY e.DepartmentID

--05. Employees Without Projects

SELECT TOP 3 e.EmployeeID, e.FirstName 
FROM Employees AS e
FULL JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
WHERE ep.EmployeeID IS NULL
ORDER BY EmployeeID

--06. Employees Hired After

SELECT e.FirstName, e.LastName, e.HireDate, d.[Name] AS 'DeptName'
FROM Employees AS e
INNER JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE e.HireDate >= Convert(datetime, '01.01.1999') 
AND d.[Name] = 'Sales' OR d.[Name] = 'Finance'
ORDER BY e.HireDate 

--07. Employees With Project

SELECT TOP 5
e.EmployeeID, e.FirstName, p.[Name] AS 'ProjectName'
FROM Employees AS e
JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p ON ep.ProjectID = p.ProjectID
WHERE p.[Name] IS NOT NULL 
AND p.StartDate > '08-13-2002'
AND p.EndDate IS NULL
ORDER BY EmployeeID

--08. Employee 24

SELECT e.EmployeeID, FirstName, 
CASE
WHEN YEAR(p.StartDate) >= 2005 THEN NULL
ELSE p.[Name]
END
FROM Employees AS e
JOIN EmployeesProjects AS ep ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24

--09. Employee Manager
SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName AS 'ManagerName'
FROM Employees AS e
JOIN Employees AS m ON m.EmployeeId = e.ManagerID
WHERE e.ManagerID IN (3, 7)
ORDER BY e.EmployeeID

--10. Employees Summary
SELECT TOP 50
e.EmployeeID,
e.FirstName + ' ' + e.LastName AS 'EmployeeName',
m.FirstName + ' ' + m.LastName AS 'ManagerName',
d.[Name] AS 'DepartmentName'
FROM Employees AS e
JOIN Employees AS m ON m.EmployeeID = e.ManagerID
JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
ORDER BY EmployeeID

--11. Min Average Salary
SELECT min(avg) AS [MinAverageSalary]
FROM (
       SELECT avg(Salary) AS [avg]
       FROM Employees
       GROUP BY DepartmentID
     ) AS AverageSalary

--12. Highest Peaks in Bulgaria

SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries AS c
JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode
JOIN Mountains AS m ON mc.MountainId = m.Id
JOIN Peaks AS p ON m.Id = p.MountainId
WHERE c.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC

--13. Count Mountain Ranges
SELECT mc.CountryCode, COUNT(mc.CountryCode) AS 'MountainInRanges'
FROM MountainsCountries AS mc
JOIN Mountains AS m ON mc.MountainId = m.Id
WHERE mc.CountryCode IN ('US', 'RU', 'BG')
GROUP BY mc.CountryCode

--14. Countries With or Without Rivers

SELECT TOP 5 
c.CountryName, r.RiverName
FROM Countries AS c
FULL JOIN CountriesRivers AS cr ON c.CountryCode = cr.CountryCode
FULL JOIN Rivers AS r ON cr.RiverId = r.Id
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName

--15. Continents and Currencies 

SELECT OrderedCurrencies.ContinentCode,
	   OrderedCurrencies.CurrencyCode,
	   OrderedCurrencies.CurrencyUsage
  FROM Continents AS c
  JOIN (
	   SELECT ContinentCode AS [ContinentCode],
	   COUNT(CurrencyCode) AS [CurrencyUsage],
	   CurrencyCode as [CurrencyCode],
	   DENSE_RANK() OVER (PARTITION BY ContinentCode
	                      ORDER BY COUNT(CurrencyCode) DESC
						  ) AS [Rank]
	   FROM Countries
	   GROUP BY ContinentCode, CurrencyCode
	   HAVING COUNT(CurrencyCode) > 1
	   )
	   AS OrderedCurrencies
    ON c.ContinentCode = OrderedCurrencies.ContinentCode
 WHERE OrderedCurrencies.Rank = 1

 --16. Countries Without any Mountains
 SELECT COUNT(c.CountryName) - COUNT(mc.MountainId) AS 'Count'
 FROM Countries AS c
 FULL JOIN MountainsCountries AS mc ON c.CountryCode = mc.CountryCode

 --17. Highest Peak and Longest River by Country
SELECT TOP (5)
       Sorted.CountryName,
	   MAX(Sorted.PeakElevation) AS HighestPeakElevation,
	   MAX(Sorted.RiverLength) AS LongestRiverLength
  FROM (
         SELECT c.CountryName AS CountryName,
		 p.Elevation AS PeakElevation,
		 r.Length AS RiverLength
         FROM Countries AS c
         LEFT JOIN MountainsCountries AS mc
         ON c.CountryCode = mc.CountryCode
         LEFT JOIN Peaks AS p
         ON mc.MountainId = p.MountainId
         LEFT JOIN CountriesRivers AS cr
         ON c.CountryCode = cr.CountryCode
         LEFT JOIN Rivers AS r
         ON cr.RiverId = r.Id
        ) AS Sorted
 GROUP BY Sorted.CountryName
 ORDER BY MAX(Sorted.PeakElevation) DESC,
	      MAX(Sorted.RiverLength) DESC,
		  Sorted.CountryName

