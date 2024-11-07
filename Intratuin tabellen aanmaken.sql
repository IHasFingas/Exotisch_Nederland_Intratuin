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
	[Description] VARCHAR(MAX) NOT NULL,
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
    [Description] VARCHAR(MAX) NOT NULL,
    Route_ID int NOT NULL,
    FOREIGN KEY (Route_ID) REFERENCES Route(ID));

CREATE TABLE Observation
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    [Name] VARCHAR(50) NOT NULL,
    [Location] VARCHAR(50) NOT NULL,
    [Description] VARCHAR(MAX) NOT NULL,
    Picture VARBINARY(MAX),
    Specie_ID int NOT NULL,
    Area_ID int NOT NULL,
    [User_ID] int NOT NULL,
	Validated bit NOT NULL,
    FOREIGN KEY (Specie_ID) REFERENCES Specie(ID),
    FOREIGN KEY (Area_ID) REFERENCES Area(ID),
    FOREIGN KEY ([User_ID]) REFERENCES [User](ID));

CREATE TABLE Question
    (ID INTEGER NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    QuestionText VARCHAR(MAX) NOT NULL,
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
	Answer_ID int NOT NULL,
    PRIMARY KEY ([User_ID], Question_ID),
    FOREIGN KEY ([User_ID]) REFERENCES [User](ID),
    FOREIGN KEY ([Question_ID]) REFERENCES Question(ID),
	FOREIGN KEY ([Answer_ID]) REFERENCES Answer(ID));

CREATE TABLE RoutePointRoutePoint
	(RoutePoint1_ID int NOT NULL,
	RoutePoint2_ID int NOT NULL,
	Distance float NOT NULL,
	PRIMARY KEY (RoutePoint1_ID, RoutePoint2_ID),
	FOREIGN KEY (RoutePoint1_ID) REFERENCES RoutePoint(ID),
	FOREIGN KEY (RoutePoint2_ID) REFERENCES RoutePoint(ID));


--Enter placeholder data
SET IDENTITY_INSERT Area ON;
INSERT INTO Area(ID, [Name], Size) VALUES (-1, 'Placeholder', 0)
SET IDENTITY_INSERT Area OFF;

SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role](ID, [Name], [Key]) VALUES (-1, 'Placeholder', '')
SET IDENTITY_INSERT [Role] OFF;	

SET IDENTITY_INSERT Specie ON;
INSERT INTO Specie(ID, [Name], Domain, Regnum, Phylum, Classus, Ordo, Familia, Genus) VALUES (-1, 'Placeholder', '', '', '', '', '', '', '');
SET IDENTITY_INSERT Specie OFF;

SET IDENTITY_INSERT RoutePoint ON;
INSERT INTO RoutePoint(ID, [Name], [Location]) VALUES (-1, 'Placeholder', '')
SET IDENTITY_INSERT RoutePoint OFF;

SET IDENTITY_INSERT [Route] ON;
INSERT INTO [Route](ID, [Name], [Length], Area_ID) VALUES (-1, 'Placeholder', 0, -1)
SET IDENTITY_INSERT [Route] OFF;

SET IDENTITY_INSERT [User] ON;
INSERT INTO [User](ID, [Name], Email, CurrentLocation, Route_ID) VALUES (-1, 'Placeholder', '', '', -1)
SET IDENTITY_INSERT [User] OFF;

SET IDENTITY_INSERT Game ON;
INSERT INTO Game(ID, [Name], [Location], [Description], Route_ID) VALUES (-1, 'Placeholder', '', '', -1)
SET IDENTITY_INSERT Game OFF;

SET IDENTITY_INSERT Question ON;
INSERT INTO Question(ID, QuestionText, Game_ID) VALUES (-1, 'Placeholder', -1)
SET IDENTITY_INSERT Question OFF;

