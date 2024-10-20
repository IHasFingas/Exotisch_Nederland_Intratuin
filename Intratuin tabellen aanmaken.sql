USE [Intratuin]

--Delete tables
DROP TABLE IF EXISTS UserQuestion;
DROP TABLE IF EXISTS UserRole;
DROP TABLE IF EXISTS RouteRoutePoint;
DROP TABLE IF EXISTS Observation;
DROP TABLE IF EXISTS Answer;
DROP TABLE IF EXISTS Question;
DROP TABLE IF EXISTS Game;
DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [Route];
DROP TABLE IF EXISTS Area;
DROP TABLE IF EXISTS POI;
DROP TABLE IF EXISTS RoutePoint;
DROP TABLE IF EXISTS Specie;
DROP TABLE IF EXISTS [Role];


--Create tables
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
INSERT INTO Area ([Name], Size)
VALUES
	('De Groote Peel',				1340),
	('Nationaal Park Hoge Kempen',	5700),
	('Brunsummerheide',				600),
	('Het Leudal',					900),
	('De Meinweg',					1800);


INSERT INTO [Role] ([Name], [Key])
VALUES
	('Hiker',			'hJ4xW8lPnQ'),
	('Volunteer',		'gT9cR2vLmD'),
	('Validator',		'Zk3qY7sHpB');


INSERT INTO Specie ([Name], Domain, Regnum, Phylum, Classus, Ordo, Familia, Genus) 
VALUES
-- Plants
	('Eikenboom',		'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Rosales',			'Fagaceae',		'Quercus'),
	('Beuk',			'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Fagales',			'Fagaceae',		'Fagus'),
	('Edelweiss',		'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Asterales',		'Asteraceae',	'Leontopodium'),
	('Brandnetel',		'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Rosales',			'Urticaceae',	'Urtica'),
	('Zevenblad',		'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Apiales',			'Apiaceae',		'Aegopodium'),
	('Berenklauw',		'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Apiales',			'Apiaceae',		'Heracleum'),
	('Hondsdraf',		'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Lamiales',		'Lamiaceae',	'Glechoma'),
	('Paardenbloem',	'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Asterales',		'Asteraceae',	'Taraxacum'),

-- Animals
	('Gewone Vos',		'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Carnivora',		'Canidae',		'Vulpes'),
	('Eurasische Egel', 'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Eulipotyphla',	'Erinaceidae',	'Erinaceus'),
	('Roodborst',		'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Passeriformes',	'Muscicapidae', 'Erithacus'),
	('Zilverreiger',	'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Pelecaniformes',	'Ardeidae',		'Ardea'),
	('Groene Specht',	'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Piciformes',		'Picidae',		'Picus'),
	('Haas',			'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Lagomorpha',		'Leporidae',	'Lepus'),
	('Veldmuis',		'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Rodentia',		'Cricetidae',	'Microtus'),
	('Zwarte Kraai',	'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Passeriformes',	'Corvidae',		'Corvus');


INSERT INTO RoutePoint ([Name], [Location])
VALUES
	('Peelzicht',		'51.3412, 5.8743'),	-- De Groote Peel
	('Peelven',			'51.3456, 5.8801'),	-- De Groote Peel
	('Kempenbos',		'50.9745, 5.6931'),	-- Nationaal Park Hoge Kempen
	('Maasvallei',		'50.9787, 5.7456'),	-- Nationaal Park Hoge Kempen
	('Heidezicht',		'50.9203, 5.9634'),	-- Brunsummerheide
	('Schinveldse Bos', '50.9371, 5.9802'),	-- Brunsummerheide
	('Leubeek',			'51.2534, 5.9712'),	-- Het Leudal
	('Exaten',			'51.2608, 5.9825'),	-- Het Leudal
	('Elfenmeer',		'51.1476, 6.0562'),	-- De Meinweg
	('Rolvennen',		'51.1613, 6.0724');	-- De Meinweg


INSERT INTO [Route] ([Name], [Length], Area_ID)
VALUES
	('Peel Trail',		2.3,	(SELECT ID FROM Area WHERE [Name] = 'De Groote Peel')),				-- Between 'Peelzicht' and 'Peelven' in De Groote Peel
	('Kempen Path',		5.6,	(SELECT ID FROM Area WHERE [Name] = 'Nationaal Park Hoge Kempen')),	-- Between 'Kempenbos' and 'Maasvallei' in Nationaal Park Hoge Kempen
	('Heide Walk',		3.1,	(SELECT ID FROM Area WHERE [Name] = 'Brunsummerheide')),			-- Between 'Heidezicht' and 'Schinveldse Bos' in Brunsummerheide
	('Leudal Loop',		4.8,	(SELECT ID FROM Area WHERE [Name] = 'Het Leudal')),					-- Between 'Leubeek' and 'Exaten' in Het Leudal
	('Meinweg Ridge',	2.7,	(SELECT ID FROM Area WHERE [Name] = 'De Meinweg'));					-- Between 'Elfenmeer' and 'Rolvennen' in De Meinweg


