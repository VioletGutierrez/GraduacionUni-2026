-- ============================================================
-- Script de creación de base de datos - SQL Server
-- Sistema de Gestión de Proyectos de Graduación UNI
-- ============================================================

IF DB_ID('GraduacionUNI') IS NULL
    CREATE DATABASE GraduacionUNI;
GO

USE GraduacionUNI;
GO

-- Eliminar tablas en orden inverso a las dependencias
IF OBJECT_ID('dbo.Reviews', 'U') IS NOT NULL DROP TABLE dbo.Reviews;
IF OBJECT_ID('dbo.Projects', 'U') IS NOT NULL DROP TABLE dbo.Projects;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

-- ============================================================
-- Tabla: Users
-- ============================================================
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(30) NOT NULL
);
GO

-- ============================================================
-- Tabla: Projects
-- ============================================================
CREATE TABLE Projects (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(250) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Status NVARCHAR(30) NOT NULL
        CONSTRAINT DF_Projects_Status DEFAULT 'Propuesta',
    StudentId INT NOT NULL,
    CONSTRAINT FK_Projects_Users
        FOREIGN KEY (StudentId) REFERENCES Users(Id)
);
GO

CREATE INDEX IX_Projects_StudentId ON Projects(StudentId);
GO

-- ============================================================
-- Tabla: Reviews
-- ============================================================
CREATE TABLE Reviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProjectId INT NOT NULL,
    TutorId INT NOT NULL,
    Comment NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(30) NOT NULL
        CONSTRAINT DF_Reviews_Status DEFAULT 'Pendiente',
    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_Reviews_CreatedAt DEFAULT GETUTCDATE(),
    CONSTRAINT FK_Reviews_Projects
        FOREIGN KEY (ProjectId) REFERENCES Projects(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Reviews_Users
        FOREIGN KEY (TutorId) REFERENCES Users(Id)
);
GO

CREATE INDEX IX_Reviews_ProjectId ON Reviews(ProjectId);
CREATE INDEX IX_Reviews_TutorId ON Reviews(TutorId);
GO

-- ============================================================
-- Datos de ejemplo (opcional)
-- ============================================================
-- INSERT INTO Users (Email, PasswordHash, Role)
-- VALUES ('estudiante@uni.edu.ni', '<hash-generado-por-IPasswordHasher>', 'Estudiante');
-- GO