PRINT 'Starting CaseManagement schema deployment'
GO

:r .\Database\schema\Tables\AuditLog.sql
:r .\Database\schema\Tables\User.sql
:r .\Database\schema\Tables\Officer.sql
:r .\Database\schema\Tables\Client.sql
:r .\Database\schema\Tables\ServiceProvider.sql
:r .\Database\schema\Tables\Case.sql

:r .\Database\schema\Seed\Seed_Lookups.sql
:r .\Database\schema\Seed\Seed_DevData.sql

PRINT 'Schema deployment complete'
GO