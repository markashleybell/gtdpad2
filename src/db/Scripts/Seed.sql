IF NOT EXISTS (SELECT * FROM SectionTypes)
BEGIN
    INSERT INTO
        SectionTypes (
            [ID],
            [Description]
        )
    VALUES
        (1, 'Rich Text Block'),
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
        'AQAAAAEAACcQAAAAEFTI0bden02dzzd79voUdie2183kdSBuQkjY1NUyh6AqREZcdimX0Ys8b4RFVsJFdg==' -- !gtdpad-test2020
    )
END
GO
