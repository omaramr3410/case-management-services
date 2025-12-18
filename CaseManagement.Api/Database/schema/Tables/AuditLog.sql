IF NOT EXISTS (
    SELECT 1
    FROM sys.tables
    WHERE name = 'AuditLog'
      AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE dbo.AuditLog
    (
        -- Unique identifier
        Id INT IDENTITY(1,1) PRIMARY KEY,

        -- Who
        UserId UNIQUEIDENTIFIER NULL,
        Username NVARCHAR(100) NULL,
        UserRole NVARCHAR(50) NULL,

        -- What
        EntityName NVARCHAR(100) NOT NULL,
        EntityId NVARCHAR(255) NOT NULL,
        Action NVARCHAR(50) NOT NULL,

        -- When
        TimestampUtc DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

        -- Where
        IpAddress NVARCHAR(45) NULL,

        -- Optional metadata
        Metadata NVARCHAR(MAX) NULL
    )

    CREATE INDEX IX_AuditLog_Entity
        ON dbo.AuditLog (EntityName, EntityId)

    CREATE INDEX IX_AuditLog_Timestamp
        ON dbo.AuditLog (TimestampUtc)
END
