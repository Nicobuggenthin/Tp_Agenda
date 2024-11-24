CREATE TABLE Eventos (
    IdEvento INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(100) NOT NULL,
    Fecha DATETIME NOT NULL,
    Descripcion NVARCHAR(400) NULL,
    Ubicacion NVARCHAR(200) NULL,
    Estado NVARCHAR(50) NOT NULL DEFAULT 'Pendiente',
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL,
    UsuarioId NVARCHAR(450) NOT NULL -- Identificador del usuario (de Google)
);

ALTER TABLE Eventos
ADD CONSTRAINT CHK_Estado CHECK (Estado IN ('Pendiente', 'Realizado', 'No realizado'));

ALTER TABLE Eventos
ADD FechaBaja DATETIME NULL;