CREATE TABLE JM.Paciente (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    NombresApellidos VARCHAR(255) NOT NULL,
    FechaNacimiento DATE NULL,
    Edad INT NULL,
    DireccionDomiciliaria VARCHAR(255) NULL,
    Escuela VARCHAR(255) NULL,
    Curso VARCHAR(100) NULL,
    CedulaNino VARCHAR(10) NOT NULL,
    TelefonoMadre BIGINT NULL,
    TelefonoPadre BIGINT NULL,
    NombreMadre VARCHAR(100) NULL,
    NombrePadre VARCHAR(100) NULL,
    RepresentanteLegal VARCHAR(100) NULL,
    EdadRepresentante INT NULL,
    CedulaRepresentante VARCHAR(10) NOT NULL,
    IdEmpresa BIGINT NOT NULL,
    UsuarioCreacion VARCHAR(100) NULL,
    FechaCreacion DATETIME  NULL,
    UsuarioModificacion VARCHAR(100) NULL,
    CorreoNotificacion VARCHAR(100) NULL,
    FechaModificacion DATETIME NULL,
    Estado bit not NULL
);
