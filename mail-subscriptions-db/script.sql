CREATE DATABASE Subscriptions;
GO

USE Subscriptions;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Topics] (
    [TopicId] int NOT NULL IDENTITY,
    [DisplayName] nvarchar(max) NULL,
    CONSTRAINT [PK_Topics] PRIMARY KEY ([TopicId])
);

GO

CREATE TABLE [Users] (
    [UserId] nvarchar(450) NOT NULL,
    [UnsubscribeToken] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [Subscriptions] (
    [SubscriptionId] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NULL,
    [TopicId] int NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([SubscriptionId]),
    CONSTRAINT [FK_Subscriptions_Topics_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [Topics] ([TopicId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Subscriptions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TopicId', N'DisplayName') AND [object_id] = OBJECT_ID(N'[Topics]'))
    SET IDENTITY_INSERT [Topics] ON;
INSERT INTO [Topics] ([TopicId], [DisplayName])
VALUES (1, N'ASP.NET Core'),
(2, N'Docker'),
(3, N'TypeScript'),
(4, N'Vanilla JS'),
(5, N'Adobe Experience Manager'),
(6, N'Node.js');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TopicId', N'DisplayName') AND [object_id] = OBJECT_ID(N'[Topics]'))
    SET IDENTITY_INSERT [Topics] OFF;

GO

CREATE INDEX [IX_Subscriptions_TopicId] ON [Subscriptions] ([TopicId]);

GO

CREATE INDEX [IX_Subscriptions_UserId] ON [Subscriptions] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190209231919_Initial', N'2.2.1-servicing-10028');

GO

