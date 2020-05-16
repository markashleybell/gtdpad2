IF NOT EXISTS (SELECT * FROM SectionTypes)
BEGIN
    INSERT INTO
        SectionTypes (
            [ID],
            [Description]
        )
    VALUES
        (1, 'Rich Text'),
        (2, 'Image Block'),
        (3, 'List')
END
GO

IF NOT EXISTS (SELECT * FROM Users)
BEGIN
    INSERT INTO
        Users(
            [ID],
            [Email],
            [Password]
        )
    VALUES (
        'df77778f-2ef3-49af-a1a8-b1f064891ef5',
        'testuser@gtdpad.com',
        'AQAAAAEAACcQAAAAEMxaC9ulPnv8pSBYzgZbfl1T/XV5bycncWD9+W/hyvtYScpeJ+HSZuDPRds9FbmDQg==' -- test123
    )
END
GO
