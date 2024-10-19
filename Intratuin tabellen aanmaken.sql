USE [Intratuin]

--Verwijderen tabellen
DROP TABLE IF EXISTS [dbo].[UserQuestion];
DROP TABLE IF EXISTS [dbo].[UserRole];
DROP TABLE IF EXISTS [dbo].[RouteRoutePoint];
DROP TABLE IF EXISTS [dbo].[Observation];
DROP TABLE IF EXISTS [dbo].[Answer];
DROP TABLE IF EXISTS [dbo].[Question];
DROP TABLE IF EXISTS [dbo].[Game];
DROP TABLE IF EXISTS [dbo].[User];
DROP TABLE IF EXISTS [dbo].[Route];
DROP TABLE IF EXISTS [dbo].[Area];
DROP TABLE IF EXISTS [dbo].[POI];
DROP TABLE IF EXISTS [dbo].[RoutePoint];
DROP TABLE IF EXISTS [dbo].[Specie];
DROP TABLE IF EXISTS [dbo].[Role];

--Aanmaken tabellen
CREATE TABLE Area
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Size FLOAT NOT NULL);

CREATE TABLE [Route]
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Length] FLOAT NOT NULL,
    Area_ID int,
    FOREIGN KEY (Area_ID) REFERENCES Area(ID));

CREATE TABLE Game
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50),
    [Description] VARCHAR(255) NOT NULL,
    Route_ID int,
    FOREIGN KEY (Route_ID) REFERENCES Route(ID));

CREATE TABLE Question
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    QuestionText VARCHAR(255) NOT NULL,
    Game_ID int,
    FOREIGN KEY (Game_ID) REFERENCES Game(ID));

CREATE TABLE Answer
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    AnswerText VARCHAR(50) NOT NULL,
    Question_ID int,
    FOREIGN KEY (Question_ID) REFERENCES Question(ID));

CREATE TABLE Specie
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Domain VARCHAR(50) NOT NULL,
    Regnum VARCHAR(50) NOT NULL,
    Phylum VARCHAR(50) NOT NULL,
    Classus VARCHAR(50) NOT NULL,
    Ordo VARCHAR(50) NOT NULL,
    Familia VARCHAR(50) NOT NULL,
    Genus VARCHAR(50) NOT NULL);

CREATE TABLE [User]
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    CurrentLocation VARCHAR(50),
    Route_ID int,
    FOREIGN KEY (Route_ID) REFERENCES Route(ID));

CREATE TABLE Observation
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50),
    [Description] VARCHAR(255) NOT NULL,
    Picture IMAGE,
    Specie_ID int,
    Area_ID int,
    [User_ID] int,
    FOREIGN KEY (Specie_ID) REFERENCES Specie(ID),
    FOREIGN KEY (Area_ID) REFERENCES Area(ID),
    FOREIGN KEY ([User_ID]) REFERENCES [User](ID));

CREATE TABLE RoutePoint
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50));

CREATE TABLE POI
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50),
    RoutePoint_ID int,
    FOREIGN KEY (RoutePoint_ID) REFERENCES RoutePoint(ID));

CREATE TABLE [Role]
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Key] VARCHAR(50) NOT NULL);

CREATE TABLE RouteRoutePoint
    (Route_ID int NOT NULL,
    RoutePoint_ID int NOT NULL,
    PRIMARY KEY (Route_ID, RoutePoint_ID),
    FOREIGN KEY (Route_ID) REFERENCES Route(ID),
    FOREIGN KEY (RoutePoint_ID) REFERENCES RoutePoint(ID));

CREATE TABLE UserQuestion
    ([User_ID] int NOT NULL,
    Question_ID int NOT NULL,
    PRIMARY KEY ([User_ID], Question_ID),
    FOREIGN KEY ([User_ID]) REFERENCES [User](ID),
    FOREIGN KEY ([Question_ID]) REFERENCES Question(ID));

CREATE TABLE UserRole
    ([User_ID] int NOT NULL,
    Role_ID int NOT NULL,
    PRIMARY KEY ([User_ID], Role_ID),
    FOREIGN KEY ([User_ID]) REFERENCES [User](ID),
    FOREIGN KEY ([Role_ID]) REFERENCES Role(ID));

--Enter data
INSERT INTO [dbo].[User] (Name, Email, CurrentLocation, Route_ID)
VALUES 
    ('Jan Janssen', 'jan.janssen@example.com', '50.8513, 5.6909', NULL),  
    ('Els van Dijk', 'els.vandijk@example.com', '50.8787, 5.9805', NULL),  
    ('Piet Meijer', 'piet.meijer@example.com', NULL, NULL),
    ('Karla Hermans', 'karla.hermans@example.com', '50.9849, 5.9704', NULL),
    ('Tom Peters', 'tom.peters@example.com', '51.1582, 5.8361', NULL),  
    ('Sanne Smit', 'sanne.smit@example.com', '51.1460, 5.8129', NULL); 

