CREATE DATABASE DB_Proy;
USE DB_Proy;
GO

CREATE TABLE T_Usuarios
(
	id_Usuario			INT IDENTITY(1,1)	PRIMARY KEY		NOT NULL,
	Email				VARCHAR(100)			NOT NULL,
	Current_Password	VARCHAR(40)				NOT NULL,
	Last_Password_1		VARCHAR(40)				NULL,
	Last_Password_2		VARCHAR(40)				NULL,
	Last_Password_3		VARCHAR(40)				NULL,
	Nombre				VARCHAR(40)				NOT NULL,
	ApellidoP			VARCHAR(40)				NULL,
	ApellidoM			VARCHAR(40)				NULL,
	Fecha_Nacimiento	DATE					NOT NULL,
	Sexo				CHAR(1) DEFAULT 'F'		NOT NULL,
	Rol					VARCHAR(40)				NOT NULL,
	Estatus				BIT DEFAULT 1			NOT NULL,
	Fecha_Registro		DATETIME				NOT NULL,
	Ultima_modificacion DATETIME				NULL,
	Fecha_Baja			DATETIME				NULL,

);



CREATE TABLE T_Favoritos
(
	id_Favorito			INT IDENTITY(1,1)	PRIMARY KEY		NOT NULL,
	id_Usuario			INT						NOT NULL,
	NumeroCap			TINYINT					NULL,	--De DB_Bible
	id_Versiculo		SMALLINT				NULL,	--De DB_Bible
);

CREATE TABLE T_Busquedas
(
	id_Busqueda			INT IDENTITY(1,1)	PRIMARY KEY		NOT NULL,
	texto				TEXT					NOT NULL,
	id_Usuario			INT						NOT NULL,
	Fecha_Busqueda		DATETIME				NOT NULL,
	id_idioma			INT						NOT NULL --De DB_Bible

);

CREATE TABLE T_Busquedas
(
    id_Busqueda INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    texto TEXT NULL,
    id_Usuario INT NOT NULL,
    Fecha_Busqueda DATETIME NOT NULL,
    id_idioma INT NOT NULL,
    Testamento INT,
    Libro INT,
    Capitulo INT,
    VersiculoInicio INT,
    VersiculoFin INT
);

ALTER TABLE T_Favoritos
ADD FOREIGN KEY (id_Usuario) REFERENCES T_Usuarios(id_Usuario);

ALTER TABLE T_Busquedas
ADD FOREIGN KEY (id_Usuario) REFERENCES T_Usuarios(id_Usuario);
