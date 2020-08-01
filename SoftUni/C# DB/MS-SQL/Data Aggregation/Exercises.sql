--01. Records' Count
SELECT 
COUNT(Id) AS 'Count'
FROM WizzardDeposits

--02. Longest Magic Wand
SELECT 
MAX(MagicWandSize) AS 'LongestMagicWand'
FROM WizzardDeposits

--03. Longest Magic Wand per Deposit Groups
SELECT 
DepositGroup, MAX(MagicWandSize) AS 'LongestMagicWand'
FROM WizzardDeposits
GROUP BY DepositGroup

--04. Smallest Deposit Group per Magic Wand Size
SELECT TOP 2
DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

--05. Deposits Sum
SELECT 
DepositGroup,
SUM(DepositAmount) AS 'TotalSum'
FROM WizzardDeposits
GROUP BY DepositGroup

--06. Deposits Sum for Ollivander Family
SELECT 
DepositGroup,
SUM(DepositAmount) AS 'TotalSum'
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

--07.Deposits Filter
SELECT
DepositGroup,
SUM(DepositAmount) AS 'TotalSum'
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY SUM(DepositAmount) DESC

--08. Deposit Charge
SELECT
DepositGroup,
MagicWandCreator,
MIN(DepositCharge) AS 'MinDepositCharge'
FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup

--09. Age Groups
SELECT 
	CASE
	  WHEN w.Age BETWEEN 0 AND 10 THEN '[0-10]'
	  WHEN w.Age BETWEEN 11 AND 20 THEN '[11-20]'
	  WHEN w.Age BETWEEN 21 AND 30 THEN '[21-30]'
	  WHEN w.Age BETWEEN 31 AND 40 THEN '[31-40]'
	  WHEN w.Age BETWEEN 41 AND 50 THEN '[41-50]'
	  WHEN w.Age BETWEEN 51 AND 60 THEN '[51-60]'
	  WHEN w.Age >= 61 THEN '[61+]'
	END AS [AgeGroup],
COUNT(*) AS [WizardCount]
	FROM WizzardDeposits AS w
GROUP BY CASE
	  WHEN w.Age BETWEEN 0 AND 10 THEN '[0-10]'
	  WHEN w.Age BETWEEN 11 AND 20 THEN '[11-20]'
	  WHEN w.Age BETWEEN 21 AND 30 THEN '[21-30]'
	  WHEN w.Age BETWEEN 31 AND 40 THEN '[31-40]'
	  WHEN w.Age BETWEEN 41 AND 50 THEN '[41-50]'
	  WHEN w.Age BETWEEN 51 AND 60 THEN '[51-60]'
	  WHEN w.Age >= 61 THEN '[61+]'
	END

--10. First Letter
SELECT DISTINCT 
LEFT(FirstName, 1) AS 'FirstLetter'
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY FirstName

--11. Average Interest
SELECT
w.DepositGroup,
w.IsDepositExpired,
AVG(w.DepositInterest) AS 'AverageInterest'
FROM WizzardDeposits AS w
WHERE w.DepositStartDate > '01/01/1985'
GROUP BY w.DepositGroup, w.IsDepositExpired
ORDER BY w.DepositGroup DESC, w.IsDepositExpired

--12. Rich Wizard, Poor Wizard
SELECT 
ABS(SUM(XX.Diff)) AS 'SumDifference'
FROM (SELECT DepositAmount - LAG(DepositAmount) OVER (ORDER BY Id)
AS DIFF FROM WizzardDeposits g) AS XX

--13. Departments Total Salaries
SELECT 
e.DepartmentID,
SUM(e.Salary) AS 'TotalSalary'
FROM Employees AS e
GROUP BY e.DepartmentID

--14. Employees Minimum Salaries
SELECT 
e.DepartmentID,
MIN(Salary) AS 'MinimumSalary'
FROM Employees AS e
WHERE e.DepartmentID IN (2, 5, 7) AND e.HireDate > '01/01/2000'
GROUP BY e.DepartmentID

--15. Employees Average Salaries

SELECT *
INTO Exercise
FROM Employees
WHERE Salary > 30000

DELETE Exercise
WHERE ManagerID = 42

UPDATE Exercise
SET Salary += 5000
WHERE DepartmentID = 1

SELECT
DepartmentID,
AVG(Salary) AS 'AverageSalary'
FROM Exercise
GROUP BY DepartmentID