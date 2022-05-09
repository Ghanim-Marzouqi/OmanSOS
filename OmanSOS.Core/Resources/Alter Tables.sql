ALTER TABLE OmanSOS.dbo.Users 
ADD Location VARCHAR (255) NULL;

ALTER TABLE OmanSOS.dbo.Donations 
ADD LocationId INT NULL;

ALTER TABLE OmanSOS.dbo.Donations 
ADD Remarks VARCHAR (2000) NULL;