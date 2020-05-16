CREATE TABLE [dbo].[Sections]
(
	[ID] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [PK_Sections] PRIMARY KEY,
    [Title] NVARCHAR(256) NOT NULL,
    [Type] INT NOT NULL,
    CONSTRAINT [FK_Sections_SectionTypes] FOREIGN KEY ([Type]) REFERENCES [SectionTypes]([ID])
)