INSERT INTO [dbo].Game (Name, Location, Description, Route_ID)
VALUES
	('Dieren Quiz', NULL, 'Kies het juiste antwoord op de vraag; dieren editie!', NULL),
	('Planten Quiz', NULL, 'Kies het juiste antwoord op de vraag; planten editie!', NULL),
	('Lanschappen Quiz', NULL, 'Kies het juiste antwoord op de vraag; landschappen editie!', NULL);

INSERT INTO [dbo].[Question] (QuestionText, Game_ID)
VALUES 
-- Questions Game 1
    ('Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en graag noten eet?', 1),
    ('Wat voor soort dier is de ree, dat vaak in Limburgse bossen en velden te zien is, en staat bekend om zijn lange poten en grote ogen?', 1),
    ('Welke van de volgende vogels zie je vaak in Limburg en staat bekend om zijn heldere, blauwe kleur en zijn acrobatische vliegkunsten?', 1),
    ('Wat voor soort dier is een vossen, die vaak ''s nachts actief is en bekend staat om zijn slimme jachttechnieken?', 1),
    ('Welke amfibie, die vaak in Limburgse poelen en vijvers leeft, staat bekend om zijn grote, platte lichaam en groene kleur?', 1),

-- Questions Game 2
	('Welke plant, die je veel ziet in Limburgse bossen, staat bekend om zijn witte bloemen en wordt vaak in verband gebracht met de lente?', 2),
    ('Welke boomsoort vind je vaak in de Limburgse heuvels en staat bekend om zijn diep ingesneden bladeren en stekelige vruchten?', 2),
    ('Welke bloem, vaak te vinden op kalkrijke gronden in Zuid-Limburg, staat bekend om zijn paarse bloemen en wordt vaak geassocieerd met droog grasland?', 2),
    ('Welke struik, veel voorkomend in Limburgse natuurgebieden, heeft felgele bloemen en wordt vaak gebruikt in hagen en parken?', 2),
    ('Welke boomsoort, veel voorkomend in Limburgse beekdalen, staat bekend om zijn smalle bladeren en vormt vaak een belangrijke habitat voor vogels en insecten?', 2),

-- Questions Game 3
	('Welke Limburgse heuvelrug is bekend om zijn glooiende landschap en rijke flora en fauna?', 3),
    ('Wat is de naam van het bekende natuurgebied in Limburg dat bekend staat om zijn kalksteenformaties en unieke flora?', 3),
    ('Welke rivier stroomt door Limburg en speelt een belangrijke rol in het landschap en de geschiedenis van de regio?', 3),
    ('Wat is de naam van de Limburgse bossen die bekend staan om hun biodiversiteit en oude eiken?', 3),
    ('Welke heuvel in Limburg is het hoogste punt van Nederland en biedt een prachtig uitzicht over de omgeving?', 3);

INSERT INTO [dbo].[Answer] (AnswerText, Question_ID) 
VALUES 
-- Answers Question 1
    ('Eekhoorn', 1), -- True
    ('Das', 1),
    ('Vos', 1),
    ('Konijn', 1),

-- Answers Question 2
    ('Ree', 2), -- True
    ('Hert', 2),
    ('Haas', 2),
    ('Wild zwijn', 2),

-- Answers Question 3
    ('IJsvogel', 3), -- True
    ('Merel', 3),
    ('Ekster', 3),
    ('Specht', 3),

-- Answers Question 4
    ('Vos', 4), -- True
    ('Wolf', 4),
    ('Lynx', 4),
    ('Wasbeerhond', 4),

-- Answers Question 5
    ('Kikker', 5), -- True
    ('Pad', 5),
    ('Salamander', 5),
    ('Waterslang', 5),

-- Answers Question 6
	 ('Bosanemoon', 6), -- True
    ('Zonnebloem', 6), 
    ('Narcis', 6), 
    ('Klaproos', 6),

-- Answers Question 7
    ('Tamme kastanje', 7), -- True
    ('Esdoorn', 7), 
    ('Beuk', 7), 
    ('Wilg', 7),

-- Answers Question 8
    ('Wilde tijm', 8), -- True
    ('Lavendel', 8), 
    ('Blauwe druifjes', 8), 
    ('Paardenbloem', 8),

