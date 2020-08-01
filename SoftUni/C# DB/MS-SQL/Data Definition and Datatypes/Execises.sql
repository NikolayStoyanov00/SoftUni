CREATE DATABASE Minions

GO

USE Minions

GO

CREATE TABLE Minions (
	Id INT NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Age INT 
);

CREATE TABLE Towns (
	Id INT NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
);

ALTER TABLE Minions 
ADD CONSTRAINT PK_Id
PRIMARY KEY(Id)

ALTER TABLE Towns 
ADD CONSTRAINT PK_TownId
PRIMARY KEY(Id)

ALTER TABLE Minions 
ADD TownId INT

ALTER TABLE Minions
ADD CONSTRAINT FK_MinionTownId
FOREIGN KEY (TownId) REFERENCES Towns(Id)

INSERT INTO Towns(Id, [Name]) VALUES 
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')

SELECT * FROM Towns

INSERT INTO Minions(Id, [Name], Age, TownId) VALUES
(1, 'Kevin', 22, 1),
(2, 'Bob', 15, 3),
(3, 'Steward', NULL, 2)

SELECT * FROM Minions

USE Minions

DELETE FROM Minions

DROP TABLE Minions

CREATE TABLE People (
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height DECIMAL(3, 2),
	[Weight] DECIMAL(5, 2),
	Gender VARCHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
);

ALTER TABLE People 
ADD CONSTRAINT CHK_P_Picture_2MB CHECK (DATALENGTH(Picture) <= 2097152);

INSERT INTO People([Name], Height, [Weight], Gender, Birthdate, Biography)
VALUES 
('Pesho Mishov', 1.25, 110.52, 'm', '2000-03-20', 'ORAEOFAEOFOAEFOAE'),
('Pesho Mishov', 1.25, 110.52, 'm', '2000-03-20', 'ORAEOFAEOFOAEFOAE'),
('Pesho Mishov', 1.25, 110.52, 'm', '2000-03-20', 'ORAEOFAEOFOAEFOAE'),
('Pesho Mishov', 1.25, 110.52, 'm', '2000-03-20', 'ORAEOFAEOFOAEFOAE'),
('Pesho Mishov', 1.25, 110.52, 'm', '2000-03-20', 'ORAEOFAEOFOAEFOAE');

SELECT * FROM People

CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX),
	LastLoginTime DATETIME,
	IsDeleted VARCHAR(5)
);

INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
VALUES 
('Choki2000', 'akfasklfask123', NULL, NULL, 'true'),
('safasfas', 'akfasklfask123', NULL, NULL, 'true'),
('safasdax', 'akfasklfask123', NULL, NULL, 'true'),
('asdasg', 'akfasklfask123', NULL, NULL, 'true'),
('ascaca', 'akfasklfask123', NULL, NULL, 'true')

SELECT * FROM Users

ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC075E1807AE;

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (Id, Username)

ALTER TABLE Users
ADD CHECK (LEN([Password]) >= 5)

ALTER TABLE Users
ADD CONSTRAINT df_LastLoginTime
DEFAULT CURRENT_TIMESTAMP FOR LastLoginTime 

ALTER TABLE Users
DROP CONSTRAINT PK_Users

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (Id)

ALTER TABLE Users
ADD CHECK (LEN(Username) >= 3)

--PROBLEM 13

CREATE TABLE Directors (
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(30) NOT NULL,
	Notes TEXT
)

CREATE TABLE Genres (
	Id INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(30) NOT NULL,
	Notes TEXT
)

CREATE TABLE Categories (
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(30) NOT NULL,
	Notes TEXT
)

CREATE TABLE Movies (
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(50) NOT NULL,
	DirectorId INT NOT NULL,
	CopyrightYear INT NOT NULL,
	[Length] TIME NOT NULL,
	GenreId INT NOT NULL,
	CategoryId INT NOT NULL,
	Rating INT,
	Notes TEXT
)

INSERT INTO Directors(DirectorName)
VALUES 
('Pesho'),
('Misho'),
('Gosho'),
('Rosko'),
('Pijo')

SELECT * FROM Directors

INSERT INTO Genres (GenreName)
VALUES 
('Pesho'),
('Misho'),
('Gosho'),
('Rosko'),
('Pijo')

SELECT * FROM Genres

INSERT INTO Categories (CategoryName)
VALUES
('Pesho'),
('Misho'),
('Gosho'),
('Rosko'),
('Pijo')

INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId)
VALUES
('Jurassic Park', 1, 2009, '01:39:32', 2, 5),
('Jurassic mark', 2, 2209, '02:39:32', 3, 4),
('Jurassic gark', 3, 2609, '03:39:32', 4, 3),
('Jurassic waerk', 4, 2109, '04:39:32', 5, 1),
('Jurassic Paawrark', 5, 2409, '05:39:32', 6, 2)

SELECT * FROM Movies

--Problem 14 - Car Dental DB

CREATE DATABASE CarRental

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(30) NOT NULL,
	DailyRate INT,
	WeeklyRate INT,
	MonthlyRate INT,
	WeekendRate INT
)

CREATE TABLE Cars (
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber NVARCHAR(30) NOT NULL,
	Manufacturer NVARCHAR(30) NOT NULL,
	Model NVARCHAR(30) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT NOT NULL,
	Doors INT NOT NULL,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(30),
	Available BIT
)

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL, 
	LastName NVARCHAR(30) NOT NULL, 
	Title NVARCHAR(30) NOT NULL, 
	Notes TEXT
)

CREATE TABLE Customers (
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenceNumber NVARCHAR(30) NOT NULL, 
	FullName NVARCHAR(60) NOT NULL, 
	[Address] NVARCHAR(30) NOT NULL, 
	City NVARCHAR(30) NOT NULL, 
	ZIPCode INT NOT NULL, 
	Notes TEXT
)

CREATE TABLE RentalOrders (
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT NOT NULL,
	CustomerId INT NOT NULL,
	CarId INT NOT NULL, 
	TankLevel INT NOT NULL,
	KilometrageStart INT NOT NULL,
	KilometrageEnd INT NOT NULL,
	TotalKilometrage INT NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE,
	TotalDays INT NOT NULL,
	RateApplied DECIMAL(15, 2),
	TaxRate DECIMAL (15, 2),
	OrderStatus NVARCHAR(30) NOT NULL,
	Notes TEXT
)

INSERT INTO Categories(CategoryName)
VALUES
('ne znam'),
('nz brat'),
('ti si znaesh')

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Condition, Available)
VALUES 
('AAA', 'Opel', 'Astra', 1998, 1, 2, 'New', 1),
('BBB', 'Astra', 'Opel', 2322, 2, 3, 'Used', 1),
('CCC', 'Mostra', 'Opel', 2321, 3, 1, 'New', 0)

INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, 
TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus)
VALUES 
(1, 1, 1, 23, 2323, 2325, 2, '2000-01-01', '2000-01-02', 1, 1.25, 1.23, 'Completed'),
(2, 2, 3, 24, 2324, 2326, 2, '2000-01-01', '2000-01-03', 2, 1.25, 1.23, 'Completed'),
(3, 1, 1, 23, 2326, 2328, 2, '2000-01-01', '2000-01-04', 3, 1.25, 1.23, 'Completed')

SELECT * FROM RentalOrders

--15 Hotel Database

CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL, 
	Title NVARCHAR(30) NOT NULL,
	Notes TEXT
)

INSERT INTO Employees (FirstName, LastName, Title)
VALUES
('PEsho', 'Mishov', 'Boss'),
('PEsho', 'Mishov', 'Boss'),
('PEsho', 'Mishov', 'Boss')

CREATE TABLE Customers (
	AccountNumber INT PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL, 
	PhoneNumber INT NOT NULL,
	EmergencyName NVARCHAR(30) NOT NULL,
	EmergencyNumber INT NOT NULL,
	Notes TEXT
)

INSERT INTO Customers (AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber)
VALUES
(123, 'Pesho', 'Moskov', 123123, 'Mosho', 123132123),
(31, 'Pesho', 'sda', 123124, 'fasfas', 123132123),
(1232123, 'dawdwa', 'dawda', 2132, 'asfaf', 21314)

CREATE TABLE RoomStatus (
	RoomStatus NVARCHAR(30) PRIMARY KEY, 
	Notes TEXT
)

INSERT INTO RoomStatus VALUES
('0', 'zaeta'),
('1', 'svobodna'),
('2', 'ss')

CREATE TABLE RoomTypes(
	RoomType NVARCHAR(30) PRIMARY KEY NOT NULL,
	Notes TEXT
)

INSERT INTO RoomTypes (RoomType)
VALUES
('Family'),
('Kids'),
('Full')

