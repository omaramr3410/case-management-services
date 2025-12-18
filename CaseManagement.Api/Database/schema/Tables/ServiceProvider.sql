IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'ServiceProvider')
BEGIN
    CREATE TABLE dbo.ServiceProvider (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Region NVARCHAR(50) NOT NULL,
        ServiceType NVARCHAR(50) NOT NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
    );

    CREATE INDEX IX_ServiceProvider_Region ON dbo.ServiceProvider(Region);
END
