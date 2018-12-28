
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/17/2017 06:47:04
-- Generated from EDMX file: D:\devel\Civic\Civic.Framework\WebApi\Civic.Framework.WebApi.Test\Models\Example.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EnvironmentEntity1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Entity1] DROP CONSTRAINT [FK_EnvironmentEntity1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Environments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Environments];
GO
IF OBJECT_ID(N'[dbo].[Entity1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity1];
GO
IF OBJECT_ID(N'[dbo].[Entity2]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Entity2];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Environments'
CREATE TABLE [dbo].[Environments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Entity1'
CREATE TABLE [dbo].[Entity1] (
    [Name] nvarchar(max)  NOT NULL,
    [EnvironmentID] int  NOT NULL,
    [Dte] datetime  NOT NULL,
    [Dte2] datetime  NULL,
    [Dble1] float  NOT NULL,
    [Dec1] decimal(12,3)  NOT NULL
);
GO

-- Creating table 'Entity2'
CREATE TABLE [dbo].[Entity2] (
    [SomeID] int IDENTITY(1,1) NOT NULL,
    [ff] nvarchar(max)  NOT NULL,
    [Modified] datetime  NOT NULL,
    [OtherDate] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Environments'
ALTER TABLE [dbo].[Environments]
ADD CONSTRAINT [PK_Environments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Name] in table 'Entity1'
ALTER TABLE [dbo].[Entity1]
ADD CONSTRAINT [PK_Entity1]
    PRIMARY KEY CLUSTERED ([Name] ASC);
GO

-- Creating primary key on [SomeID], [ff] in table 'Entity2'
ALTER TABLE [dbo].[Entity2]
ADD CONSTRAINT [PK_Entity2]
    PRIMARY KEY CLUSTERED ([SomeID], [ff] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EnvironmentID] in table 'Entity1'
ALTER TABLE [dbo].[Entity1]
ADD CONSTRAINT [FK_EnvironmentEntity1]
    FOREIGN KEY ([EnvironmentID])
    REFERENCES [dbo].[Environments]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EnvironmentEntity1'
CREATE INDEX [IX_FK_EnvironmentEntity1]
ON [dbo].[Entity1]
    ([EnvironmentID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------