﻿CREATE TABLE [dbo].[Pages]
(
	[ID] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_Pages] PRIMARY KEY,
    [Owner] UNIQUEIDENTIFIER NOT NULL,
    [Title] NVARCHAR(256) NOT NULL,
    [Slug] NVARCHAR(512) NOT NULL,
    CONSTRAINT [FK_Pages_Users] FOREIGN KEY ([Owner]) REFERENCES [Users]([ID])
)
