IF (NOT EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'SpawnTrafficTest' ))
BEGIN
     CREATE DATABASE SpawnTrafficTest
END
GO

USE SpawnTrafficTest
GO

-- SpawnTrafficTest App

IF (NOT EXISTS (SELECT 1 FROM sys.syslogins where name = 'SpawnTrafficTestApp'))
BEGIN
     CREATE LOGIN SpawnTrafficTestApp WITH 
            PASSWORD = 'Autospawn!01',
            DEFAULT_DATABASE = SpawnTrafficTest, 
            CHECK_EXPIRATION = OFF, 
            CHECK_POLICY = OFF;
END

IF (NOT EXISTS (SELECT 1 FROM SpawnTrafficTest.sys.database_principals where name = 'SpawnTrafficTestApp' ))
BEGIN
     CREATE USER SpawnTrafficTestApp FOR LOGIN SpawnTrafficTestApp WITH DEFAULT_SCHEMA = app;
END

IF NOT EXISTS ( SELECT 1 FROM sys.schemas WHERE name = 'app')
BEGIN
     EXEC('CREATE SCHEMA app');
END

GRANT SELECT ON SCHEMA ::app TO SpawnTrafficTestApp
GRANT INSERT ON SCHEMA ::app TO SpawnTrafficTestApp
GRANT UPDATE ON SCHEMA ::app TO SpawnTrafficTestApp
GRANT DELETE ON SCHEMA ::app TO SpawnTrafficTestApp
