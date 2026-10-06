-- ============================================================
-- Script de creación de base de datos - SQLite
-- Sistema de Gestión de Proyectos de Graduación UNI
-- ============================================================
-- NOTA: Con Entity Framework Core, este script se genera
-- automáticamente al ejecutar db.Database.EnsureCreated().
-- Se incluye como referencia del esquema.

CREATE TABLE Users (
    Id INTEGER NOT NULL CONSTRAINT PK_Users PRIMARY KEY AUTOINCREMENT,
    Email TEXT NOT NULL,
    PasswordHash TEXT NOT NULL,
    Role TEXT NOT NULL
);

CREATE UNIQUE INDEX IX_Users_Email ON Users (Email);

CREATE TABLE Projects (
    Id INTEGER NOT NULL CONSTRAINT PK_Projects PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT NULL,
    Status TEXT NOT NULL DEFAULT 'Propuesta',
    StudentId INTEGER NOT NULL,
    CONSTRAINT FK_Projects_Users_StudentId
        FOREIGN KEY (StudentId) REFERENCES Users (Id) ON DELETE CASCADE
);

CREATE INDEX IX_Projects_StudentId ON Projects (StudentId);

CREATE TABLE Reviews (
    Id INTEGER NOT NULL CONSTRAINT PK_Reviews PRIMARY KEY AUTOINCREMENT,
    ProjectId INTEGER NOT NULL,
    TutorId INTEGER NOT NULL,
    Comment TEXT NOT NULL,
    Status TEXT NOT NULL DEFAULT 'Pendiente',
    CreatedAt TEXT NOT NULL,
    CONSTRAINT FK_Reviews_Projects_ProjectId
        FOREIGN KEY (ProjectId) REFERENCES Projects (Id) ON DELETE CASCADE,
    CONSTRAINT FK_Reviews_Users_TutorId
        FOREIGN KEY (TutorId) REFERENCES Users (Id) ON DELETE RESTRICT
);

CREATE INDEX IX_Reviews_ProjectId ON Reviews (ProjectId);
CREATE INDEX IX_Reviews_TutorId ON Reviews (TutorId);