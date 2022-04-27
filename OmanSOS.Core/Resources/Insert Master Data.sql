/* Use Database */
USE OmanSOS;

/* Insert Master Table Data */
INSERT INTO Categories (Id, Name) VALUES (1, 'Food');
INSERT INTO Categories (Id, Name) VALUES (2, 'Clothes');
INSERT INTO Categories (Id, Name) VALUES (3, 'Money');
INSERT INTO Categories (Id, Name) VALUES (4, 'Covid 19');
INSERT INTO Categories (Id, Name) VALUES (5, 'Zakah');
INSERT INTO Categories (Id, Name) VALUES (6, 'Fasting Fidyah');
INSERT INTO Categories (Id, Name) VALUES (7, 'Sadaqa');

INSERT INTO Locations (Id, Name) VALUES (1, 'Muscat');
INSERT INTO Locations (Id, Name) VALUES (2, 'Bosher');
INSERT INTO Locations (Id, Name) VALUES (3, 'Al Seeb');
INSERT INTO Locations (Id, Name) VALUES (4, 'Nizwa');
INSERT INTO Locations (Id, Name) VALUES (5, 'Salalah');

INSERT INTO Priorities (Id, Name) VALUES (1, 'Low');
INSERT INTO Priorities (Id, Name) VALUES (2, 'Medium');
INSERT INTO Priorities (Id, Name) VALUES (3, 'High');
INSERT INTO Priorities (Id, Name) VALUES (4, 'Urgent');

INSERT INTO UserTypes (Id, Name) VALUES (1, 'Regular');
INSERT INTO UserTypes (Id, Name) VALUES (2, 'Administrator');