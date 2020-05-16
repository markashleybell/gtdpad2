﻿CREATE TABLE [dbo].[TextBlocks]
(
	[ID] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_TextBlocks] PRIMARY KEY,
    [Section] UNIQUEIDENTIFIER NOT NULL,
    [Text] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [FK_TextBlocks_Sections] FOREIGN KEY ([Section]) REFERENCES [Sections] ([ID])
)
