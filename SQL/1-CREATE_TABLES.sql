-- This script was generated by a beta version of the ERD tool in pgAdmin 4.
-- Please log an issue at https://redmine.postgresql.org/projects/pgadmin4/issues/new if you find any bugs, including reproduction steps.
BEGIN;


CREATE TABLE IF NOT EXISTS public."AspNetRoleClaims"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "RoleId" integer NOT NULL,
    "ClaimType" text COLLATE pg_catalog."default",
    "ClaimValue" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."AspNetRoles"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" character varying(256) COLLATE pg_catalog."default",
    "NormalizedName" character varying(256) COLLATE pg_catalog."default",
    "ConcurrencyStamp" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."AspNetUserClaims"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "UserId" integer NOT NULL,
    "ClaimType" text COLLATE pg_catalog."default",
    "ClaimValue" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."AspNetUserLogins"
(
    "LoginProvider" text COLLATE pg_catalog."default" NOT NULL,
    "ProviderKey" text COLLATE pg_catalog."default" NOT NULL,
    "ProviderDisplayName" text COLLATE pg_catalog."default",
    "UserId" integer NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey")
);

CREATE TABLE IF NOT EXISTS public."AspNetUserRoles"
(
    "UserId" integer NOT NULL,
    "RoleId" integer NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId")
);

CREATE TABLE IF NOT EXISTS public."AspNetUserTokens"
(
    "UserId" integer NOT NULL,
    "LoginProvider" text COLLATE pg_catalog."default" NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Value" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name")
);

CREATE TABLE IF NOT EXISTS public."AspNetUsers"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    nome text COLLATE pg_catalog."default",
    sobrenome text COLLATE pg_catalog."default",
    email text COLLATE pg_catalog."default",
    "UserName" character varying(256) COLLATE pg_catalog."default",
    "NormalizedUserName" character varying(256) COLLATE pg_catalog."default",
    "Email" character varying(256) COLLATE pg_catalog."default",
    "NormalizedEmail" character varying(256) COLLATE pg_catalog."default",
    "EmailConfirmed" boolean NOT NULL,
    "PasswordHash" text COLLATE pg_catalog."default",
    "SecurityStamp" text COLLATE pg_catalog."default",
    "ConcurrencyStamp" text COLLATE pg_catalog."default",
    "PhoneNumber" text COLLATE pg_catalog."default",
    "PhoneNumberConfirmed" boolean NOT NULL,
    "TwoFactorEnabled" boolean NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean NOT NULL,
    "AccessFailedCount" integer NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS public."Autor"
(
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    nome text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    descricao text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    CONSTRAINT "PK_Autor" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public."Editora"
(
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    nome text COLLATE pg_catalog."default" NOT NULL,
    descricao text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_Editora" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public."Genero"
(
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    nome text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    descricao text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    CONSTRAINT "PK_Genero" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public."Livro"
(
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "idGenero" integer NOT NULL,
    nome text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    descricao text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    edicao integer NOT NULL,
    "dataCadastro" timestamp without time zone NOT NULL,
    "urlCapa" text COLLATE pg_catalog."default" NOT NULL DEFAULT ''::text,
    valor numeric NOT NULL,
    avaliacao numeric NOT NULL,
    "anoPublicacao" integer NOT NULL DEFAULT 0,
    "idEditora" integer NOT NULL DEFAULT 0,
    CONSTRAINT "PK_Livro" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public."Livro_Autor"
(
    "idLivro" integer NOT NULL,
    "idAutor" integer NOT NULL,
    id integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    CONSTRAINT "PK_Livro_Autor" PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public."__EFMigrationsHistory"
(
    "MigrationId" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "ProductVersion" character varying(32) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

ALTER TABLE IF EXISTS public."AspNetRoleClaims"
    ADD CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
    REFERENCES public."AspNetRoles" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_AspNetRoleClaims_RoleId"
    ON public."AspNetRoleClaims"("RoleId");


ALTER TABLE IF EXISTS public."AspNetUserClaims"
    ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId")
    REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_AspNetUserClaims_UserId"
    ON public."AspNetUserClaims"("UserId");


ALTER TABLE IF EXISTS public."AspNetUserLogins"
    ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId")
    REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_AspNetUserLogins_UserId"
    ON public."AspNetUserLogins"("UserId");


ALTER TABLE IF EXISTS public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId")
    REFERENCES public."AspNetRoles" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_AspNetUserRoles_RoleId"
    ON public."AspNetUserRoles"("RoleId");


ALTER TABLE IF EXISTS public."AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId")
    REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;


ALTER TABLE IF EXISTS public."AspNetUserTokens"
    ADD CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId")
    REFERENCES public."AspNetUsers" ("Id") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;


ALTER TABLE IF EXISTS public."Livro"
    ADD CONSTRAINT "FK_Livro_Editora_idEditora" FOREIGN KEY ("idEditora")
    REFERENCES public."Editora" (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_Livro_idEditora"
    ON public."Livro"("idEditora");


ALTER TABLE IF EXISTS public."Livro"
    ADD CONSTRAINT "FK_Livro_Genero_idGenero" FOREIGN KEY ("idGenero")
    REFERENCES public."Genero" (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_Livro_idGenero"
    ON public."Livro"("idGenero");


ALTER TABLE IF EXISTS public."Livro_Autor"
    ADD CONSTRAINT "FK_Livro_Autor_Autor_idAutor" FOREIGN KEY ("idAutor")
    REFERENCES public."Autor" (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_Livro_Autor_idAutor"
    ON public."Livro_Autor"("idAutor");


ALTER TABLE IF EXISTS public."Livro_Autor"
    ADD CONSTRAINT "FK_Livro_Autor_Livro_idLivro" FOREIGN KEY ("idLivro")
    REFERENCES public."Livro" (id) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE;
CREATE INDEX IF NOT EXISTS "IX_Livro_Autor_idLivro"
    ON public."Livro_Autor"("idLivro");

END;