-- Answers Question 9
    ('Brem', 9), -- True
    ('Forsythia', 9), 
    ('Rododendron', 9), 
    ('Jasmijn', 9),

-- Answers Question 10
    ('Zwarte els', 10), -- True
    ('Eik', 10), 
    ('Berk', 10), 
    ('Populier', 10),

-- Answers Question 11
 ('Sint-Pietersberg', 11), -- True
    ('Hondsrug', 11),
    ('Veluwe', 11),
    ('Utrechtse Heuvelrug', 11),

-- Answers Question 12
    ('De Meinweg', 12), -- True
    ('De Hoge Veluwe', 12),
    ('Sallandse Heuvelrug', 12),
    ('De Biesbosch', 12),

-- Answers Question 13
    ('Maas', 13), -- True
    ('Rijn', 13),
    ('IJssel', 13),
    ('Schelde', 13),

-- Answers Question 14
    ('Het Leudal', 14), -- True
    ('Het Amsterdamse Bos', 14),
    ('Het Amsterdamse Bos', 14),
    ('De Veluwezoom', 14),

-- Answers Question 15
    ('Vaalserberg', 15), -- True
    ('Gulperberg', 15),
    ('Sint-Pietersberg', 15),
    ('Cauberg', 15);

INSERT INTO Specie (Name, Domain, Regnum, Phylum, Classus, Ordo, Familia, Genus) 
VALUES
-- Species planten
('Eikenboom', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Rosales', 'Fagaceae', 'Quercus'),
('Beuk', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Fagales', 'Fagaceae', 'Fagus'),
('Edelweiss', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Asterales', 'Asteraceae', 'Leontopodium'),
('Kruiden (Brandnetel)', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Rosales', 'Urticaceae', 'Urtica'),
('Zevenblad', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Apiales', 'Apiaceae', 'Aegopodium'),
('Berenklauw', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Apiales', 'Apiaceae', 'Heracleum'),
('Hondsdraf', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Lamiales', 'Lamiaceae', 'Glechoma'),
('Paardenbloem', 'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Asterales', 'Asteraceae', 'Taraxacum'),

-- Species dieren
('Gewone Vos', 'Eukarya', 'Animalia', 'Chordata', 'Mammalia', 'Carnivora', 'Canidae', 'Vulpes'),
('Eurasische Egel', 'Eukarya', 'Animalia', 'Chordata', 'Mammalia', 'Eulipotyphla', 'Erinaceidae', 'Erinaceus'),
('Roodborst', 'Eukarya', 'Animalia', 'Chordata', 'Aves', 'Passeriformes', 'Muscicapidae', 'Erithacus'),
('Zilverreiger', 'Eukarya', 'Animalia', 'Chordata', 'Aves', 'Pelecaniformes', 'Ardeidae', 'Ardea'),
('Groene Specht', 'Eukarya', 'Animalia', 'Chordata', 'Aves', 'Piciformes', 'Picidae', 'Picus'),
('Haas', 'Eukarya', 'Animalia', 'Chordata', 'Mammalia', 'Lagomorpha', 'Leporidae', 'Lepus'),
('Veldmuis', 'Eukarya', 'Animalia', 'Chordata', 'Mammalia', 'Rodentia', 'Cricetidae', 'Microtus'),
('Zwarte Kraai', 'Eukarya', 'Animalia', 'Chordata', 'Aves', 'Passeriformes', 'Corvidae', 'Corvus');

INSERT INTO Observation (Name, Location, Description, Picture, Specie_ID, Area_ID, [User_ID])
VALUES 
('Paardenbloem', '50.8503, 5.6900', 'Deze bloem heeft diep ingesneden, groene bladeren die dicht bij de grond groeien. Het heeft heldergele bloemen op lange, stevige stelen.', NULL, 8, NULL, 5),
('Haas', NULL, 'Deze haas heeft een lichtbruine vacht met een witachtige onderkant. Zijn lange oren staan rechtop en zijn ogen zijn groot en zwart, waardoor hij altijd alert is op gevaar.', NULL, 14, NULL, 1),
('Berenklauw', '51.0543, 5.1815', 'Deze plant heeft grote, handvormige bladeren en lange, holle stelen met witte bloemen die in een schermvormige bloeiwijze groeien.', NULL, 6, NULL, 5),
('Vos', NULL, 'Deze vos heeft een roodbruine vacht met een witte punt aan de staart en een lichte onderbuik. Hij is slim en wendbaar, vaak te zien in bossen en velden.', NULL, 9, NULL, 2);



/*
select* FROM [User]
select* FROM Game
select* FROM Question
select* FROM Answer
select* FROM Specie
select* FROM Observation
*/