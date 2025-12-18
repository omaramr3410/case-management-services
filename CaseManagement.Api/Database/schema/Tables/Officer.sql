IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Officer')
BEGIN
    CREATE TABLE dbo.Officer
    (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        UserId UNIQUEIDENTIFIER NOT NULL,

        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Region NVARCHAR(50) NOT NULL,

        CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

        CONSTRAINT FK_Officer_User
            FOREIGN KEY (UserId) REFERENCES dbo.[User](Id)
    );

    CREATE INDEX IX_Officer_UserId ON dbo.Officer(UserId);
    CREATE INDEX IX_Officer_Region ON dbo.Officer(Region);
END