--Enter other data
INSERT INTO Area ([Name], Size)
VALUES
	('Brunsummerheide',				600),
	('De Groote Peel',				1340),
	('Nationaal Park Hoge Kempen',	5700);


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
	('Uitkijktoren',						'51.3369, 5.8024'),	-- De Groote Peel
	('Kabouterpad De Pelen',				'51.3272, 5.8010'),	-- De Groote Peel
	('Uitkijktoren Belfort de Vossenberg',	'51.3500, 5.8474'), -- De Groote Peel
	('Puur de Peel',						'51.3642, 5.8016'), -- De Groote Peel
	('Dood Doet Leven',						'51.3364, 5.7917'), -- De Groote Peel
	('Moshut',								'51.3363, 5.8042'), -- De Groote Peel
	('Aan Het Elfde',						'51.3468, 5.8127'), -- De Groote Peel
	('Steltloperven',						'51.3522, 5.8119'), -- De Groote Peel
	('Toegangspoort Mechelse Heide',		'50.9797, 5.6601'),	-- Nationaal Park Hoge Kempen
	('Fietsen door de heide',				'50.9556, 5.6163'),	-- Nationaal Park Hoge Kempen
	('Start Wandelingen De Litzberg',		'51.0051, 5.6530'), -- Nationaal Park Hoge Kempen
	('Meisberg',							'50.9872, 5.6429'), -- Nationaal Park Hoge Kempen
	('Natuurpunt lake outlook',				'51.0010, 5.6674'), -- Nationaal Park Hoge Kempen
	('Voormalige grindgroeve',				'51.0030, 5.6158'); -- Nationaal Park Hoge Kempen


INSERT INTO [Route] ([Name], [Length], Area_ID)
VALUES
	('Peel Trail',		2.3,	(SELECT ID FROM Area WHERE [Name] = 'De Groote Peel')),				-- Between 'Peelzicht' and 'Peelven' in De Groote Peel
	('Kempen Path',		5.6,	(SELECT ID FROM Area WHERE [Name] = 'Nationaal Park Hoge Kempen')),	-- Between 'Kempenbos' and 'Maasvallei' in Nationaal Park Hoge Kempen
	('Heide Walk',		3.1,	(SELECT ID FROM Area WHERE [Name] = 'Brunsummerheide'));			-- Between 'Heidezicht' and 'Schinveldse Bos' in Brunsummerheide


