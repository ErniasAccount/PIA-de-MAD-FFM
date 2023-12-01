USE DB_Proy;	
GO

--FUNCIONES

--Perdí la fuente, no mover
CREATE FUNCTION fnDividirPalabras
(
    @List NVARCHAR(MAX),
    @Delimiter NVARCHAR(255)
)
RETURNS TABLE
WITH SCHEMABINDING
AS
    RETURN 
    (
      WITH E1(N) AS 
      (
        SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL
        SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL
        SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1 UNION ALL SELECT 1
      ),                          
      E2(N) AS (SELECT 1 FROM E1 a, E1 b), 
      E4(N) AS (SELECT 1 FROM E2 a, E2 b), 
      cteTally(N) AS 
      (
        SELECT 0 UNION ALL
        SELECT TOP (DATALENGTH(ISNULL(@List,1))) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) FROM E4
      ),
      cteStart(N1) AS 
      (
        SELECT t.N+1 
        FROM cteTally t
        WHERE (SUBSTRING(@List,t.N,1) = @Delimiter OR t.N = 0)
      )
      SELECT Item = SUBSTRING(@List, s.N1, ISNULL(NULLIF(CHARINDEX(@Delimiter,@List,s.N1),0)-s.N1,8000))
      FROM cteStart s
    );

GO

CREATE FUNCTION fnObtenerHistorialBusquedas(@idUsuario INT)
RETURNS TABLE
AS
RETURN 
(
    SELECT id_Busqueda, texto, Fecha_Busqueda, id_idioma, Testamento, Libro, Capitulo, VersiculoInicio, VersiculoFin
    FROM T_Busquedas
    WHERE id_Usuario = @idUsuario
)
GO

-- VIEWS
CREATE VIEW VistaVersiculos AS
SELECT v.Id_Version, t.Id_Testamento, v.Id_Libro, v.NumeroCap, v.NumeroVers, v.Texto, l.Nombre
FROM DB_Bible.dbo.Versiculos v
JOIN DB_Bible.dbo.Libros l ON v.Id_Libro = l.Id_Libro
JOIN DB_Bible.dbo.Testamentos t ON l.Id_Testamento = t.Id_Testamento;
GO

CREATE VIEW VistaInformacionUsuario AS
SELECT id_Usuario, Email, Current_Password, Nombre, ApellidoP, ApellidoM, Fecha_Nacimiento, Sexo, Rol
FROM T_Usuarios
GO

-- TRIGGERS
CREATE TRIGGER tr_TriggerDeLast_Password
ON T_Usuarios
INSTEAD OF UPDATE
AS
BEGIN
    -- Comprueba si se ha actualizado la contraseña
    IF UPDATE(Current_Password)
    BEGIN
        -- Obtiene la ID del usuario que se ha actualizado
        DECLARE @IDUsuario INT = (SELECT id_Usuario FROM inserted)

        -- Obtiene la nueva contraseña
        DECLARE @NewPassword VARCHAR(40) = (SELECT Current_Password FROM inserted WHERE id_Usuario = @IDUsuario)

        -- Obtiene la contraseña actual
        DECLARE @CurrentPassword VARCHAR(40) = (SELECT Current_Password FROM T_Usuarios WHERE id_Usuario = @IDUsuario)

        -- Obtiene las contraseñas anteriores
        DECLARE @LastPassword1 VARCHAR(40) = (SELECT Last_Password_1 FROM T_Usuarios WHERE id_Usuario = @IDUsuario)
        DECLARE @LastPassword2 VARCHAR(40) = (SELECT Last_Password_2 FROM T_Usuarios WHERE id_Usuario = @IDUsuario)

        -- Actualiza las contraseñas anteriores y la contraseña actual
        UPDATE T_Usuarios
        SET Current_Password = @NewPassword,
            Last_Password_1 = @CurrentPassword,
            Last_Password_2 = CASE WHEN Last_Password_1 IS NOT NULL THEN Last_Password_1 ELSE NULL END,
            Last_Password_3 = CASE WHEN Last_Password_2 IS NOT NULL THEN Last_Password_2 ELSE NULL END
        WHERE id_Usuario = @IDUsuario
    END
    ELSE
    BEGIN
        -- Si no se actualiza la contraseña, realiza la actualización normal
        UPDATE T_Usuarios
        SET Email = inserted.Email,
            Nombre = inserted.Nombre,
            ApellidoP = inserted.ApellidoP,
            ApellidoM = inserted.ApellidoM,
            Fecha_Nacimiento = inserted.Fecha_Nacimiento,
            Sexo = inserted.Sexo,
            Rol = inserted.Rol,
            Estatus = inserted.Estatus,
            Fecha_Registro = inserted.Fecha_Registro,
            Ultima_modificacion = inserted.Ultima_modificacion,
            Fecha_Baja = inserted.Fecha_Baja
        FROM inserted
        WHERE T_Usuarios.id_Usuario = inserted.id_Usuario
    END
