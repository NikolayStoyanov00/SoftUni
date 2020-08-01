CREATE DATABASE Airport

--Section 1. DDL (30 pts)
CREATE TABLE Planes (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	Seats INT NOT NULL,
	[Range] INT NOT NULL
)

CREATE TABLE Flights (
	Id INT PRIMARY KEY IDENTITY,
	DepartureTime DATETIME,
	ArrivalTime DATETIME,
	Origin NVARCHAR(50) NOT NULL,
	Destination NVARCHAR(50) NOT NULL,
	PlaneId INT NOT NULL FOREIGN KEY REFERENCES Planes(Id)
)

CREATE TABLE Passengers (
	Id INT PRIMARY KEY IDENTITY, 
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Age INT NOT NULL,
	[Address] NVARCHAR(30) NOT NULL,
	PassportId CHAR(11) NOT NULL
)

CREATE TABLE LuggageTypes (
	Id INT PRIMARY KEY IDENTITY,
	Type NVARCHAR(30) NOT NULL
)

CREATE TABLE Luggages (
	Id INT PRIMARY KEY IDENTITY,
	LuggageTypeId INT NOT NULL FOREIGN KEY REFERENCES LuggageTypes(Id),
	PassengerId INT NOT NULL FOREIGN KEY REFERENCES Passengers(Id)
)

CREATE TABLE Tickets (
	Id INT PRIMARY KEY IDENTITY,
	PassengerId INT NOT NULL FOREIGN KEY REFERENCES Passengers(Id),
	FlightId INT NOT NULL FOREIGN KEY REFERENCES Flights(Id),
	LuggageId INT NOT NULL FOREIGN KEY REFERENCES Luggages(Id),
	Price DECIMAL(10, 2) NOT NULL
)

--Section 2. DML (10 pts)
INSERT INTO Planes ([Name], Seats, [Range])
VALUES 
('Airbus 336', 112, 5132),
('Airbus 330', 432, 5325),
('Boeing 369', 231, 2355),
('Stelt 297', 254, 2143),
('Boeing 338', 165, 5111),
('Airbus 558', 387, 1342),
('Boeing 128', 345, 5541)

INSERT INTO LuggageTypes ([Type])
VALUES
('Crossbody Bag'),
('School Backpack'),
('Shoulder Bag')

UPDATE Tickets 
SET Price += Price * 0.13
FROM Tickets AS t
JOIN Flights AS f ON t.FlightId = f.Id
WHERE f.Destination = 'Carlsbad'

DELETE t 
FROM Tickets AS t
JOIN Flights AS f ON t.FlightId = f.Id
WHERE f.Destination = 'Ayn Halagim'

DELETE FROM Flights
WHERE Destination = 'Ayn Halagim'

--03. Update

--The 'Tr' Planes
SELECT 
Id, [Name], Seats, [Range]
FROM Planes
WHERE [Name] LIKE '%tr%'
ORDER BY Id, [Name], Seats, [Range]

--Flight Profits
SELECT 
f.Id,
SUM(t.Price) AS 'Price'
FROM Flights AS f
JOIN Tickets AS t ON t.FlightId = f.Id
GROUP BY f.Id
ORDER BY SUM(t.Price) DESC, f.Id

--Passenger Trips
SELECT 
p.FirstName + ' ' + LastName AS 'Full Name',
f.Origin,
f.Destination
FROM Passengers AS p
JOIN Tickets AS t ON t.PassengerId = p.Id
JOIN Flights AS f ON t.FlightId = f.Id
ORDER BY [Full Name], f.Origin, f.Destination

--Non Adventures People
SELECT
p.FirstName,
p.LastName,
p.Age
FROM Passengers AS p
LEFT JOIN Tickets AS t ON t.PassengerId = p.Id
WHERE t.PassengerId IS NULL
ORDER BY Age DESC, FirstName, LastName

--Full Info
SELECT 
p.FirstName + ' ' + p.LastName AS 'Full Name',
pl.[Name] AS 'Plane Name',
f.Origin + ' ' + '-' + ' ' + f.Destination AS 'Trip',
lt.[Type] AS 'Luggage Type'
FROM Passengers AS p
FULL JOIN Tickets AS t ON t.PassengerId = p.Id
FULL JOIN Flights AS f ON t.FlightId = f.Id
FULL JOIN Luggages AS l ON l.Id = t.LuggageId
FULL JOIN LuggageTypes AS lt ON lt.Id = l.LuggageTypeId
FULL JOIN Planes AS pl ON pl.Id = f.PlaneId
WHERE p.FirstName IS NOT NULL AND 
Origin IS NOT NULL AND 
Destination IS NOT NULL
ORDER BY [Full Name], [Plane Name], Origin, Destination, [Luggage Type]

--PSP
SELECT 
p.[Name],
p.Seats,
COUNT(pg.Id) AS 'Passengers Count'
FROM Planes AS p
LEFT JOIN Flights AS f ON f.PlaneId = p.Id
LEFT JOIN Tickets AS t ON t.FlightId = f.Id
LEFT JOIN Passengers AS pg ON pg.Id = t.PassengerId
GROUP BY p.Name, p.Seats
ORDER BY COUNT(pg.Id) DESC, p.Name, p.Seats

SELECT * FROM Planes

--Section 4. Programmability (20 pts)

--11. Vacation
CREATE FUNCTION udf_CalculateTickets(@origin VARCHAR(30), @destination VARCHAR(30), @peopleCount INT)
RETURNS NVARCHAR(30)
 BEGIN
 DECLARE @totalPrice DECIMAL(10, 2)
 DECLARE @validFlight INT

 IF (@peopleCount <= 0)
 BEGIN
 RETURN 'Invalid people count!'
 END

 SELECT 
 @totalPrice = SUM(Price)
 FROM Tickets AS t
 JOIN Flights AS f ON f.Id = t.FlightId
 WHERE f.Origin = @origin AND f.Destination = @destination
 
 SELECT
 @validFlight = COUNT(f.Id)
 FROM Flights AS f
 WHERE f.Origin = @origin AND f.Destination = @destination

 IF (@validFlight = 0)
  BEGIN
   RETURN 'Invalid flight!'
  END

 RETURN CONCAT('Total price ', @totalPrice * @peopleCount)
 END

SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', 33) AS 'Output'
SELECT dbo.udf_CalculateTickets('Kolyshley','Rancabolang', -1) AS 'Output'
SELECT dbo.udf_CalculateTickets('Kolyshley','sgsg', 33) AS 'Output'

--12. Wrong data
CREATE PROC usp_CancelFlights 
AS
UPDATE Flights
SET DepartureTime = NULL, ArrivalTime = NULL
WHERE ArrivalTime > DepartureTime

EXEC usp_CancelFlights


