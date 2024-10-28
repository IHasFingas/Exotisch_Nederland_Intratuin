USE [Intratuin]

--Delete tables
DROP TABLE IF EXISTS RoutePointRoutePoint;
DROP TABLE IF EXISTS UserQuestion;
DROP TABLE IF EXISTS RouteRoutePoint;
DROP TABLE IF EXISTS UserRole;

DROP TABLE IF EXISTS Answer;

DROP TABLE IF EXISTS Question;
DROP TABLE IF EXISTS Observation;

DROP TABLE IF EXISTS Game;
DROP TABLE IF EXISTS [User];

DROP TABLE IF EXISTS POI;
DROP TABLE IF EXISTS [Route];

DROP TABLE IF EXISTS RoutePoint;
DROP TABLE IF EXISTS Specie;
DROP TABLE IF EXISTS [Role];
DROP TABLE IF EXISTS Area;


--Create tables
CREATE TABLE Area
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Size FLOAT NOT NULL);

CREATE TABLE [Role]
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Key] VARCHAR(50) NOT NULL);

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

CREATE TABLE RoutePoint
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50) NOT NULL);

CREATE TABLE [Route]
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Length] FLOAT NOT NULL,
    Area_ID int NOT NULL,
    FOREIGN KEY (Area_ID) REFERENCES Area(ID));

CREATE TABLE POI
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50) NOT NULL,
    RoutePoint_ID int NOT NULL,
    FOREIGN KEY (RoutePoint_ID) REFERENCES RoutePoint(ID));

CREATE TABLE [User]
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    CurrentLocation VARCHAR(50) NOT NULL,
    Route_ID int NOT NULL,
    FOREIGN KEY (Route_ID) REFERENCES Route(ID));

CREATE TABLE Game
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    Route_ID int NOT NULL,
    FOREIGN KEY (Route_ID) REFERENCES Route(ID));

CREATE TABLE Observation
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    Picture VARBINARY(MAX),
    Specie_ID int NOT NULL,
    Area_ID int NOT NULL,
    [User_ID] int NOT NULL,
    FOREIGN KEY (Specie_ID) REFERENCES Specie(ID),
    FOREIGN KEY (Area_ID) REFERENCES Area(ID),
    FOREIGN KEY ([User_ID]) REFERENCES [User](ID));

CREATE TABLE Question
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    QuestionText VARCHAR(255) NOT NULL,
    Game_ID int NOT NULL,
    FOREIGN KEY (Game_ID) REFERENCES Game(ID));

CREATE TABLE Answer
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    AnswerText VARCHAR(50) NOT NULL,
    Question_ID int NOT NULL,
	CorrectAnswer bit NOT NULL,
    FOREIGN KEY (Question_ID) REFERENCES Question(ID));

CREATE TABLE UserRole
    ([User_ID] int NOT NULL,
    Role_ID int NOT NULL,
    PRIMARY KEY ([User_ID], Role_ID),
    FOREIGN KEY ([User_ID]) REFERENCES [User](ID),
    FOREIGN KEY ([Role_ID]) REFERENCES Role(ID));

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

CREATE TABLE RoutePointRoutePoint
	(RoutePoint1_ID int NOT NULL,
	RoutePoint2_ID int NOT NULL,
	Distance float NOT NULL,
	PRIMARY KEY (RoutePoint1_ID, RoutePoint2_ID),
	FOREIGN KEY (RoutePoint1_ID) REFERENCES RoutePoint(ID),
	FOREIGN KEY (RoutePoint2_ID) REFERENCES RoutePoint(ID));



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
	('Hiker',			'0000000000'),
	('Volunteer',		'gT9cR2vLmD'),
	('Validator',		'Zk3qY7sHpB');


