/* Use Database */
USE OmanSOS;

/* Insert Master Table Data */
INSERT INTO Categories (Id, Name) VALUES (1, 'Food');
INSERT INTO Categories (Id, Name) VALUES (2, 'Clothes');
INSERT INTO Categories (Id, Name) VALUES (3, 'Money');

INSERT INTO Priorities (Id, Name) VALUES (1, 'Low');
INSERT INTO Priorities (Id, Name) VALUES (2, 'Medium');
INSERT INTO Priorities (Id, Name) VALUES (3, 'High');

INSERT INTO UserTypes (Id, Name) VALUES (1, 'Regular');
INSERT INTO UserTypes (Id, Name) VALUES (2, 'Administrator');

/* Insert Adminstrator */
INSERT INTO Users (NationalId, UserTypeId, Name, Email, Phone) VALUES (22770461, 2, 'Muzna', '16178@email.muscatcollege.edu.om', '91341451');