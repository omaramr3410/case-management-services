IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Case')
BEGIN
    CREATE TABLE dbo.[Case] (
        Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
        ClientId UNIQUEIDENTIFIER NOT NULL,
        AssignedOfficerId UNIQUEIDENTIFIER NULL,
        ServiceProviderId UNIQUEIDENTIFIER NULL,
        Status NVARCHAR(50) NOT NULL,
        Region NVARCHAR(50) NOT NULL,
        Recommendations NVARCHAR(MAX) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
        UpdatedAt DATETIME2 NULL,

        CONSTRAINT FK_Case_Client FOREIGN KEY (ClientId) REFERENCES dbo.Client(Id),
        CONSTRAINT FK_Case_Officer FOREIGN KEY (AssignedOfficerId) REFERENCES dbo.Officer(Id),
        CONSTRAINT FK_Case_ServiceProvider FOREIGN KEY (ServiceProviderId) REFERENCES dbo.ServiceProvider(Id)
    );

    CREATE INDEX IX_Case_Status ON dbo.[Case](Status);
    CREATE INDEX IX_Case_Region ON dbo.[Case](Region);
END
