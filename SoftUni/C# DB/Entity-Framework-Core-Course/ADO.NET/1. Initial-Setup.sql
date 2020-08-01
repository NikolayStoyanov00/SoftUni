--1. Initial Setup

CREATE DATABASE MinionsDB

USE MinionsDB

CREATE TABLE Countries (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE Towns (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	CountryId INT FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Minions (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	Age INT NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE EvilnessFactors (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE Villains (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id)
)

CREATE TABLE MinionsVillains (
	MinionId INT NOT NULL,
	VillainId INT NOT NULL 
)

ALTER TABLE MinionsVillains
ADD CONSTRAINT pk_MinionId_VillainId PRIMARY KEY (MinionId, VillainId)

ALTER TABLE MinionsVillains
ADD FOREIGN KEY (MinionId) REFERENCES Minions(Id)

ALTER TABLE MinionsVillains
ADD FOREIGN KEY (VillainId) REFERENCES Villains(Id)

INSERT INTO Countries ([Name]) 
VALUES 
('Bulgaria'),
('Germany'),
('United Kingdom'),
('United States'),
('Italy')

INSERT INTO Towns ([Name], CountryId) 
VALUES 
('Sofia', 1),
('Heilbronn', 2),
('London', 3),
('Miami', 4),
('Rome', 5)

INSERT INTO Minions([Name], Age, TownId) 
VALUES
('Peshko', 3, 1),
('Gerard', 13, 2),
('Will', 23, 3),
('Michael', 32, 4),
('Pablo', 43, 5)

INSERT INTO EvilnessFactors ([Name])
VALUES
('Super good'),
('Good'),
('Bad'),
('Evil'),
('Super Evil')

INSERT INTO Villains ([Name], EvilnessFactorId)
VALUES
('Misho', 1),
('Goshko', 2),
('Rosko', 3),
('Loshko', 4),
('Mnogo lo6', 5)

INSERT INTO MinionsVillains (MinionId, VillainId)
VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(5, 2)