INSERT INTO RouteRoutePoint (Route_ID, RoutePoint_ID)
VALUES
	((SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelzicht')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelven')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Kempenbos')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Maasvallei')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Heidezicht')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Schinveldse Bos')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Leudal Loop'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Leubeek')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Meinweg Ridge'),	(SELECT ID FROM RoutePoint WHERE [Name] = 'Exaten')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Meinweg Ridge'),	(SELECT ID FROM RoutePoint WHERE [Name] = 'Rolvennen'));


INSERT INTO POI ([Name], [Location], RoutePoint_ID)
VALUES
	('Peel Observation Tower',		(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Peelzicht'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelzicht')),
	('Peelven Birdwatch',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Peelven'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelven')),
	('Kempen Forest View',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Kempenbos'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Kempenbos')),
	('Maas River Lookout',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Maasvallei'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Maasvallei')),
	('Heide Panorama',				(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Heidezicht'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Heidezicht')),
	('Schinveldse Forest Shelter',	(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Schinveldse Bos'),	(SELECT ID FROM RoutePoint WHERE [Name] = 'Schinveldse Bos')),
	('Leubeek Waterfall',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Leubeek'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Leubeek')),
	('Exaten Castle Ruins',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Exaten'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Exaten')),
	('Elfenmeer Lakeside',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Elfenmeer'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Elfenmeer')),
	('Rolvennen Scenic Point',		(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Rolvennen'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Rolvennen'));


