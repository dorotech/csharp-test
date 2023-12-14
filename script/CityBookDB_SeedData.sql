SET IDENTITY_INSERT [Books] ON
GO

INSERT INTO [Books]([Id],[Title],[Author],[Language],[Edition],[Pages],[Publishing],
[ISBN10],[ISBN13],[DimensionLength],[DimensionHeight],[DimensionWidth])
VALUES
(1,
'O verdadeiro criador de tudo: Como o cérebro humano esculpiu o universo como nós o conhecemos',
'Miguel Nicolelis', 'Português', 1, 400, 'Editora Crítica', '6555350288', '978-6555350289', 22.8, 15.2, 1.8)
,
(2,
'Made In Macaíba',
'Miguel Nicolelis', 'Português', 1, 312, 'Editora Crítica', '8542206894', '978-8542206890', 22.6, 15.8, 2)
,
(3,
'Prazer Em Conhecer. A Aventura Da Ciência E Da Educação',
'Gilberto Nicolelis; Miguel Dimenstein', 
'Português', 1, 112, 'Editora Papirus', '8561773030', '978-8561773038', 20.83, 13.72, 1.02)
GO

SET IDENTITY_INSERT [Books] OFF
GO

SET IDENTITY_INSERT [Users] ON
GO

INSERT INTO [Users] ([Id],[FullName],[Login],[Email],[Password],[Status],[CreatedAt],[LastLoginDate],[IsAdmin])
VALUES(1, 'Administrator', 'admin@dorotech.com.br', 'admin@dorotech.com.br', '02101824E1C1FBBCE9FF19A87FC6B6B5', 1, SYSDATETIMEOFFSET(), NULL, 1);

SET IDENTITY_INSERT [Users] OFF
GO