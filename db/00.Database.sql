IF (NOT EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'SpawnTraffic' ))
BEGIN
     CREATE DATABASE SpawnTraffic
END
GO

USE SpawnTraffic
GO

-- SpawnTraffic App

IF (NOT EXISTS (SELECT 1 FROM sys.syslogins where name = 'SpawnTrafficApp'))
BEGIN
     CREATE LOGIN SpawnTrafficApp WITH 
            PASSWORD = 'Autospawn!01',
            DEFAULT_DATABASE = SpawnTraffic, 
            CHECK_EXPIRATION = OFF, 
            CHECK_POLICY = OFF;
END

IF (NOT EXISTS (SELECT 1 FROM SpawnTraffic.sys.database_principals where name = 'SpawnTrafficApp' ))
BEGIN
     CREATE USER SpawnTrafficApp FOR LOGIN SpawnTrafficApp WITH DEFAULT_SCHEMA = app;
END

IF NOT EXISTS ( SELECT 1 FROM sys.schemas WHERE name = 'app')
BEGIN
     EXEC('CREATE SCHEMA app');
END

GRANT SELECT ON SCHEMA ::app TO SpawnTrafficApp
GRANT INSERT ON SCHEMA ::app TO SpawnTrafficApp
GRANT UPDATE ON SCHEMA ::app TO SpawnTrafficApp
GRANT DELETE ON SCHEMA ::app TO SpawnTrafficApp
