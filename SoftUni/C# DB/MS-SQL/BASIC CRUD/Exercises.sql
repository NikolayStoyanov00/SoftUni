ALTER TABLE Countries WITH CHECK ADD CONSTRAINT FK_Countries_Continents
FOREIGN KEY(ContinentCode) REFERENCES Continents (ContinentCode)
GO

ALTER TABLE Countries WITH CHECK ADD CONSTRAINT FK_Countries_Currencies
FOREIGN KEY(CurrencyCode) REFERENCES Currencies (CurrencyCode)
GO

ALTER TABLE CountriesRivers WITH CHECK ADD CONSTRAINT FK_CountriesRivers_Countries
FOREIGN KEY(CountryCode) REFERENCES Countries (CountryCode)
GO

ALTER TABLE CountriesRivers WITH CHECK ADD CONSTRAINT FK_CountriesRivers_Rivers
FOREIGN KEY(RiverId) REFERENCES Rivers (Id)
GO

ALTER TABLE MountainsCountries WITH CHECK ADD CONSTRAINT FK_MountainsCountries_Countries
FOREIGN KEY(CountryCode) REFERENCES Countries (CountryCode)
GO

ALTER TABLE MountainsCountries WITH CHECK ADD CONSTRAINT FK_MountainsCountries_Mountains
FOREIGN KEY(MountainId) REFERENCES Mountains (Id)
GO

ALTER TABLE Peaks WITH CHECK ADD CONSTRAINT FK_Peaks_Mountains
FOREIGN KEY(MountainId) REFERENCES Mountains (Id)
GO

SELECT PeakName FROM Peaks
ORDER BY PeakName

SELECT TOP 30 CountryName, Population FROM Countries
WHERE ContinentCode = 'EU'
ORDER BY [Population] DESC, CountryName

SELECT * FROM Countries

SELECT CountryName, CountryCode,
CASE 
	WHEN CurrencyCode = 'EUR' THEN 'Euro'
	ELSE 'Not Euro'
END AS Currency
FROM Countries
ORDER BY CountryName