END
GO

--PROCEDURES
CREATE PROCEDURE spGestionUsuarios
	(
	@Accion					CHAR(1),
	@id_Usuario				INT= NULL,

	@Email					VARCHAR(100)= NULL,
	@Current_Password		VARCHAR(40)= NULL,
	@Last_Password_1		VARCHAR(40)= NULL,
	@Last_Password_2		VARCHAR(40)= NULL,
	@Last_Password_3		VARCHAR(40)= NULL,
	@Nombre					VARCHAR(40)	= NULL,
	@ApellidoP				VARCHAR(40)	= NULL,
	@ApellidoM				VARCHAR(40)	= NULL,
	@Fecha_Nacimiento		DATE		= NULL,
	@Sexo					CHAR(1)		= NULL,
	@Rol					VARCHAR(40)	= NULL,
	@Estatus				BIT			= NULL,
	@Fecha_Registro			DATETIME	= NULL,
	@Ultima_Modificacion	DATETIME	= NULL,
	@Fecha_Baja				DATETIME	= NULL
										
	)
	AS
		
BEGIN
		DECLARE @HOY		SMALLDATETIME
		SET	@HOY= GETDATE();
	
	--INSERT
	
	IF @Accion = 'I'
	BEGIN
		INSERT INTO T_Usuarios(Email, Current_Password, Nombre, ApellidoP, ApellidoM, Fecha_Nacimiento, Sexo, Rol, Estatus, Fecha_Registro)
			VALUES(@Email, @Current_Password, @Nombre, @ApellidoP, @ApellidoM, @Fecha_Nacimiento, @Sexo, @Rol, @Estatus, @HOY);
	END;
	
	--UPDATE
	IF @Accion = 'U'
	BEGIN
		UPDATE T_Usuarios
		SET	
		Email					=	@Email,				
		Current_Password		=	@Current_Password,
		Last_Password_1			=	@Last_Password_1,
		Last_Password_2			=	@Last_Password_2,
		Last_Password_3			=	@Last_Password_3,
		Nombre					=	@Nombre,
		ApellidoP				=	@ApellidoP,
		ApellidoM				=	@ApellidoM,
		Fecha_Nacimiento		=	@Fecha_Nacimiento,
		Sexo					=	@Sexo,
		Rol						=	@Rol,
		Estatus					=	@Estatus,
		Fecha_Registro			=	@Fecha_Registro,
		Ultima_modificacion 	=	@Ultima_modificacion,
		Fecha_Baja				=	@Fecha_Baja

		WHERE id_Usuario		=	@id_Usuario;
	END;

	--DELETE
	IF @Accion = 'D'
	BEGIN
		DELETE FROM T_Usuarios
		WHERE id_Usuario = @id_Usuario;
	END;

	--BUSCA SI EL USUARIO INGRESADO EXISTE
	IF @Accion = 'E'
	BEGIN
    DECLARE @existe BIT
    EXEC @existe = fnBuscarUsuario @Email, @Current_Password
    SELECT @existe AS Existe
	RETURN @existe
	END

	--ENCUENTRA LA ID DEL USUARIO POR EMAIL Y CONTRASEÑA
	IF @Accion = 'B'
	BEGIN
    SELECT @id_Usuario = id_Usuario FROM T_Usuarios WHERE Email = @Email AND Current_Password = @Current_Password
    SELECT @id_Usuario AS id_Usuario
    RETURN @id_Usuario
	END


	--ACTIVACION LOGICA
	IF @Accion = 'L'
	BEGIN
	UPDATE  T_Usuarios
	SET 
		Estatus = 1
		WHERE id_Usuario = @id_Usuario;
	END;
	
		--DESACTIVACION LOGICA
	IF @Accion = 'X'
	BEGIN
	UPDATE  T_Usuarios
	SET 
		Estatus = 0
		WHERE id_Usuario = @id_Usuario;
	END;

	--SELECT
	IF @Accion = 'S'
	BEGIN
	SELECT id_Usuario, Email, Current_Password, Last_Password_1, Last_Password_2, Last_Password_3, Nombre, ApellidoP, ApellidoM, Fecha_Nacimiento, Sexo, Rol, Estatus, Fecha_Registro, Ultima_Modificacion, Fecha_Baja
	FROM T_Usuarios
		WHERE id_Usuario = @id_Usuario;
	END;


	--MOSTRAR ACTIVOS
	IF @Accion = 'A'
	BEGIN
	SELECT id_Usuario, Email, Current_Password, Last_Password_1, Last_Password_2, Last_Password_3, Nombre, ApellidoP, ApellidoM, Fecha_Nacimiento, Sexo, Rol, Estatus, Fecha_Registro, Ultima_Modificacion, Fecha_Baja 
	FROM T_Usuarios
	WHERE Estatus = 1
	ORDER BY Email
	END;

		--MOSTRAR NO ACTIVOS
	IF @Accion = 'Z'
	BEGIN
	SELECT id_Usuario, Email, Current_Password, Last_Password_1, Last_Password_2, Last_Password_3, Nombre, ApellidoP, ApellidoM, Fecha_Nacimiento, Sexo, Rol, Estatus, Fecha_Registro, Ultima_Modificacion, Fecha_Baja 
	FROM T_Usuarios
	WHERE Estatus = 0
	ORDER BY Email
	END;

		--MOSTRAR TODO
	IF @Accion = 'T'
	BEGIN
	SELECT id_Usuario, Email, Current_Password, Last_Password_1, Last_Password_2, Last_Password_3, Nombre, ApellidoP, ApellidoM, Fecha_Nacimiento, Sexo, Rol, Estatus, Fecha_Registro, Ultima_Modificacion, Fecha_Baja 
	FROM T_Usuarios
	ORDER BY id_Usuario
	END;

