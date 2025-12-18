IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Client')
BEGIN
    CREATE TABLE dbo.Client (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Region NVARCHAR(50) NOT NULL,
        Status NVARCHAR(50) NOT NULL,
        SSN NVARCHAR(11) NOT NULL,
        DateOfBirth DATE NOT NULL,
        Phone NVARCHAR(50) NOT NULL,
        Address NVARCHAR(50) NOT NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );

    CREATE INDEX IX_Client_LastName ON dbo.Client(LastName);
END
