CREATE DATABASE BitBucket

USE BitBucket

--Section 1. DDL (30 pts)
CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY,
	Username NVARCHAR(30) NOT NULL,
	Password NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL
)

CREATE TABLE Repositories (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE RepositoriesContributors (
	RepositoryId INT NOT NULL FOREIGN KEY REFERENCES Repositories(Id),
	ContributorId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
)

ALTER TABLE RepositoriesContributors
ADD CONSTRAINT pk_Repository_Contributor
PRIMARY KEY (RepositoryId,ContributorId)

CREATE TABLE Issues (
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(255) NOT NULL,
	IssueStatus CHAR(6) NOT NULL,
	RepositoryId INT NOT NULL FOREIGN KEY REFERENCES Repositories(Id),
	AssigneeId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
)

CREATE TABLE Commits (
	Id INT PRIMARY KEY IDENTITY,
	Message NVARCHAR(255) NOT NULL,
	IssueId INT FOREIGN KEY REFERENCES Issues(Id),
	RepositoryId INT NOT NULL FOREIGN KEY REFERENCES Repositories(Id),
	ContributorId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
)

CREATE TABLE Files (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NOT NULL,
	Size DECIMAL(10, 2) NOT NULL,
	ParentId INT FOREIGN KEY REFERENCES Files(Id),
	CommitId INT NOT NULL FOREIGN KEY REFERENCES Commits(Id)
)