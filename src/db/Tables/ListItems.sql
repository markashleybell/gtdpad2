﻿CREATE TABLE [dbo].[ListItems]
(
	[ID] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_ListItems] PRIMARY KEY,
    [List] UNIQUEIDENTIFIER NOT NULL,
    [Text] NVARCHAR(1024) NOT NULL,
    [Order] INT NOT NULL,
    CONSTRAINT [FK_ListItems_Sections] FOREIGN KEY ([List]) REFERENCES [Sections] ([ID])
)
