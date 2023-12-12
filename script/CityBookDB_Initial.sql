IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Books] (
    [Id] int NOT NULL IDENTITY,
    [Title] varchar(250) NOT NULL,
    [Language] varchar(50) NOT NULL,
    [Edition] int NOT NULL,
    [Pages] int NOT NULL,
    [Publishing] varchar(150) NOT NULL,
    [ISBN10] varchar(10) NOT NULL,
    [ISBN13] varchar(13) NOT NULL,
    [DimensionLength] decimal(5,2) NULL,
    [DimensionHeight] decimal(5,2) NULL,
    [DimensionWidth] decimal(5,2) NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Book_Unique_ISBN10] ON [Books] ([ISBN10]);
GO

CREATE UNIQUE INDEX [IX_Book_Unique_ISBN13] ON [Books] ([ISBN13]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231212130539_Initial', N'7.0.14');
GO

COMMIT;
GO