END;
GO

CREATE PROCEDURE spFiltrosDeBusqueda
    @Accion CHAR(1),
    @Testamento TINYINT = NULL,
    @Libro TINYINT = NULL,
    @Capitulo TINYINT = NULL,
    @VersiculoInicio TINYINT = NULL,
    @VersiculoFin TINYINT = NULL
AS
BEGIN
    -- BUSQUEDA RANGO DE VERSICULOS
    IF @Accion = 'V'
    BEGIN
        SELECT NumeroCap, NumeroVers, Texto
        FROM VistaVersiculos
        WHERE Id_Testamento = @Testamento AND Id_Libro = @Libro AND NumeroCap = @Capitulo AND NumeroVers BETWEEN @VersiculoInicio AND @VersiculoFin
    END
    -- BUSQUEDA TODOS LOS CAPITULOS
    ELSE IF @Accion = 'T'
    BEGIN
        SELECT NumeroCap, NumeroVers, Texto
        FROM VistaVersiculos
        WHERE Id_Testamento = @Testamento AND Id_Libro = @Libro AND NumeroVers BETWEEN @VersiculoInicio AND @VersiculoFin
    END
    -- BUSQUEDA TODOS LOS LIBROS
    ELSE IF @Accion = 'L'
    BEGIN
        SELECT NumeroCap, NumeroVers, Texto
        FROM VistaVersiculos
        WHERE Id_Testamento = @Testamento AND NumeroCap = @Capitulo AND NumeroVers BETWEEN @VersiculoInicio AND @VersiculoFin
    END
END
GO

CREATE PROCEDURE spObtenerInformacionUsuario
    @IDUsuario INT
AS
BEGIN
    SELECT id_Usuario, Email, Current_Password, Nombre, ApellidoP, ApellidoM, Fecha_Nacimiento, Sexo, Rol
    FROM VistaInformacionUsuario
    WHERE id_Usuario = @IDUsuario
END
GO

CREATE PROCEDURE spRegistrarBusqueda
    @Testamento INT,
    @Libro INT,
    @Capitulo INT,
    @VersiculoInicio INT,
    @VersiculoFin INT,
    @idUsuario INT,
    @idIdioma INT
AS
BEGIN
    INSERT INTO T_Busquedas (Testamento, Libro, Capitulo, VersiculoInicio, VersiculoFin, id_Usuario, Fecha_Busqueda, id_idioma)
    VALUES (@Testamento, @Libro, @Capitulo, @VersiculoInicio, @VersiculoFin, @idUsuario, GETDATE(), @idIdioma)
END
GO

CREATE PROCEDURE spObtenerHistorialBusquedas
    @idUsuario INT
AS
BEGIN
    SELECT * FROM fnObtenerHistorialBusquedas(@idUsuario)
END
GO

CREATE PROCEDURE spBorrarHistorialBusquedas
    @idUsuario INT
AS
BEGIN
    DELETE FROM T_Busquedas WHERE id_Usuario = @idUsuario
END
