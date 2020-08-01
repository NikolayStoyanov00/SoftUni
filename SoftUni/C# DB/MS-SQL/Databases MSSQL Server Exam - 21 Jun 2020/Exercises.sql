--Section 1. DDL (30 pts)
CREATE DATABASE TripService

CREATE TABLE Cities (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	CountryCode NCHAR(2) NOT NULL
)

CREATE TABLE Hotels (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30),
	CityId INT NOT NULL FOREIGN KEY REFERENCES Cities(Id),
	EmployeeCount INT NOT NULL,
	BaseRATE DECIMAL(10, 2)
)

CREATE TABLE Rooms (
	Id INT PRIMARY KEY IDENTITY,
	Price DECIMAL(10, 2) NOT NULL,
	[Type] NVARCHAR(20) NOT NULL,
	Beds INT NOT NULL,
	HotelId INT NOT NULL FOREIGN KEY REFERENCES Hotels(Id)
)

CREATE TABLE Trips (
	Id INT PRIMARY KEY IDENTITY,
	RoomId INT NOT NULL FOREIGN KEY REFERENCES Rooms(Id),
	BookDate DATE NOT NULL,
	ArrivalDate DATE NOT NULL,
	ReturnDate DATE NOT NULL,
	CancelDate DATE
)

ALTER TABLE Trips
ADD CONSTRAINT CHK_BookDate CHECK (BookDate < ArrivalDate);

ALTER TABLE Trips
ADD CONSTRAINT CHK_ArrivalDate CHECK (ArrivalDate < ReturnDate);

CREATE TABLE Accounts (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(20),
	LastName NVARCHAR(50) NOT NULL,
	CityId INT NOT NULL FOREIGN KEY REFERENCES Cities(Id),
	BirthDate DATE NOT NULL,
	Email VARCHAR(100) NOT NULL UNIQUE
)

CREATE TABLE AccountsTrips (
	AccountId INT NOT NULL FOREIGN KEY REFERENCES Accounts(Id),
	TripId INT NOT NULL FOREIGN KEY REFERENCES Trips(Id),
	Luggage INT NOT NULL CHECK (Luggage >= 0)
)

ALTER TABLE AccountsTrips
ADD CONSTRAINT pk_AccountId_TripId
PRIMARY KEY (AccountId, TripId)

--Section 2. DML (10 pts)

--02.Insert
INSERT INTO Accounts (FirstName, MiddleName, LastName, CityId, BirthDate, Email)
VALUES 
('John', 'Smith', 'Smith', 34, '1975-07-21', 'j_smith@gmail.com'),
('Gosho', NULL, 'Petrov', 11, '1978-05-16', 'g_petrov@gmail.com'),
('Ivan', 'Petrovich', 'Pavlov', 59, '1849-09-26', 'i_pavlov@softuni.bg'),
('Friedrich', 'Wilhelm', 'Nietzsche', 2, '1844-10-15', 'f_nietzsche@softuni.bg')

INSERT INTO Trips (RoomId, BookDate, ArrivalDate, ReturnDate, CancelDate)
VALUES
(101, '2015-04-12', '2015-04-14', '2015-04-20', '2015-02-02'),
(102, '2015-07-07', '2015-07-15', '2015-07-22', '2015-04-29'),
(103, '2013-07-17', '2013-07-23', '2013-07-24', NULL),
(104, '2012-03-17', '2012-03-31', '2012-04-01', '2012-01-10'),
(109, '2017-08-07', '2017-08-28', '2017-08-29', NULL)

--03. Update
UPDATE Rooms
SET Price += Price * 0.14
WHERE HotelId = 5 OR HotelId = 7 OR HotelId = 9

--04. Delete
DELETE AccountsTrips
WHERE AccountId = 47

--Section 3. Querying (40 pts)

--05. EEE-Mails
SELECT 
a.FirstName, 
a.LastName, 
FORMAT(a.BirthDate, 'MM-dd-yyyy') AS 'BirthDate',
c.Name AS 'HomeTown',
a.Email 
FROM Accounts AS a
JOIN Cities AS c ON c.Id = a.CityId
WHERE a.Email LIKE 'e%'
ORDER BY c.Name

--06. City Statistics
SELECT 
c.Name,
COUNT(h.Id) AS 'Hotels'
FROM Cities AS c
JOIN Hotels AS h ON h.CityId = c.Id
GROUP BY c.Id, c.Name
ORDER BY [Hotels] DESC, c.Name