CREATE TABLE BedTypes (
	BedType NVARCHAR(30) PRIMARY KEY NOT NULL, 
	Notes TEXT
)

INSERT INTO BedTypes (BedType) 
VALUES
('Adult'),
('Child'),
('Baby')

CREATE TABLE Rooms (
	RoomNumber INT PRIMARY KEY NOT NULL, 
	RoomType NVARCHAR(30) NOT NULL,
	BedType NVARCHAR(30) NOT NULL,
	Rate DECIMAL(10, 2) NOT NULL, 
	RoomStatus BIT NOT NULL,
	Notes TEXT
) 

INSERT INTO Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus)
VALUES
(1, 'Waa', 'wowo', 1.25, 0),
(2, 'wawWaa', 'wdaw', 1.23, 0),
(3, 'awfawa', 'awfawfaw', 1.27, 1)

CREATE TABLE Payments (
	Id INT PRIMARY KEY NOT NULL, 
	EmployeeId INT NOT NULL, 
	PaymentDate DATETIME NOT NULL,
	AccountNumber INT NOT NULL, 
	FirstDateOccupied DATE NOT NULL, 
	LastDateOccupied DATE, 
	TotalDays INT, 
	AmountCharged DECIMAL (10, 2) NOT NULL, 
	TaxRate DECIMAL (10, 2) NOT NULL, 
	TaxAmount DECIMAL (10, 2) NOT NULL, 
	PaymentTotal DECIMAL (10, 2) NOT NULL, 
	Notes TEXT
) 

INSERT INTO Payments  (Id, EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, 
TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal) 
VALUES
(1, 1, '2020-01-01 00:00:00', 1, '2020-01-02 00:00:00', NULL, NULL, 23, 1.2, 1.5, 25.2),
(2, 2, '2020-01-02 00:00:00', 2, '2020-01-03 00:00:00', NULL, NULL, 24, 1.5, 1.2, 25.7),
(3, 3, '2020-01-01 00:00:00', 3, '2020-01-02 00:00:00', NULL, NULL, 23, 1.2, 1.5, 25.2)

CREATE TABLE Occupancies (
	Id INT PRIMARY KEY NOT NULL, 
	EmployeeId INT NOT NULL,
	DateOccupied DATE NOT NULL,
	AccountNumber INT NOT NULL, 
	RoomNumber INT NOT NULL,
	RateApplied DECIMAL (10, 2) NOT NULL, 
	PhoneCharge DECIMAL (10, 2), 
	Notes TEXT
) 

INSERT INTO Occupancies (Id, EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge)
VALUES 
(1, 1, '2020-01-01', 1, 1, 1.25, 1.2),
(2, 2, '2020-01-02', 2, 2, 2.25, 2.2),
(3, 3, '2020-01-03', 3, 3, 3.25, 3.2)

-- 16. Create SoftUni Database

CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns (
	Id INT PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE Addresses (
	Id INT PRIMARY KEY IDENTITY, 
	AddressText NVARCHAR(50) NOT NULL, 
	TownId INT FOREIGN KEY REFERENCES Towns(Id)
) 

CREATE TABLE Departments (
	Id INT PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(30) NOT NULL
) 

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY, 
	FirstName NVARCHAR(30) NOT NULL,
	MiddleName NVARCHAR(30),
	LastName NVARCHAR(30) NOT NULL, 
	JobTitle NVARCHAR(30) NOT NULL, 
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	HireDate DATE NOT NULL, 
	Salary DECIMAL(10,2) NOT NULL, 
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
) 

INSERT INTO Towns 
VALUES
('Sofia'), 
('Plovdiv'), 
('Varna'),
('Burgas')

INSERT INTO Departments
VALUES
('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
VALUES 
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25),
('Georgi', 'Terziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00),
('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88)

SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

SELECT * FROM Towns
ORDER BY [Name]

SELECT * FROM Departments
ORDER BY [Name]

SELECT * FROM Employees
ORDER BY Salary DESC

SELECT [Name] from Towns
ORDER BY [Name]

SELECT [Name] from Departments
ORDER BY [Name]

SELECT FirstName, LastName, JobTitle, Salary FROM Employees
ORDER BY Salary DESC

UPDATE Employees
SET Salary += Salary * 0.10

SELECT Salary FROM Employees

USE Hotel
SELECT * FROM Payments

UPDATE Payments
SET TaxRate -= TaxRate * 0.03

SELECT TaxRate FROM Payments

DELETE FROM Occupancies