INSERT INTO POI ([Name], [Description], [Location], RoutePoint_ID)
VALUES
    ('Uitkijktoren',					'A tall tower offering expansive views over the surrounding nature reserve.',					(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Uitkijktoren'),					(SELECT ID FROM RoutePoint WHERE [Name] = 'Uitkijktoren')),
    ('Kabouterpad De Pelen',			'A whimsical nature trail designed for children, featuring fun, interactive gnome figures.',	(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Kabouterpad De Pelen'),			(SELECT ID FROM RoutePoint WHERE [Name] = 'Kabouterpad De Pelen')),
    ('Natuurpunt lake outlook',			'A peaceful lookout point over a lake, ideal for birdwatching and quiet reflection.',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Natuurpunt lake outlook'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Natuurpunt lake outlook')),
    ('Voormalige grindgroeve',			'The site of a former gravel quarry, now a serene nature spot with unique terrain.',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Voormalige grindgroeve'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Voormalige grindgroeve')),
    ('Uitkijkpunt Brunssummerheide',	'Scenic viewpoint over the heather fields and forests of Brunsummerheide.',						(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Uitkijkpunt Brunssummerheide'),	(SELECT ID FROM RoutePoint WHERE [Name] = 'Uitkijkpunt Brunssummerheide')),
    ('Bron van de Roode beek',			'The tranquil source of the Roode Beek, nestled in lush surroundings.',							(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Bron van de Roode beek'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Bron van de Roode beek'));


INSERT INTO [User] ([Name], Email, CurrentLocation, Route_ID)
VALUES 
    ('Jan Janssen',		'jan.janssen@example.com',		'50.8513, 5.6909',	(SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path')),
	('Piet Meijer',		'piet.meijer@example.com',		'',					(SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail')),
    ('Els van Dijk',	'els.vandijk@example.com',		'50.8787, 5.9805',	(SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk')),  
    ('Karla Hermans',	'karla.hermans@example.com',	'50.9849, 5.9704',	(SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk')),
    ('Tom Peters',		'tom.peters@example.com',		'51.1582, 5.8361',	(SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail')),  
    ('Sanne Smit',		'sanne.smit@example.com',		'51.1460, 5.8129',	(SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path'));


INSERT INTO Game ([Name], [Location], [Description], Route_ID)
VALUES
	('Dieren Quiz',			(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Kabouterpad De Pelen'),		'Kies het juiste antwoord op de vraag; dieren editie!',			(SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail')),
	('Planten Quiz',		(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Natuurpunt lake outlook'),	'Kies het juiste antwoord op de vraag; planten editie!',		(SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path')),
	('Lanschappen Quiz',	(SELECT [Location] FROM RoutePoint WHERE [Name] = 'Bron van de Roode Beek'),	'Kies het juiste antwoord op de vraag; landschappen editie!',	(SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk'));


INSERT INTO Observation ([Name], [Location], [Description], Picture, Specie_ID, Area_ID, [User_ID], Validated)
VALUES 
	('Paardenbloem',		'50.8503, 5.6900',	'Deze bloem heeft diep ingesneden, groene bladeren die dicht bij de grond groeien. Het heeft heldergele bloemen op lange, stevige stelen.',										0001, (SELECT ID FROM Specie WHERE [Name] = 'Paardenbloem'),	(SELECT ID FROM Area WHERE [Name] = 'Brunsummerheide'),				(SELECT ID FROM [User] WHERE [Name] = 'Els van Dijk'),	0),
	('Haas',				'',					'Deze haas heeft een lichtbruine vacht met een witachtige onderkant. Zijn lange oren staan rechtop en zijn ogen zijn groot en zwart, waardoor hij altijd alert is op gevaar.',	0002, (SELECT ID FROM Specie WHERE [Name] = 'Haas'),			(SELECT ID FROM Area WHERE [Name] = 'Nationaal Park Hoge Kempen'),	(SELECT ID FROM [User] WHERE [Name] = 'Jan Janssen'),	0),
	('Berenklauw',			'51.0543, 5.1815',	'Deze plant heeft grote, handvormige bladeren en lange, holle stelen met witte bloemen die in een schermvormige bloeiwijze groeien.',											0003, (SELECT ID FROM Specie WHERE [Name] = 'Berenklauw'),		(SELECT ID FROM Area WHERE [Name] = 'De Groote Peel'),				(SELECT ID FROM [User] WHERE [Name] = 'Tom Peters'),	0);


INSERT INTO Question (QuestionText, Game_ID)
VALUES 
-- Questions Game 1
    ('Welke van de volgende dieren, die je vaak in de Limburgse bossen kunt vinden, is bekend om zijn felrode staart en eet graag noten?',								1),
    ('Wat voor soort dier is de ree, bekend om zijn lange poten en grote ogen?',							1),
    ('Welke van de volgende vogels zie je vaak in Limburg en staat bekend om zijn heldere, blauwe kleur en zijn acrobatische vliegkunsten?',							1),
    ('Wat voor soort dier is een adelaar, bekend om zijn scherpe klauwen en uitstekende gezichtsvermogen?',													1),
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
    ('Zoogdier',			(SElECT ID FROM Question WHERE [ID] = '2'), 1), 
    ('Vogel',				(SElECT ID FROM Question WHERE [ID] = '2'), 0),
    ('Reptiel',				(SElECT ID FROM Question WHERE [ID] = '2'), 0),
    ('Amfibie',				(SElECT ID FROM Question WHERE [ID] = '2'), 0),

-- Answers Question 3
    ('IJsvogel',			(SElECT ID FROM Question WHERE [ID] = '3'), 1), 
    ('Merel',				(SElECT ID FROM Question WHERE [ID] = '3'), 0),
    ('Ekster',				(SElECT ID FROM Question WHERE [ID] = '3'), 0),
    ('Specht',				(SElECT ID FROM Question WHERE [ID] = '3'), 0),

-- Answers Question 4
    ('Vogel',				(SElECT ID FROM Question WHERE [ID] = '4'), 1), 
    ('Reptiel',				(SElECT ID FROM Question WHERE [ID] = '4'), 0),
    ('Amfibie',				(SElECT ID FROM Question WHERE [ID] = '4'), 0),
    ('Zoogdier',			(SElECT ID FROM Question WHERE [ID] = '4'), 0),

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
	((SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Uitkijkpunt Brunssummerheide')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Heide Walk'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Bron van de Roode beek')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Uitkijktoren')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Peel Trail'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Kabouterpad De Pelen')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Natuurpunt lake outlook')),
	((SELECT ID FROM [Route] WHERE [Name] = 'Kempen Path'),		(SELECT ID FROM RoutePoint WHERE [Name] = 'Voormalige grindgroeve'));


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


INSERT INTO UserQuestion ([User_ID], Question_ID, Answer_ID)
VALUES
    -- Linking Jan Janssen to two random questions
    ((SELECT ID FROM [User] WHERE [Name] = 'Jan Janssen'), (SELECT ID FROM Question WHERE [ID] = 1), (SELECT ID FROM Answer WHERE Question_ID = 1 AND CorrectAnswer = 1)),
    ((SELECT ID FROM [User] WHERE [Name] = 'Jan Janssen'), (SELECT ID FROM Question WHERE [ID] = 4), (SELECT ID FROM Answer WHERE Question_ID = 4 AND CorrectAnswer = 1)),

    -- Linking Piet Meijer to one question
    ((SELECT ID FROM [User] WHERE [Name] = 'Piet Meijer'), (SELECT ID FROM Question WHERE [ID] = 2), (SELECT ID FROM Answer WHERE Question_ID = 2 AND CorrectAnswer = 1)),

    -- Linking Els van Dijk to two random questions
    ((SELECT ID FROM [User] WHERE [Name] = 'Els van Dijk'), (SELECT ID FROM Question WHERE [ID] = 3), (SELECT ID FROM Answer WHERE Question_ID = 3 AND CorrectAnswer = 1)),
    ((SELECT ID FROM [User] WHERE [Name] = 'Els van Dijk'), (SELECT ID FROM Question WHERE [ID] = 5), (SELECT ID FROM Answer WHERE Question_ID = 5 AND CorrectAnswer = 1)),

    -- Linking Karla Hermans to one question
    ((SELECT ID FROM [User] WHERE [Name] = 'Karla Hermans'), (SELECT ID FROM Question WHERE [ID] = 6), (SELECT ID FROM Answer WHERE Question_ID = 5 AND CorrectAnswer = 1)),

    -- Linking Tom Peters to one question
    ((SELECT ID FROM [User] WHERE [Name] = 'Tom Peters'), (SELECT ID FROM Question WHERE [ID] = 7), (SELECT ID FROM Answer WHERE Question_ID = 7 AND CorrectAnswer = 1)),

    -- Linking Sanne Smit to two random questions
    ((SELECT ID FROM [User] WHERE [Name] = 'Sanne Smit'), (SELECT ID FROM Question WHERE [ID] = 8), (SELECT ID FROM Answer WHERE Question_ID = 8 AND CorrectAnswer = 1)),
    ((SELECT ID FROM [User] WHERE [Name] = 'Sanne Smit'), (SELECT ID FROM Question WHERE [ID] = 9), (SELECT ID FROM Answer WHERE Question_ID = 9 AND CorrectAnswer = 1));


INSERT INTO RoutePointRoutePoint (RoutePoint1_ID, RoutePoint2_ID, Distance)
VALUES
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 2), 0.25),
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 6), 1.8),
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 7), 0.8),
	((SELECT ID FROM RoutePoint WHERE ID = 1), (SELECT ID FROM RoutePoint WHERE ID = 10), 1.5),
	((SELECT ID FROM RoutePoint WHERE ID = 2), (SELECT ID FROM RoutePoint WHERE ID = 4), 1.7),
	((SELECT ID FROM RoutePoint WHERE ID = 2), (SELECT ID FROM RoutePoint WHERE ID = 5), 2.2),
	((SELECT ID FROM RoutePoint WHERE ID = 2), (SELECT ID FROM RoutePoint WHERE ID = 8), 1.4),
	((SELECT ID FROM RoutePoint WHERE ID = 3), (SELECT ID FROM RoutePoint WHERE ID = 4), 0.65),
	((SELECT ID FROM RoutePoint WHERE ID = 3), (SELECT ID FROM RoutePoint WHERE ID = 8), 0.75),
	((SELECT ID FROM RoutePoint WHERE ID = 4), (SELECT ID FROM RoutePoint WHERE ID = 5), 2.4),
	((SELECT ID FROM RoutePoint WHERE ID = 5), (SELECT ID FROM RoutePoint WHERE ID = 10), 0.65),
	((SELECT ID FROM RoutePoint WHERE ID = 6), (SELECT ID FROM RoutePoint WHERE ID = 7), 1.1),
	((SELECT ID FROM RoutePoint WHERE ID = 6), (SELECT ID FROM RoutePoint WHERE ID = 8), 1.3),
	((SELECT ID FROM RoutePoint WHERE ID = 6), (SELECT ID FROM RoutePoint WHERE ID = 9), 0.85),
	((SELECT ID FROM RoutePoint WHERE ID = 7), (SELECT ID FROM RoutePoint WHERE ID = 9), 0.24),

	((SELECT ID FROM RoutePoint WHERE ID = 11), (SELECT ID FROM RoutePoint WHERE ID = 12), 1.1),
	((SELECT ID FROM RoutePoint WHERE ID = 11), (SELECT ID FROM RoutePoint WHERE ID = 15), 1.2),
	((SELECT ID FROM RoutePoint WHERE ID = 11), (SELECT ID FROM RoutePoint WHERE ID = 16), 0.03), --lol
	((SELECT ID FROM RoutePoint WHERE ID = 12), (SELECT ID FROM RoutePoint WHERE ID = 13), 5.1),
	((SELECT ID FROM RoutePoint WHERE ID = 12), (SELECT ID FROM RoutePoint WHERE ID = 15), 1.8),
	((SELECT ID FROM RoutePoint WHERE ID = 13), (SELECT ID FROM RoutePoint WHERE ID = 14), 5.3),
	((SELECT ID FROM RoutePoint WHERE ID = 13), (SELECT ID FROM RoutePoint WHERE ID = 17), 2.9),
	((SELECT ID FROM RoutePoint WHERE ID = 14), (SELECT ID FROM RoutePoint WHERE ID = 15), 4.7),
	((SELECT ID FROM RoutePoint WHERE ID = 14), (SELECT ID FROM RoutePoint WHERE ID = 18), 1.8),
	((SELECT ID FROM RoutePoint WHERE ID = 16), (SELECT ID FROM RoutePoint WHERE ID = 17), 2.4),
	((SELECT ID FROM RoutePoint WHERE ID = 16), (SELECT ID FROM RoutePoint WHERE ID = 18), 2.9),
	((SELECT ID FROM RoutePoint WHERE ID = 17), (SELECT ID FROM RoutePoint WHERE ID = 18), 2),

	((SELECT ID FROM RoutePoint WHERE ID = 19), (SELECT ID FROM RoutePoint WHERE ID = 20), 5.6),
	((SELECT ID FROM RoutePoint WHERE ID = 19), (SELECT ID FROM RoutePoint WHERE ID = 22), 1.7),
	((SELECT ID FROM RoutePoint WHERE ID = 20), (SELECT ID FROM RoutePoint WHERE ID = 22), 4.8),
	((SELECT ID FROM RoutePoint WHERE ID = 21), (SELECT ID FROM RoutePoint WHERE ID = 22), 3.1),
	((SELECT ID FROM RoutePoint WHERE ID = 21), (SELECT ID FROM RoutePoint WHERE ID = 23), 4.9),
	((SELECT ID FROM RoutePoint WHERE ID = 21), (SELECT ID FROM RoutePoint WHERE ID = 24), 4.5),
	((SELECT ID FROM RoutePoint WHERE ID = 22), (SELECT ID FROM RoutePoint WHERE ID = 23), 7.1),
	((SELECT ID FROM RoutePoint WHERE ID = 22), (SELECT ID FROM RoutePoint WHERE ID = 24), 3.9);


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