INSERT INTO Specie ([Name], Domain, Regnum, Phylum, Classus, Ordo, Familia, Genus) 
VALUES
-- Plants
	('Eikenboom',			'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Rosales',			'Fagaceae',		'Quercus'),
	('Beuk',				'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Fagales',			'Fagaceae',		'Fagus'),
	('Edelweiss',			'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Asterales',		'Asteraceae',	'Leontopodium'),
	('Brandnetel',			'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Rosales',			'Urticaceae',	'Urtica'),
	('Zevenblad',			'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Apiales',			'Apiaceae',		'Aegopodium'),
	('Berenklauw',			'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Apiales',			'Apiaceae',		'Heracleum'),
	('Hondsdraf',			'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Lamiales',		'Lamiaceae',	'Glechoma'),
	('Paardenbloem',		'Eukarya', 'Plantae', 'Angiosperms', 'Eudicots', 'Asterales',		'Asteraceae',	'Taraxacum'),

-- Animals
	('Gewone Vos',			'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Carnivora',		'Canidae',		'Vulpes'),
	('Eurasische Egel',		'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Eulipotyphla',	'Erinaceidae',	'Erinaceus'),
	('Roodborst',			'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Passeriformes',	'Muscicapidae', 'Erithacus'),
	('Zilverreiger',		'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Pelecaniformes',	'Ardeidae',		'Ardea'),
	('Groene Specht',		'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Piciformes',		'Picidae',		'Picus'),
	('Haas',				'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Lagomorpha',		'Leporidae',	'Lepus'),
	('Veldmuis',			'Eukarya', 'Animalia', 'Chordata', 'Mammalia',	 'Rodentia',		'Cricetidae',	'Microtus'),
	('Zwarte Kraai',		'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Passeriformes',	'Corvidae',		'Corvus'),
	('Grote Kamsalamander',	'Eukarya', 'Animalia', 'Chordata', 'Amphibia',	 'Urodela',			'Salamandridae','Triturus'),
	('Grote Bonte Specht',	'Eukarya', 'Animalia', 'Chordata', 'Aves',		 'Piciformes',		'Picidae',		'Dendrocopos');


INSERT INTO RoutePoint ([Name], [Location])
VALUES
	('Peelzicht',							'51.3412, 5.8743'),	-- De Groote Peel
	('Peelven',								'51.3456, 5.8801'),	-- De Groote Peel
	('Uitkijktoren ''t Elfde',				'51.3443, 5.8292'), -- De Groote Peel
	('De Pelen Bezoekerscentrum',			'51.3447, 5.8305'), -- De Groote Peel
	('Knuppelbrugpad',						'51.3461, 5.8342'), -- De Groote Peel
	('Kwijtweg Zandvlakte',					'51.3478, 5.8289'), -- De Groote Peel
	('Molentje Peelven',					'51.3490, 5.8320'), -- De Groote Peel
	('Heidestruik Vlakte',					'51.3485, 5.8373'), -- De Groote Peel
	('Het Peelkanaal',						'51.3459, 5.8356'), -- De Groote Peel
	('Uitkijkpunt Het Beuven',				'51.3420, 5.8302'), -- De Groote Peel
	('Kempenbos',							'50.9745, 5.6931'),	-- Nationaal Park Hoge Kempen
	('Maasvallei',							'50.9787, 5.7456'),	-- Nationaal Park Hoge Kempen
	('Uitkijkpunt Brunssummerheide',		'50.9294, 5.9978'),	-- Brunssummerheide
	('Bron van de Roode beek',				'50.9283, 5.9969'),	-- Brunssummerheide
	('Restaurant Schrieversheide',			'50.9231, 5.9809'), -- Brunssummerheide
	('Hondenuitlaatplek',					'50.9189, 5.9814'), -- Brunssummerheide
	('Monument Nicky Verstappen',			'50.9198, 6.0120'), -- Brunssummerheide
	('Parkeerplaats Koffiepoel',			'50.9339, 5.9883'), -- Brunssummerheide
	('Manege Brunssummerheide',				'50.9351, 5.9956'), -- Brunssummerheide
	('Hoogste punt Brunssummerheide',		'50.9271, 5.9793'), -- Brunssummerheide
	('ICC/Gen. Eisenhower Hotel',			'50.9372, 5.9941'), -- Brunssummerheide
	('Parkeren Brunssummerheide-Oost',		'50.9251, 6.0140'), -- Brunssummerheide
	('Leubeek',								'51.2534, 5.9712'),	-- Het Leudal
	('Exaten',								'51.2608, 5.9825'),	-- Het Leudal
	('Leumolen',							'51.2561, 5.9372'), -- Het Leudal
	('Leudal Bezoekerscentrum',				'51.2591, 5.9386'), -- Het Leudal
	('St. Ursula Kapel',					'51.2547, 5.9318'), -- Het Leudal
	('Watermolenweg',						'51.2560, 5.9331'), -- Het Leudal
	('Leudalbos Wandelgebied',				'51.2584, 5.9345'), -- Het Leudal
	('Kasteel Groot Buggenum',				'51.2507, 5.9341'), -- Het Leudal
	('Roggelse Beek',						'51.2572, 5.9394'), -- Het Leudal
	('Veldkruis bij Sint Servaaskapel',		'51.2532, 5.9358'), -- Het Leudal
	('Elfenmeer',							'51.1476, 6.0562'),	-- De Meinweg
	('Rolvennen',							'51.1613, 6.0724');	-- De Meinweg