INSERT INTO [User] ([Name], Email, CurrentLocation, Route_ID)
VALUES 
    ('Jan Janssen',		'jan.janssen@example.com',		'50.8513, 5.6909',	(SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path')),
	('Piet Meijer',		'piet.meijer@example.com',		'',					(SELECT ID FROM [Route] WHERE [Name] = 'Leudal Loop')),
    ('Els van Dijk',	'els.vandijk@example.com',		'50.8787, 5.9805',	(SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk')),  
    ('Karla Hermans',	'karla.hermans@example.com',	'50.9849, 5.9704',	(SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk')),
    ('Tom Peters',		'tom.peters@example.com',		'51.1582, 5.8361',	(SELECT ID FROM [Route] WHERE [Name] = 'Meinweg Ridge')),  
    ('Sanne Smit',		'sanne.smit@example.com',		'51.1460, 5.8129',	(SELECT ID FROM [Route] WHERE [Name] = 'Meinweg Ridge'));


INSERT INTO Game ([Name], [Location], [Description], Route_ID)
VALUES
	('Dieren Quiz',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Peelzicht'),	'Kies het juiste antwoord op de vraag; dieren editie!',			(SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail')),
	('Planten Quiz',		(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Kempenbos'),	'Kies het juiste antwoord op de vraag; planten editie!',		(SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path')),
	('Lanschappen Quiz',	(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Elfenmeer'),	'Kies het juiste antwoord op de vraag; landschappen editie!',	(SELECT ID FROM [Route] WHERE [Name] = 'Meinweg Ridge'));


INSERT INTO Observation ([Name], [Location], [Description], Picture, Specie_ID, Area_ID, [User_ID])
VALUES 
	('Paardenbloem',		'50.8503, 5.6900',	'Deze bloem heeft diep ingesneden, groene bladeren die dicht bij de grond groeien. Het heeft heldergele bloemen op lange, stevige stelen.',										NULL, (SELECT ID FROM Specie WHERE [Name] = 'Paardenbloem'),	(SELECT ID FROM Area WHERE [Name] = 'Brunsummerheide'),				(SELECT ID FROM [User] WHERE [Name] = 'Els van Dijk')),
	('Haas',				'',					'Deze haas heeft een lichtbruine vacht met een witachtige onderkant. Zijn lange oren staan rechtop en zijn ogen zijn groot en zwart, waardoor hij altijd alert is op gevaar.',	NULL, (SELECT ID FROM Specie WHERE [Name] = 'Haas'),			(SELECT ID FROM Area WHERE [Name] = 'Nationaal Park Hoge Kempen'),	(SELECT ID FROM [User] WHERE [Name] = 'Jan Janssen')),
	('Berenklauw',			'51.0543, 5.1815',	'Deze plant heeft grote, handvormige bladeren en lange, holle stelen met witte bloemen die in een schermvormige bloeiwijze groeien.',											NULL, (SELECT ID FROM Specie WHERE [Name] = 'Berenklauw'),		(SELECT ID FROM Area WHERE [Name] = 'De Meinweg'),					(SELECT ID FROM [User] WHERE [Name] = 'Tom Peters'));


INSERT INTO Question (QuestionText, Game_ID)
VALUES 
-- Questions Game 1
    ('Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en graag noten eet?',								1),
    ('Wat voor soort dier is de ree, dat vaak in Limburgse bossen en velden te zien is, en staat bekend om zijn lange poten en grote ogen?',							1),
    ('Welke van de volgende vogels zie je vaak in Limburg en staat bekend om zijn heldere, blauwe kleur en zijn acrobatische vliegkunsten?',							1),
    ('Wat voor soort dier is een vossen, die vaak ''s nachts actief is en bekend staat om zijn slimme jachttechnieken?',												1),
    ('Welke amfibie, die vaak in Limburgse poelen en vijvers leeft, staat bekend om zijn grote, platte lichaam en groene kleur?',										1),

-- Questions Game 2
	('Welke plant, die je veel ziet in Limburgse bossen, staat bekend om zijn witte bloemen en wordt vaak in verband gebracht met de lente?',							2),
    ('Welke boomsoort vind je vaak in de Limburgse heuvels en staat bekend om zijn diep ingesneden bladeren en stekelige vruchten?',									2),
    ('Welke bloem, vaak te vinden op kalkrijke gronden in Zuid-Limburg, staat bekend om zijn paarse bloemen en wordt vaak geassocieerd met droog grasland?',			2),
    ('Welke struik, veel voorkomend in Limburgse natuurgebieden, heeft felgele bloemen en wordt vaak gebruikt in hagen en parken?',										2),
    ('Welke boomsoort, veel voorkomend in Limburgse beekdalen, staat bekend om zijn smalle bladeren en vormt vaak een belangrijke habitat voor vogels en insecten?',	2),

-- Questions Game 3
	('Welke Limburgse heuvelrug is bekend om zijn glooiende landschap en rijke flora en fauna?',																		3),
    ('Wat is de naam van het bekende natuurgebied in Limburg dat bekend staat om zijn kalksteenformaties en unieke flora?',												3),
    ('Welke rivier stroomt door Limburg en speelt een belangrijke rol in het landschap en de geschiedenis van de regio?',												3),
    ('Wat is de naam van de Limburgse bossen die bekend staan om hun biodiversiteit en oude eiken?',																	3),
    ('Welke heuvel in Limburg is het hoogste punt van Nederland en biedt een prachtig uitzicht over de omgeving?',														3);


INSERT INTO Answer (AnswerText, Question_ID)
VALUES 
-- Answers Question 1
    ('Eekhoorn',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en graag noten eet?')), -- True
    ('Das',					(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en graag noten eet?')),
    ('Vos',					(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en graag noten eet?')),
    ('Konijn',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en graag noten eet?')),

-- Answers Question 2
    ('Ree',					(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is de ree, dat vaak in Limburgse bossen en velden te zien is, en staat bekend om zijn lange poten en grote ogen?')), -- True
    ('Hert',				(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is de ree, dat vaak in Limburgse bossen en velden te zien is, en staat bekend om zijn lange poten en grote ogen?')),
    ('Haas',				(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is de ree, dat vaak in Limburgse bossen en velden te zien is, en staat bekend om zijn lange poten en grote ogen?')),
    ('Wild zwijn',			(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is de ree, dat vaak in Limburgse bossen en velden te zien is, en staat bekend om zijn lange poten en grote ogen?')),

-- Answers Question 3
    ('IJsvogel',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende vogels zie je vaak in Limburg en staat bekend om zijn heldere, blauwe kleur en zijn acrobatische vliegkunsten?')), -- True
    ('Merel',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende vogels zie je vaak in Limburg en staat bekend om zijn heldere, blauwe kleur en zijn acrobatische vliegkunsten?')),
    ('Ekster',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende vogels zie je vaak in Limburg en staat bekend om zijn heldere, blauwe kleur en zijn acrobatische vliegkunsten?')),
    ('Specht',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke van de volgende vogels zie je vaak in Limburg en staat bekend om zijn heldere, blauwe kleur en zijn acrobatische vliegkunsten?')),

-- Answers Question 4
    ('Vos',					(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is een vossen, die vaak ''s nachts actief is en bekend staat om zijn slimme jachttechnieken?')), -- True
    ('Wolf',				(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is een vossen, die vaak ''s nachts actief is en bekend staat om zijn slimme jachttechnieken?')),
    ('Lynx',				(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is een vossen, die vaak ''s nachts actief is en bekend staat om zijn slimme jachttechnieken?')),
    ('Wasbeerhond',			(SElECT ID FROM Question WHERE [QuestionText] = 'Wat voor soort dier is een vossen, die vaak ''s nachts actief is en bekend staat om zijn slimme jachttechnieken?')),

-- Answers Question 5
    ('Kikker',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke amfibie, die vaak in Limburgse poelen en vijvers leeft, staat bekend om zijn grote, platte lichaam en groene kleur?')), -- True
    ('Pad',					(SElECT ID FROM Question WHERE [QuestionText] = 'Welke amfibie, die vaak in Limburgse poelen en vijvers leeft, staat bekend om zijn grote, platte lichaam en groene kleur?')),
    ('Salamander',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke amfibie, die vaak in Limburgse poelen en vijvers leeft, staat bekend om zijn grote, platte lichaam en groene kleur?')),
    ('Waterslang',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke amfibie, die vaak in Limburgse poelen en vijvers leeft, staat bekend om zijn grote, platte lichaam en groene kleur?')),

-- Answers Question 6
	('Bosanemoon',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke plant, die je veel ziet in Limburgse bossen, staat bekend om zijn witte bloemen en wordt vaak in verband gebracht met de lente?')), -- True
    ('Zonnebloem',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke plant, die je veel ziet in Limburgse bossen, staat bekend om zijn witte bloemen en wordt vaak in verband gebracht met de lente?')), 
    ('Narcis',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke plant, die je veel ziet in Limburgse bossen, staat bekend om zijn witte bloemen en wordt vaak in verband gebracht met de lente?')), 
    ('Klaproos',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke plant, die je veel ziet in Limburgse bossen, staat bekend om zijn witte bloemen en wordt vaak in verband gebracht met de lente?')),

-- Answers Question 7
    ('Tamme kastanje',		(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort vind je vaak in de Limburgse heuvels en staat bekend om zijn diep ingesneden bladeren en stekelige vruchten?')), -- True
    ('Esdoorn',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort vind je vaak in de Limburgse heuvels en staat bekend om zijn diep ingesneden bladeren en stekelige vruchten?')), 
    ('Beuk',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort vind je vaak in de Limburgse heuvels en staat bekend om zijn diep ingesneden bladeren en stekelige vruchten?')), 
    ('Wilg',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort vind je vaak in de Limburgse heuvels en staat bekend om zijn diep ingesneden bladeren en stekelige vruchten?')),

-- Answers Question 8
    ('Wilde tijm',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke bloem, vaak te vinden op kalkrijke gronden in Zuid-Limburg, staat bekend om zijn paarse bloemen en wordt vaak geassocieerd met droog grasland?')), -- True
    ('Lavendel',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke bloem, vaak te vinden op kalkrijke gronden in Zuid-Limburg, staat bekend om zijn paarse bloemen en wordt vaak geassocieerd met droog grasland?')), 
    ('Blauwe druifjes',		(SElECT ID FROM Question WHERE [QuestionText] = 'Welke bloem, vaak te vinden op kalkrijke gronden in Zuid-Limburg, staat bekend om zijn paarse bloemen en wordt vaak geassocieerd met droog grasland?')), 
    ('Paardenbloem',		(SElECT ID FROM Question WHERE [QuestionText] = 'Welke bloem, vaak te vinden op kalkrijke gronden in Zuid-Limburg, staat bekend om zijn paarse bloemen en wordt vaak geassocieerd met droog grasland?')),

-- Answers Question 9
    ('Brem',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke struik, veel voorkomend in Limburgse natuurgebieden, heeft felgele bloemen en wordt vaak gebruikt in hagen en parken?')), -- True
    ('Forsythia',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke struik, veel voorkomend in Limburgse natuurgebieden, heeft felgele bloemen en wordt vaak gebruikt in hagen en parken?')), 
    ('Rododendron',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke struik, veel voorkomend in Limburgse natuurgebieden, heeft felgele bloemen en wordt vaak gebruikt in hagen en parken?')), 
    ('Jasmijn',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke struik, veel voorkomend in Limburgse natuurgebieden, heeft felgele bloemen en wordt vaak gebruikt in hagen en parken?')),

-- Answers Question 10
    ('Zwarte els',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort, veel voorkomend in Limburgse beekdalen, staat bekend om zijn smalle bladeren en vormt vaak een belangrijke habitat voor vogels en insecten?')), -- True
    ('Eik',					(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort, veel voorkomend in Limburgse beekdalen, staat bekend om zijn smalle bladeren en vormt vaak een belangrijke habitat voor vogels en insecten?')), 
    ('Berk',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort, veel voorkomend in Limburgse beekdalen, staat bekend om zijn smalle bladeren en vormt vaak een belangrijke habitat voor vogels en insecten?')), 
    ('Populier',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke boomsoort, veel voorkomend in Limburgse beekdalen, staat bekend om zijn smalle bladeren en vormt vaak een belangrijke habitat voor vogels en insecten?')),

-- Answers Question 11
	('Sint-Pietersberg',	(SElECT ID FROM Question WHERE [QuestionText] = 'Welke Limburgse heuvelrug is bekend om zijn glooiende landschap en rijke flora en fauna?')), -- True
    ('Hondsrug',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke Limburgse heuvelrug is bekend om zijn glooiende landschap en rijke flora en fauna?')),
    ('Veluwe',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke Limburgse heuvelrug is bekend om zijn glooiende landschap en rijke flora en fauna?')),
    ('Utrechtse Heuvelrug',	(SElECT ID FROM Question WHERE [QuestionText] = 'Welke Limburgse heuvelrug is bekend om zijn glooiende landschap en rijke flora en fauna?')),

-- Answers Question 12
    ('De Meinweg',			(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van het bekende natuurgebied in Limburg dat bekend staat om zijn kalksteenformaties en unieke flora?')), -- True
    ('De Hoge Veluwe',		(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van het bekende natuurgebied in Limburg dat bekend staat om zijn kalksteenformaties en unieke flora?')),
    ('Sallandse Heuvelrug',	(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van het bekende natuurgebied in Limburg dat bekend staat om zijn kalksteenformaties en unieke flora?')),
    ('De Biesbosch',		(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van het bekende natuurgebied in Limburg dat bekend staat om zijn kalksteenformaties en unieke flora?')),

-- Answers Question 13
    ('Maas',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke rivier stroomt door Limburg en speelt een belangrijke rol in het landschap en de geschiedenis van de regio?')), -- True
    ('Rijn',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke rivier stroomt door Limburg en speelt een belangrijke rol in het landschap en de geschiedenis van de regio?')),
    ('IJssel',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke rivier stroomt door Limburg en speelt een belangrijke rol in het landschap en de geschiedenis van de regio?')),
    ('Schelde',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke rivier stroomt door Limburg en speelt een belangrijke rol in het landschap en de geschiedenis van de regio?')),

-- Answers Question 14
    ('Het Leudal',			(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van de Limburgse bossen die bekend staan om hun biodiversiteit en oude eiken?')), -- True
    ('Het Amsterdamse Bos',	(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van de Limburgse bossen die bekend staan om hun biodiversiteit en oude eiken?')),
    ('Het Amsterdamse Bos',	(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van de Limburgse bossen die bekend staan om hun biodiversiteit en oude eiken?')),
    ('De Veluwezoom',		(SElECT ID FROM Question WHERE [QuestionText] = 'Wat is de naam van de Limburgse bossen die bekend staan om hun biodiversiteit en oude eiken?')),

-- Answers Question 15
    ('Vaalserberg',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke heuvel in Limburg is het hoogste punt van Nederland en biedt een prachtig uitzicht over de omgeving?')), -- True
    ('Gulperberg',			(SElECT ID FROM Question WHERE [QuestionText] = 'Welke heuvel in Limburg is het hoogste punt van Nederland en biedt een prachtig uitzicht over de omgeving?')),
    ('Sint-Pietersberg',	(SElECT ID FROM Question WHERE [QuestionText] = 'Welke heuvel in Limburg is het hoogste punt van Nederland en biedt een prachtig uitzicht over de omgeving?')),
    ('Cauberg',				(SElECT ID FROM Question WHERE [QuestionText] = 'Welke heuvel in Limburg is het hoogste punt van Nederland en biedt een prachtig uitzicht over de omgeving?'));


-- Show tables
select* FROM [User]
select* FROM Game
select* FROM Question
select* FROM Answer
select* FROM Specie
select* FROM Observation
