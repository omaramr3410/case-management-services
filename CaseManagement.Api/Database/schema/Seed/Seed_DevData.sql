-- ServiceProvider seed
IF NOT EXISTS (SELECT 1 FROM dbo.ServiceProvider WHERE [Name] = 'Richmond Health Services')
BEGIN
    INSERT INTO dbo.ServiceProvider (Id, [Name], Region, ServiceType)
    VALUES (NEWID(), 'Richmond Health Services', 'VA', '');
END

-- Users seed
-- IF NOT EXISTS (SELECT 1 FROM dbo.[User] where Username = 'admin')
-- BEGIN
--     INSERT INTO dbo.[User] (Id, Username, PasswordHash, Role)
--     VALUES (NEWID(), 'admin', 'PENDING', 'Admin');
--     INSERT INTO [dbo].[User] (Id, Username, PasswordHash, Role)
--     VALUES
--     (
--         NEWID(),
--         'admin',
--         '$2a$11$8F4Xc6tEYXyK4h2YkI6z0Oe8j4Kn4H3G4wC4T.3Q4ZL.6xZlO0f2K',
--         'Admin'
--     ),
--     (
--         NEWID(),
--         'officer1',
--         --Password123!
--         '$2a$11$8F4Xc6tEYXyK4h2YkI6z0Oe8j4Kn4H3G4wC4T.3Q4ZL.6xZlO0f2K',
--         'Officer'
--     );
-- END