INSERT INTO [Route] ([Name], [Length], Area_ID)
VALUES
	('Peel Trail',		2.3,	(SELECT ID FROM Area WHERE [Name] = 'De Groote Peel')),				-- Between 'Peelzicht' and 'Peelven' in De Groote Peel
	('Kempen Path',		5.6,	(SELECT ID FROM Area WHERE [Name] = 'Nationaal Park Hoge Kempen')),	-- Between 'Kempenbos' and 'Maasvallei' in Nationaal Park Hoge Kempen
	('Heide Walk',		3.1,	(SELECT ID FROM Area WHERE [Name] = 'Brunsummerheide')),			-- Between 'Heidezicht' and 'Schinveldse Bos' in Brunsummerheide
	('Leudal Loop',		4.8,	(SELECT ID FROM Area WHERE [Name] = 'Het Leudal')),					-- Between 'Leubeek' and 'Exaten' in Het Leudal
	('Meinweg Ridge',	2.7,	(SELECT ID FROM Area WHERE [Name] = 'De Meinweg'));					-- Between 'Elfenmeer' and 'Rolvennen' in De Meinweg


INSERT INTO POI ([Name], [Location], RoutePoint_ID)
VALUES
	('Peel Observation Tower',		(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Peelzicht'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelzicht')),
	('Peelven Birdwatch',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Peelven'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelven')),
	('Kempen Forest View',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Kempenbos'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Kempenbos')),
	('Maas River Lookout',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Maasvallei'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Maasvallei')),
	('Uitkijkpunt Brunssummerheide',				(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Uitkijkpunt Brunssummerheide'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Uitkijkpunt Brunssummerheide')),
	('Bron van de Roode beekr',	(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Bron van de Roode beek'),	(SELECT ID FROM RoutePoint WHERE [Name] = 'Bron van de Roode beek')),
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
	('Paardenbloem',		'50.8503, 5.6900',	'Deze bloem heeft diep ingesneden, groene bladeren die dicht bij de grond groeien. Het heeft heldergele bloemen op lange, stevige stelen.',										0001, (SELECT ID FROM Specie WHERE [Name] = 'Paardenbloem'),	(SELECT ID FROM Area WHERE [Name] = 'Brunsummerheide'),				(SELECT ID FROM [User] WHERE [Name] = 'Els van Dijk')),
	('Haas',				'',					'Deze haas heeft een lichtbruine vacht met een witachtige onderkant. Zijn lange oren staan rechtop en zijn ogen zijn groot en zwart, waardoor hij altijd alert is op gevaar.',	0002, (SELECT ID FROM Specie WHERE [Name] = 'Haas'),			(SELECT ID FROM Area WHERE [Name] = 'Nationaal Park Hoge Kempen'),	(SELECT ID FROM [User] WHERE [Name] = 'Jan Janssen')),
	('Berenklauw',			'51.0543, 5.1815',	'Deze plant heeft grote, handvormige bladeren en lange, holle stelen met witte bloemen die in een schermvormige bloeiwijze groeien.',											0003, (SELECT ID FROM Specie WHERE [Name] = 'Berenklauw'),		(SELECT ID FROM Area WHERE [Name] = 'De Meinweg'),					(SELECT ID FROM [User] WHERE [Name] = 'Tom Peters'));


INSERT INTO Question (QuestionText, Game_ID)
VALUES 
-- Questions Game 1
    ('Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en eet graag noten?',								1),
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


INSERT INTO Answer (AnswerText, Question_ID, CorrectAnswer)
VALUES 
-- Answers Question 1
    ('Eekhoorn',			(SElECT ID FROM Question WHERE [ID] = '1'), 1), 
    ('Das',					(SElECT ID FROM Question WHERE [ID] = '1'), 0),
    ('Vos',					(SElECT ID FROM Question WHERE [ID] = '1'), 0),
    ('Konijn',				(SElECT ID FROM Question WHERE [ID] = '1'), 0),

-- Answers Question 2
    ('Ree',					(SElECT ID FROM Question WHERE [ID] = '2'), 1), 
    ('Hert',				(SElECT ID FROM Question WHERE [ID] = '2'), 0),
    ('Haas',				(SElECT ID FROM Question WHERE [ID] = '2'), 0),
    ('Wild zwijn',			(SElECT ID FROM Question WHERE [ID] = '2'), 0),

-- Answers Question 3
    ('IJsvogel',			(SElECT ID FROM Question WHERE [ID] = '3'), 1), 
    ('Merel',				(SElECT ID FROM Question WHERE [ID] = '3'), 0),
    ('Ekster',				(SElECT ID FROM Question WHERE [ID] = '3'), 0),
    ('Specht',				(SElECT ID FROM Question WHERE [ID] = '3'), 0),

-- Answers Question 4
    ('Vos',					(SElECT ID FROM Question WHERE [ID] = '4'), 1), 
    ('Wolf',				(SElECT ID FROM Question WHERE [ID] = '4'), 0),
    ('Lynx',				(SElECT ID FROM Question WHERE [ID] = '4'), 0),
    ('Wasbeerhond',			(SElECT ID FROM Question WHERE [ID] = '4'), 0),

-- Answers Question 5
    ('Kikker',				(SElECT ID FROM Question WHERE [ID] = '5'), 1), 
    ('Pad',					(SElECT ID FROM Question WHERE [ID] = '5'), 0),
    ('Salamander',			(SElECT ID FROM Question WHERE [ID] = '5'), 0),
    ('Waterslang',			(SElECT ID FROM Question WHERE [ID] = '5'), 0),

-- Answers Question 6
	('Bosanemoon',			(SElECT ID FROM Question WHERE [ID] = '6'), 2), 
    ('Zonnebloem',			(SElECT ID FROM Question WHERE [ID] = '6'), 0), 
    ('Narcis',				(SElECT ID FROM Question WHERE [ID] = '6'), 0), 
    ('Klaproos',			(SElECT ID FROM Question WHERE [ID] = '6'), 0),

-- Answers Question 7
    ('Tamme kastanje',		(SElECT ID FROM Question WHERE [ID] = '7'), 1), 
    ('Esdoorn',				(SElECT ID FROM Question WHERE [ID] = '7'), 0), 
    ('Beuk',				(SElECT ID FROM Question WHERE [ID] = '7'), 0), 
    ('Wilg',				(SElECT ID FROM Question WHERE [ID] = '7'), 0),

-- Answers Question 8
    ('Wilde tijm',			(SElECT ID FROM Question WHERE [ID] = '8'), 1), 
    ('Lavendel',			(SElECT ID FROM Question WHERE [ID] = '8'), 0), 
    ('Blauwe druifjes',		(SElECT ID FROM Question WHERE [ID] = '8'), 0), 
    ('Paardenbloem',		(SElECT ID FROM Question WHERE [ID] = '8'), 0),

-- Answers Question 9
    ('Brem',				(SElECT ID FROM Question WHERE [ID] = '9'), 1), 
    ('Forsythia',			(SElECT ID FROM Question WHERE [ID] = '9'), 0), 
    ('Rododendron',			(SElECT ID FROM Question WHERE [ID] = '9'), 0), 
    ('Jasmijn',				(SElECT ID FROM Question WHERE [ID] = '9'), 0),

-- Answers Question 10
    ('Zwarte els',			(SElECT ID FROM Question WHERE [ID] = '10'), 1), 
    ('Eik',					(SElECT ID FROM Question WHERE [ID] = '10'), 0), 
    ('Berk',				(SElECT ID FROM Question WHERE [ID] = '10'), 0), 
    ('Populier',			(SElECT ID FROM Question WHERE [ID] = '10'), 0),

-- Answers Question 11
	('Sint-Pietersberg',	(SElECT ID FROM Question WHERE [ID] = '11'), 1), 
    ('Hondsrug',			(SElECT ID FROM Question WHERE [ID] = '11'), 0),
    ('Veluwe',				(SElECT ID FROM Question WHERE [ID] = '11'), 0),
    ('Utrechtse Heuvelrug',	(SElECT ID FROM Question WHERE [ID] = '11'), 0),

-- Answers Question 12
    ('De Meinweg',			(SElECT ID FROM Question WHERE [ID] = '12'), 1), 
    ('De Hoge Veluwe',		(SElECT ID FROM Question WHERE [ID] = '12'), 0),
    ('Sallandse Heuvelrug',	(SElECT ID FROM Question WHERE [ID] = '12'), 0),
    ('De Biesbosch',		(SElECT ID FROM Question WHERE [ID] = '12'), 0),

-- Answers Question 13
    ('Maas',				(SElECT ID FROM Question WHERE [ID] = '13'), 1), 
    ('Rijn',				(SElECT ID FROM Question WHERE [ID] = '13'), 0),
    ('IJssel',				(SElECT ID FROM Question WHERE [ID] = '13'), 0),
    ('Schelde',				(SElECT ID FROM Question WHERE [ID] = '13'), 0),

-- Answers Question 14
    ('Het Leudal',			(SElECT ID FROM Question WHERE [ID] = '14'), 1), 
    ('Het Amsterdamse Bos',	(SElECT ID FROM Question WHERE [ID] = '14'), 0),
    ('Het Amsterdamse Bos',	(SElECT ID FROM Question WHERE [ID] = '14'), 0),
    ('De Veluwezoom',		(SElECT ID FROM Question WHERE [ID] = '14'), 0),

-- Answers Question 15
    ('Vaalserberg',			(SElECT ID FROM Question WHERE [ID] = '15'), 1), 
    ('Gulperberg',			(SElECT ID FROM Question WHERE [ID] = '15'), 0),
    ('Sint-Pietersberg',	(SElECT ID FROM Question WHERE [ID] = '15'), 0),
    ('Cauberg',				(SElECT ID FROM Question WHERE [ID] = '15'), 0);

INSERT INTO RouteRoutePoint (Route_ID, RoutePoint_ID)
VALUES
	((SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelzicht')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Peelven')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Kempenbos')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Maasvallei')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Uitkijkpunt Brunssummerheide')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Bron van de Roode beek')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Leudal Loop'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Leubeek')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Meinweg Ridge'),	(SELECT ID FROM RoutePoint WHERE [Name] = 'Exaten')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Meinweg Ridge'),	(SELECT ID FROM RoutePoint WHERE [Name] = 'Rolvennen'));

