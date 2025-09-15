-- Script para criação das tabelas de Permissão e relacionamento com Usuário

CREATE TABLE Permissoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(255) NULL
);

CREATE TABLE UsuariosPermissoes (
    UsuarioId INT NOT NULL,
    PermissaoId INT NOT NULL,
    PRIMARY KEY (UsuarioId, PermissaoId),
    CONSTRAINT FK_UsuariosPermissoes_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UsuariosPermissoes_Permissao FOREIGN KEY (PermissaoId) REFERENCES Permissoes(Id) ON DELETE CASCADE
);
