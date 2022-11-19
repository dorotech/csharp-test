INSERT INTO "AspNetRoles" VALUES (1, 'admin', 'ADMIN', '1');

INSERT INTO "Editora" (id, nome, descricao) VALUES 
	(DEFAULT, 'Companhia da Letras', 'Teste'),
	(DEFAULT, 'Aleph', 'Teste'),
	(DEFAULT, 'Suma', 'Teste'),
	(DEFAULT, 'Editora Intrínseca', 'Teste'),
	(DEFAULT, 'Grupo Editorial Record', 'Teste'),
	(DEFAULT, 'Editora Rocco', 'Teste');

INSERT INTO "Autor" (id, nome, descricao) VALUES 
    (DEFAULT, 'Colleen Hoover', 'Teste'),
    (DEFAULT, 'Gusatve Flaubert', 'Teste'),
    (DEFAULT, 'Junji Ito', 'Teste'),
    (DEFAULT, 'J.R.R. Tolkien', 'Teste'),
    (DEFAULT, 'George S Clason', 'Teste'),
    (DEFAULT, 'Christina Lauren', 'Teste'),
    (DEFAULT, 'T. Harv Eker', 'Teste'),
    (DEFAULT, 'Napoleon Hill', 'Teste'),
    (DEFAULT, 'Taylor Jenkins Reid', 'Teste'),
    (DEFAULT, 'Morgan Housel', 'Teste'),
    (DEFAULT, 'Nizan Guanaes', 'Teste'),
    (DEFAULT, 'Tahereh Mafi', 'Teste'),
    (DEFAULT, 'Clarissa Pinkola Estés', 'Teste'),
    (DEFAULT, 'Beth O’Leary', 'Teste'),
    (DEFAULT, 'Nicholas Boothman', 'Teste');

INSERT INTO "Genero" (id, nome, descricao) VALUES 
    (DEFAULT, 'Administração, Negócios e Economia', 'Teste'),
    (DEFAULT, 'Arte, Cinema e Fotografia', 'Teste'),
    (DEFAULT, 'Artesanato, Casa e Estilo de Vida', 'Teste'),
    (DEFAULT, 'Autoajuda', 'Teste'),
    (DEFAULT, 'Biografias e Histórias Reais', 'Teste'),
    (DEFAULT, 'Ciências', 'Teste'),
    (DEFAULT, 'Computação, Informática e Mídias Digitais', 'Teste'),
    (DEFAULT, 'Crônicas, Humor e Entretenimento', 'Teste'),
    (DEFAULT, 'Direito', 'Teste'),
    (DEFAULT, 'Educação, Referência e Didáticos', 'Teste'),
    (DEFAULT, 'Engenharia e Transporte', 'Teste'),
    (DEFAULT, 'Esportes e Lazer', 'Teste'),
    (DEFAULT, 'Fantasia, Horror e Ficção Científica', 'Teste'),
    (DEFAULT, 'Literatura e Ficção', 'Teste'),
    (DEFAULT, 'Religião e Espiritualidade', 'Teste'),
    (DEFAULT, 'Romance', 'Teste'),
    (DEFAULT, 'Drama', 'Teste');

INSERT INTO "Livro"  VALUES 
    (DEFAULT, 16, 'É assim que começa', 'Teste', 1, '2020-01-22 00:00:00', 'http://imgsrc.com/1.jpg', 44.59, 5, 2022, 1),
    (DEFAULT, 16, 'Madame Bovary', 'Teste', 1, '2020-01-22 00:00:00', 'http://imgsrc.com/1.jpg', 54.95, 4.8, 2022, 2),
    (DEFAULT, 16, 'É Assim que Acaba', 'Teste', 1, '2020-01-22 00:00:00', 'http://imgsrc.com/1.jpg', 34.48, 4.6, 2022, 3),
    (DEFAULT, 14, 'Sensor', 'Teste', 2, '2020-02-22 00:00:00', 'http://imgsrc.com/1.jpg', 45.40, 3.8, 2021, 4),
    (DEFAULT, 13, 'O Senhor dos Anéis', 'Teste', 3, '2020-02-15 00:00:00', 'http://imgsrc.com/1.jpg', 89.90, 4, 2021, 5),
    (DEFAULT, 1, 'O homem mais rico da Babilônia', 'Teste', 3, '2020-02-20 00:00:00', 'http://imgsrc.com/1.jpg', 17.99, 4.2, 2021, 6),
    (DEFAULT, 8, 'Imperfeitos', 'Teste', 2, '2020-02-20 00:00:00', 'http://imgsrc.com/1.jpg', 22.40, 5, 2020, 1),
    (DEFAULT, 17, 'Todas as suas (im)perfeições', 'Teste', 1, '2020-02-20 00:00:00', 'http://imgsrc.com/1.jpg', 35.99, 4.7, 2020, 2),
    (DEFAULT, 17, 'Verity', 'Teste', 1, '2020-02-20 00:00:00', 'http://imgsrc.com/1.jpg', 37.99, 5, 2019, 3),
    (DEFAULT, 1, 'Os segredos da mente milionária', 'Teste', 1, '2017-12-20 00:00:00', 'http://imgsrc.com/1.jpg', 33.99, 3.5, 2019, 4),
    (DEFAULT, 14, 'Mais esperto que o Diabo', 'Teste', 3, '2019-06-10 00:00:00', 'http://imgsrc.com/1.jpg', 25.20, 4.5, 2018, 5),
    (DEFAULT, 17, 'Os sete maridos de Evelyn Hugo', 'Teste', 1, '2018-05-15 00:00:00', 'http://imgsrc.com/1.jpg', 31.4, 4, 2017, 6);

INSERT INTO "Livro_Autor" ("idLivro", "idAutor", id) VALUES 
    (1, 1, DEFAULT),
    (2, 2, DEFAULT),
    (3, 1, DEFAULT),
    (4, 3, DEFAULT),
    (5, 4, DEFAULT),
    (6, 5, DEFAULT),
    (7, 6, DEFAULT),
    (8, 1, DEFAULT),
    (9, 1, DEFAULT),
    (10, 7, DEFAULT),
    (11, 8, DEFAULT),
    (12, 9, DEFAULT);