INSERT INTO UserRole ([User_ID], Role_ID)
VALUES
    -- Jan Janssen is a Hiker and a Volunteer
    ((SELECT ID FROM [User] WHERE [Name] = 'Jan Janssen'), (SELECT ID FROM [Role] WHERE [Name] = 'Hiker')),
    ((SELECT ID FROM [User] WHERE [Name] = 'Jan Janssen'), (SELECT ID FROM [Role] WHERE [Name] = 'Volunteer')),

    -- Piet Meijer is a Validator
    ((SELECT ID FROM [User] WHERE [Name] = 'Piet Meijer'), (SELECT ID FROM [Role] WHERE [Name] = 'Validator')),

    -- Els van Dijk is a Hiker
    ((SELECT ID FROM [User] WHERE [Name] = 'Els van Dijk'), (SELECT ID FROM [Role] WHERE [Name] = 'Hiker')),

    -- Karla Hermans is a Hiker and a Validator
    ((SELECT ID FROM [User] WHERE [Name] = 'Karla Hermans'), (SELECT ID FROM [Role] WHERE [Name] = 'Hiker')),
    ((SELECT ID FROM [User] WHERE [Name] = 'Karla Hermans'), (SELECT ID FROM [Role] WHERE [Name] = 'Validator')),

    -- Tom Peters is a Volunteer
    ((SELECT ID FROM [User] WHERE [Name] = 'Tom Peters'), (SELECT ID FROM [Role] WHERE [Name] = 'Volunteer')),

    -- Sanne Smit is a Hiker and a Volunteer
    ((SELECT ID FROM [User] WHERE [Name] = 'Sanne Smit'), (SELECT ID FROM [Role] WHERE [Name] = 'Hiker')),
    ((SELECT ID FROM [User] WHERE [Name] = 'Sanne Smit'), (SELECT ID FROM [Role] WHERE [Name] = 'Volunteer'));


