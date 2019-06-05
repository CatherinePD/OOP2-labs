﻿CREATE TABLE [dbo].[Contacts]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Email] NVARCHAR(75) NOT NULL,
    [Address] NVARCHAR(75) NULL,
    [Phone] NVARCHAR(75) NULL,
    [Photo] NVARCHAR(MAX) NULL
)
