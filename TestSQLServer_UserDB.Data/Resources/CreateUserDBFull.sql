
USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_accessadmin')      
     EXEC (N'CREATE SCHEMA db_accessadmin')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_backupoperator')      
     EXEC (N'CREATE SCHEMA db_backupoperator')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_datareader')      
     EXEC (N'CREATE SCHEMA db_datareader')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_datawriter')      
     EXEC (N'CREATE SCHEMA db_datawriter')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_ddladmin')      
     EXEC (N'CREATE SCHEMA db_ddladmin')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_denydatareader')      
     EXEC (N'CREATE SCHEMA db_denydatareader')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_denydatawriter')      
     EXEC (N'CREATE SCHEMA db_denydatawriter')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_owner')      
     EXEC (N'CREATE SCHEMA db_owner')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_securityadmin')      
     EXEC (N'CREATE SCHEMA db_securityadmin')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'dbo')      
     EXEC (N'CREATE SCHEMA dbo')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'guest')      
     EXEC (N'CREATE SCHEMA guest')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'INFORMATION_SCHEMA')      
     EXEC (N'CREATE SCHEMA INFORMATION_SCHEMA')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'sys')      
     EXEC (N'CREATE SCHEMA sys')                                   
 GO                                                               

USE UserDB
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'user_db')      
     EXEC (N'CREATE SCHEMA user_db')                                   
 GO                                                               

USE UserDB
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'datosimportantes'  AND sc.name = N'user_db'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'datosimportantes'  AND sc.name = N'user_db'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [user_db].[datosimportantes]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[user_db].[datosimportantes]
(
   [iddatosImportantes] int  NOT NULL,
   [datos] nvarchar(45)  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'user_db.datosimportantes',
        N'SCHEMA', N'user_db',
        N'TABLE', N'datosimportantes'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE UserDB
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user'  AND sc.name = N'user_db'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user'  AND sc.name = N'user_db'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [user_db].[user]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[user_db].[user]
(
   [username] nvarchar(16)  NOT NULL,
   [email] nvarchar(255)  NULL,
   [password] nvarchar(32)  NOT NULL,
   [create_time] datetime  NULL,
   [user_unique_id] int IDENTITY(2, 1)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'user_db.`user`',
        N'SCHEMA', N'user_db',
        N'TABLE', N'user'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE UserDB
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_datosimportantes'  AND sc.name = N'user_db'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user_datosimportantes'  AND sc.name = N'user_db'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [user_db].[user_datosimportantes]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[user_db].[user_datosimportantes]
(
   [user_user_unique_id] int  NOT NULL,
   [datosImportantes_iddatosImportantes] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'user_db.user_datosimportantes',
        N'SCHEMA', N'user_db',
        N'TABLE', N'user_datosimportantes'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE UserDB
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_datosimportantes_iddatosImportantes'  AND sc.name = N'user_db'  AND type in (N'PK'))
ALTER TABLE [user_db].[datosimportantes] DROP CONSTRAINT [PK_datosimportantes_iddatosImportantes]
 GO



ALTER TABLE [user_db].[datosimportantes]
 ADD CONSTRAINT [PK_datosimportantes_iddatosImportantes]
   PRIMARY KEY
   CLUSTERED ([iddatosImportantes] ASC)

GO


USE UserDB
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_user_unique_id'  AND sc.name = N'user_db'  AND type in (N'PK'))
ALTER TABLE [user_db].[user] DROP CONSTRAINT [PK_user_user_unique_id]
 GO



ALTER TABLE [user_db].[user]
 ADD CONSTRAINT [PK_user_user_unique_id]
   PRIMARY KEY
   CLUSTERED ([user_unique_id] ASC)

GO


USE UserDB
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_datosimportantes_user_user_unique_id'  AND sc.name = N'user_db'  AND type in (N'PK'))
ALTER TABLE [user_db].[user_datosimportantes] DROP CONSTRAINT [PK_user_datosimportantes_user_user_unique_id]
 GO



ALTER TABLE [user_db].[user_datosimportantes]
 ADD CONSTRAINT [PK_user_datosimportantes_user_user_unique_id]
   PRIMARY KEY
   CLUSTERED ([user_user_unique_id] ASC, [datosImportantes_iddatosImportantes] ASC)

GO


USE UserDB
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_datosimportantes'  AND sc.name = N'user_db'  AND si.name = N'fk_user_datosImportantes_datosImportantes1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_user_datosImportantes_datosImportantes1_idx] ON [user_db].[user_datosimportantes] 
GO
CREATE NONCLUSTERED INDEX [fk_user_datosImportantes_datosImportantes1_idx] ON [user_db].[user_datosimportantes]
(
   [datosImportantes_iddatosImportantes] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE UserDB
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_datosimportantes$fk_user_datosImportantes_datosImportantes1'  AND sc.name = N'user_db'  AND type in (N'F'))
ALTER TABLE [user_db].[user_datosimportantes] DROP CONSTRAINT [user_datosimportantes$fk_user_datosImportantes_datosImportantes1]
 GO



ALTER TABLE [user_db].[user_datosimportantes]
 ADD CONSTRAINT [user_datosimportantes$fk_user_datosImportantes_datosImportantes1]
 FOREIGN KEY 
   ([datosImportantes_iddatosImportantes])
 REFERENCES 
   [UserDB].[user_db].[datosimportantes]     ([iddatosImportantes])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_datosimportantes$fk_user_datosImportantes_user'  AND sc.name = N'user_db'  AND type in (N'F'))
ALTER TABLE [user_db].[user_datosimportantes] DROP CONSTRAINT [user_datosimportantes$fk_user_datosImportantes_user]
 GO



ALTER TABLE [user_db].[user_datosimportantes]
 ADD CONSTRAINT [user_datosimportantes$fk_user_datosImportantes_user]
 FOREIGN KEY 
   ([user_user_unique_id])
 REFERENCES 
   [UserDB].[user_db].[user]     ([user_unique_id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE UserDB
GO
ALTER TABLE  [user_db].[datosimportantes]
 ADD DEFAULT NULL FOR [datos]
GO


USE UserDB
GO
ALTER TABLE  [user_db].[user]
 ADD DEFAULT NULL FOR [email]
GO

ALTER TABLE  [user_db].[user]
 ADD DEFAULT getdate() FOR [create_time]
GO