INSERT INTO RoutePointRoutePoint (RoutePoint1_ID, RoutePoint2_ID, Distance)
VALUES
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 2), 0.14),
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 6), 0.83),
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 7), 0.65),
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 10), 1.23),
	((SELECT ID FROM RoutePoint WHERE ID = 2), (SELECT ID FROM RoutePoint WHERE ID = 4), 1.51),
	((SELECT ID FROM RoutePoint WHERE ID = 2), (SELECT ID FROM RoutePoint WHERE ID = 5), 1.42),
	((SELECT ID FROM RoutePoint WHERE ID = 2), (SELECT ID FROM RoutePoint WHERE ID = 8), 1.24),
	((SELECT ID FROM RoutePoint WHERE ID = 3), (SELECT ID FROM RoutePoint WHERE ID = 4), 0.47),
	((SELECT ID FROM RoutePoint WHERE ID = 3), (SELECT ID FROM RoutePoint WHERE ID = 8), 0.46),
	((SELECT ID FROM RoutePoint WHERE ID = 4), (SELECT ID FROM RoutePoint WHERE ID = 5), 2.15),
	((SELECT ID FROM RoutePoint WHERE ID = 5), (SELECT ID FROM RoutePoint WHERE ID = 10), 0.6),
	((SELECT ID FROM RoutePoint WHERE ID = 6), (SELECT ID FROM RoutePoint WHERE ID = 7), 0.53),
	((SELECT ID FROM RoutePoint WHERE ID = 6), (SELECT ID FROM RoutePoint WHERE ID = 8), 0.98),
	((SELECT ID FROM RoutePoint WHERE ID = 6), (SELECT ID FROM RoutePoint WHERE ID = 9), 0.55),
	((SELECT ID FROM RoutePoint WHERE ID = 7), (SELECT ID FROM RoutePoint WHERE ID = 9), 0.26);


-- Show tables
SELECT * FROM Area
SELECT * FROM [Role]
SELECT * FROM Specie
SELECT * FROM RoutePoint
SELECT * FROM [Route]
SELECT * FROM POI
SELECT * FROM [User]
SELECT * FROM Game
SELECT * FROM Observation
SELECT * FROM Question
SELECT * FROM Answer

SELECT * FROM UserRole
SELECT * FROM RouteRoutePoint
SELECT * FROM UserQuestion
SELECT * FROM RoutePointRoutePoint