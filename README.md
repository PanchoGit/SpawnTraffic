# SpawnTraffic

A console application to store skaters, two operations are allowed:

	1. Register a new skater.

	2. Display stored skaters.

Data will be stored on a Redis cache.
Logs about success, fails or info execution can be registered on SQL Server, files and also can be displayed on same console app.

## Requirements
- Visual Studio 2017 (15.9.4)
- Net Core Runtime 2.1
- SQL Server
- Redis Cache

## Database

1. Create Database SpawnTraffic on a SQL Server executing following scripts:

	SpawnTraffic\db\00.Database.sql

		Script will create Database, Login, User and Schema with corresponding grants.

	SpawnTraffic\db\01.App.Tables.sql

		Script will create the Log table.

	All script can be executed multiple times without duplicating objects.

## Redis Cache

2. Redis settings will take configuration server info from appsetting.json from the SpawnTraffic.AppCmd project (Redis:ConnectionString).
	By default database will be number 2.	

## Log4net

3. File logger will use Log4Net, the config file is stored on Configs folder and the path can be changed on appsetting.json.

## Console application

4. Console application can be deployed with Visual Studio 2017 (15.9.4) by right click on SpawnTraffic.AppCmd and execute Publish.

## Loggers

5. Available plugins loggers are deployed with the publish process:

	- SpawnTraffic.ConsoleLogger.dll
	- SpawnTraffic.DatabaseLogger.dll
	- SpawnTraffic.FileLogger.dll

Dll files can be copied or deleted in the plugins folder (Plugins) during the execution of the application.

## Execution

6. Use following command to execute the console application on deployed files:
	> dotnet SpawnTraffic.AppCmd.dll

## Integration Test

- The integration test require to create another Database by using following scripts:
	SpawnTraffic\db\Test\00.Database.sql
	SpawnTraffic\db\Test\01.App.Tables.sql
	Following script will create the a database SpawnTrafficTest and related login and users.