--07. Longest and Shortest Trips
SELECT
at.AccountId,
a.FirstName + ' ' + a.LastName AS 'FullName',
MAX(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS 'LongestTrip',
MIN(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS 'ShortestTrip'
FROM AccountsTrips AS at
JOIN Accounts AS a ON a.Id = at.AccountId
JOIN Trips AS t ON at.TripId = t.Id
WHERE a.MiddleName IS NULL AND t.CancelDate IS NULL
GROUP BY at.AccountId, a.FirstName, a.LastName
ORDER BY [LongestTrip] DESC, [ShortestTrip]

--08. Metropolis
SELECT TOP 10 
c.Id, 
c.Name,
c.CountryCode AS 'Country',
COUNT(a.Id) AS 'Accounts'
FROM Cities AS c
JOIN Accounts AS a ON a.CityId = c.Id
GROUP BY c.Id, c.Name, c.CountryCode
ORDER BY [Accounts] DESC

--09. Romantic Getaways
SELECT
a.Id,
a.Email,
c.Name AS 'City',
COUNT(at.AccountId) AS 'Trips'
FROM Accounts AS a
JOIN Cities AS c ON c.Id = a.CityId
JOIN AccountsTrips AS at ON at.AccountId = a.Id
JOIN Trips AS t ON t.Id = at.TripId
JOIN Rooms AS r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
WHERE h.CityId = a.CityId
GROUP BY a.Id, a.Email, c.Name
ORDER BY [Trips] DESC, a.Id

--10. GDPR Violation
SELECT
t.Id,
a.FirstName + ' ' + IIF(a.MiddleName IS NOT NULL, a.MiddleName  + ' ', '') + a.LastName AS 'Full Name',
c.Name AS 'From',
(SELECT c.Name
FROM Hotels AS H
JOIN Cities AS c ON c.Id = h.CityId
WHERE h.Id = r.HotelId) AS 'To',
CASE
 WHEN t.CancelDate IS NOT NULL THEN 'Canceled'
 ELSE CAST(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate) AS nvarchar) + ' days'
END AS 'Duration'
FROM Trips AS t
JOIN AccountsTrips AS at ON at.TripId = t.Id
JOIN Accounts AS a ON a.Id = at.AccountId
JOIN Cities AS c ON c.Id = a.CityId
JOIN Rooms AS r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
ORDER BY [Full Name], t.Id


--Section 4. Programmability (14 pts)

--11. Available Room

CREATE FUNCTION udf_GetAvailableRoom (@HotelId INT, @Date DATE, @People INT)
RETURNS NVARCHAR(100)
 BEGIN
	DECLARE @occupied BIT
	DECLARE @roomId INT
	DECLARE @freeRoomId INT
	DECLARE @totalPrice DECIMAL(10, 2)
	DECLARE @roomType NVARCHAR(30)
	DECLARE @roomBeds INT
	DECLARE @result NVARCHAR(100)

	 SELECT @roomId = (Select
	 r.Id
	 FROM Trips AS t
	 JOIN Rooms AS r ON r.Id = t.RoomId
	 JOIN Hotels AS h ON h.Id = r.HotelId
	 WHERE @Date BETWEEN t.ArrivalDate AND t.ReturnDate AND t.CancelDate IS NULL
	 AND h.Id = @HotelId
	 AND r.Beds >= @People
	 GROUP BY r.Id, r.Price, h.BaseRATE, t.ArrivalDate, t.ReturnDate
	 ORDER BY (h.BaseRATE + r.Price) * @People DESC OFFSET 0 ROWS
	 )

	 IF(@roomId IS NOT NULL) 
	 BEGIN
	  RETURN 'No rooms available'
	 END
	 ELSE IF (@roomId IS NULL)
	 BEGIN
	  SELECT @totalPrice = (SELECT TOP 1
		 (h.BaseRATE + r.Price) * @People AS 'TotalPrice'
		 FROM Trips AS t
		 JOIN Rooms AS r ON r.Id = t.RoomId
		 JOIN Hotels AS h ON h.Id = r.HotelId
		 WHERE @Date NOT BETWEEN t.ArrivalDate AND t.ReturnDate 
		 AND h.Id = @HotelId
		 AND r.Beds >= @People
		 GROUP BY r.Id, r.Price, h.BaseRATE, t.ArrivalDate, t.ReturnDate
		 ORDER BY [TotalPrice] DESC
		 )

		SELECT @freeRoomId = (SELECT TOP 1
		 r.Id
		 FROM Trips AS t
		 JOIN Rooms AS r ON r.Id = t.RoomId
		 JOIN Hotels AS h ON h.Id = r.HotelId
		 WHERE @Date NOT BETWEEN t.ArrivalDate AND t.ReturnDate 
		 AND h.Id = @HotelId
		 AND r.Beds >= @People
		 GROUP BY r.Id, r.Price, h.BaseRATE, t.ArrivalDate, t.ReturnDate
		 ORDER BY (h.BaseRATE + r.Price) * @People DESC 
		 )

		 SELECT @roomType = (SELECT TOP 1
		 r.Type
		 FROM Trips AS t
		 JOIN Rooms AS r ON r.Id = t.RoomId
		 JOIN Hotels AS h ON h.Id = r.HotelId
		 WHERE @Date NOT BETWEEN t.ArrivalDate AND t.ReturnDate 
		 AND h.Id = @HotelId
		 AND r.Beds >= @People
		 GROUP BY r.Id, r.Price, h.BaseRATE, t.ArrivalDate, t.ReturnDate, r.Type
		 ORDER BY (h.BaseRATE + r.Price) * @People DESC 
		 )

		 SELECT @roomBeds = (SELECT TOP 1
		 r.Beds
		 FROM Trips AS t
		 JOIN Rooms AS r ON r.Id = t.RoomId
		 JOIN Hotels AS h ON h.Id = r.HotelId
		 WHERE @Date NOT BETWEEN t.ArrivalDate AND t.ReturnDate 
		 AND h.Id = @HotelId
		 AND r.Beds >= @People
		 GROUP BY r.Id, r.Price, h.BaseRATE, t.ArrivalDate, t.ReturnDate, r.Beds
		 ORDER BY (h.BaseRATE + r.Price) * @People DESC
		 )

		 SET @result = CONCAT('Room ', @freeRoomId, ': ', @roomType, 
		 ' (', @roomBeds , ' beds) - $' , @totalPrice)

		 RETURN @result
	 END
		 
	 RETURN 'Test'
 END

 DROP FUNCTION dbo.udf_GetAvailableRoom

 SELECT dbo.udf_GetAvailableRoom(112, '2011-12-17', 2)

 SELECT 
 r.Id,
 t.ArrivalDate,
 t.ReturnDate,
 (h.BaseRATE + r.Price) * 3 AS 'TotalPrice'
FROM Trips AS t
JOIN Rooms AS r ON r.Id = t.RoomId
JOIN Hotels AS h ON h.Id = r.HotelId
WHERE '2011-05-31' BETWEEN t.ArrivalDate AND t.ReturnDate AND t.CancelDate IS NOT NULL,
WHERE '2011-05-31' NOT BETWEEN t.ArrivalDate AND t.ReturnDate
GROUP BY r.Id, r.Price, h.BaseRATE, t.ArrivalDate, t.ReturnDate
ORDER BY [TotalPrice] DESC

SELECT
		 r.Id
		 FROM Trips AS t
		 JOIN Rooms AS r ON r.Id = t.RoomId
		 JOIN Hotels AS h ON h.Id = r.HotelId
		 WHERE '2011-05-31' BETWEEN t.ArrivalDate AND t.ReturnDate AND t.CancelDate IS NOT NULL
		 GROUP BY r.Id, r.Price, h.BaseRATE, t.ArrivalDate, t.ReturnDate
		 ORDER BY (h.BaseRATE + r.Price) * 2 DESC 


--12. Switch Room
CREATE PROC usp_SwitchRoom (@TripId INT, @TargetRoomId INT)
AS
BEGIN
DECLARE @targetRoomIdHotel INT
DECLARE @tripHotelId INT
DECLARE @targetRoomBeds INT
DECLARE @tripsAccounts INT



SET @tripHotelId = (
 SELECT 
  h.Id
 FROM Trips AS t
 JOIN Rooms AS r ON r.Id = t.RoomId
 JOIN Hotels AS h ON h.Id = r.HotelId
 WHERE t.Id = @TripId
)

SET @targetRoomIdHotel = (
	 SELECT 
	 h.Id
	 FROM Hotels AS h
	 JOIN Rooms AS r ON r.HotelId = h.Id
	 WHERE r.Id = @TargetRoomId
)

SET @targetRoomBeds = (
	SELECT Beds FROM Rooms
	WHERE Id = @TargetRoomId
)

SELECT @tripsAccounts = COUNT(*) from AccountsTrips
WHERE @TripId = TripId

 IF (@tripHotelId != @targetRoomIdHotel)
  BEGIN
	THROW 51000, 'Target room is in another hotel!', 1
  END
 ELSE 
  BEGIN
    IF (@tripsAccounts > @targetRoomBeds)
	  BEGIN
	   THROW 51000, 'Not enough beds in target room!', 1
	  END 
	ELSE 
	BEGIN
	   UPDATE Trips
	   SET RoomId = @TargetRoomId
	   WHERE Id = @TripId
	END
  END
END

  EXEC usp_SwitchRoom 10, 8

  DROP PROC usp_SwitchRoom
