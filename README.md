\# Sistema de Gestión de Proyectos de Graduación UNI



Sistema web \*\*API-First\*\* para la gestión de proyectos de graduación en la Universidad Nacional de Ingeniería (UNI). Permite registrar propuestas, asignar tutores, dar seguimiento, registrar revisiones y controlar estados.



\##  Asignatura



\*\*Diseño de Sistemas en Internet\*\* — Programa Ingeniería de Sistemas — UNI



\##  Arquitectura



El proyecto utiliza \*\*Modelo C4\*\* (niveles 1 y 2) + \*\*diagramas de secuencia\*\* + \*\*API-First\*\*, documentado con \*\*Mermaid.js\*\* y versionado en GitHub.



\### C4 Nivel 1 — Contexto



```mermaid

C4Context

&#x20;   title Sistema de Gestión de Proyectos de Graduación UNI - Contexto



&#x20;   Person(estudiante, "Estudiante", "Registra y da seguimiento a su proyecto de graduación")

&#x20;   Person(tutor, "Tutor", "Revisa proyectos, registra observaciones y valida avances")

&#x20;   Person(coordinador, "Coordinador", "Administra asignaciones, estados y seguimiento académico")

&#x20;   Person(admin, "Administrador", "Administra usuarios, roles y catálogos")



&#x20;   System(uni, "Sistema de Gestión de Proyectos de Graduación UNI", "Plataforma web para registrar, revisar, aprobar y dar seguimiento a proyectos")



&#x20;   System\_Ext(email, "Servicio de correo institucional", "Envía notificaciones académicas")

&#x20;   System\_Ext(storage, "Almacenamiento de documentos", "Conserva documentos entregados por los estudiantes")

&#x20;   System\_Ext(registro, "Sistema de Registro Académico", "Datos académicos oficiales")



&#x20;   Rel(estudiante, uni, "Registra propuestas, entrega documentos y consulta avances")

&#x20;   Rel(tutor, uni, "Revisa proyectos y registra observaciones")

&#x20;   Rel(coordinador, uni, "Gestiona proyectos, tutores y estados")

&#x20;   Rel(admin, uni, "Administra usuarios y catálogos")

&#x20;   Rel(uni, email, "Envía notificaciones")

&#x20;   Rel(uni, storage, "Almacena y recupera documentos")

&#x20;   Rel(uni, registro, "Consulta datos académicos", "HTTPS/JSON")

```



\### C4 Nivel 2 — Contenedores



```mermaid

C4Container

&#x20;   title Sistema de Gestión de Proyectos de Graduación UNI - Contenedores



&#x20;   Person(estudiante, "Estudiante", "Usuario académico")

&#x20;   Person(tutor, "Tutor", "Docente tutor")

&#x20;   Person(coordinador, "Coordinador", "Responsable académico")



&#x20;   System\_Boundary(uni, "Sistema de Gestión de Proyectos de Graduación UNI") {

&#x20;       Container(web, "Aplicación Web", "React / Angular / JavaScript", "Interfaz para estudiantes, tutores y coordinadores")

&#x20;       Container(api, "API REST", "ASP.NET Core Web API / C#", "Autenticación, reglas de negocio y servicios HTTP")

&#x20;       ContainerDb(db, "Base de Datos", "SQLite / SQL Server", "Usuarios, proyectos, revisiones, estados y catálogos")

&#x20;       Container(files, "Gestor de Documentos", "Almacenamiento de objetos", "Documentos de propuestas, avances y defensa")

&#x20;       Container(notify, "Servicio de Notificaciones", "C# / SMTP", "Mensajes y alertas")

&#x20;   }



&#x20;   System\_Ext(email, "Correo institucional", "SMTP / proveedor de correo")



&#x20;   Rel(estudiante, web, "Usa", "HTTPS")

&#x20;   Rel(tutor, web, "Usa", "HTTPS")

&#x20;   Rel(coordinador, web, "Usa", "HTTPS")

&#x20;   Rel(web, api, "Consume API REST", "HTTPS/JSON")

&#x20;   Rel(api, db, "Lee y escribe", "EF Core")

&#x20;   Rel(api, files, "Sube y recupera documentos", "HTTPS")

&#x20;   Rel(api, notify, "Solicita notificación", "HTTPS/SMTP")

&#x20;   Rel(notify, email, "Envía", "SMTP")

```



\### Secuencia — Login



```mermaid

sequenceDiagram

&#x20;   autonumber

&#x20;   actor E as Estudiante

&#x20;   participant W as Aplicación Web

&#x20;   participant A as API REST

&#x20;   participant DB as SQLite



&#x20;   E->>W: Ingresa correo y contraseña

&#x20;   W->>A: POST /api/auth/login

&#x20;   activate A

&#x20;   A->>DB: Buscar usuario por correo

&#x20;   DB-->>A: Usuario + hash

&#x20;   A->>A: Verificar contraseña

&#x20;   alt Credenciales válidas

&#x20;       A-->>W: 200 OK + token JWT

&#x20;       W-->>E: Mostrar panel principal

&#x20;   else Credenciales inválidas

&#x20;       A-->>W: 401 Unauthorized

&#x20;       W-->>E: Mostrar error

&#x20;   end

&#x20;   deactivate A

```



\### Secuencia — Registrar propuesta



```mermaid

sequenceDiagram

&#x20;   autonumber

&#x20;   actor E as Estudiante

&#x20;   participant W as Aplicación Web

&#x20;   participant A as API REST

&#x20;   participant DB as SQLite



&#x20;   E->>W: Completa formulario de propuesta

&#x20;   W->>A: POST /api/projects

&#x20;   activate A

&#x20;   A->>A: Validar DTO y reglas

&#x20;   A->>DB: Crear proyecto

&#x20;   DB-->>A: Id del proyecto

&#x20;   A-->>W: 201 Created + proyecto

&#x20;   deactivate A

&#x20;   W-->>E: Mostrar número de proyecto

```



\##  API principal



| Método | Endpoint | Descripción | Auth |

|---|---|---|---|

| POST | `/api/auth/register` | Registrar usuario | ❌ |

| POST | `/api/auth/login` | Autenticar usuario | ❌ |

| GET | `/api/projects/mine` | Listar proyectos del estudiante | ✅ |

| POST | `/api/projects` | Registrar propuesta | ✅ |

| GET | `/api/projects/{id}` | Consultar proyecto | ✅ |



\### Ejemplo de login



\*\*Request:\*\*



```http

POST /api/auth/login

Content-Type: application/json



{

&#x20; "email": "estudiante2@uni.edu.ni",

&#x20; "password": "Test1234!"

}

```



\*\*Response (200 OK):\*\*



```json

{

&#x20; "userId": 2,

&#x20; "email": "estudiante2@uni.edu.ni",

&#x20; "role": "Estudiante",

&#x20; "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."

}

```



\##  Tecnologías



| Capa | Tecnología |

|---|---|

| Backend | ASP.NET Core Web API / C# 12 |

| ORM | Entity Framework Core 8 |

| Base de datos | SQLite (desarrollo) / SQL Server (producción) |

| Autenticación | JWT + Identity PasswordHasher |

| Documentación | Swagger / OpenAPI 3.0 |

| Diagramas | Mermaid.js (C4 + Secuencia) |

| Control de versiones | Git + GitHub |



\##  Ejecución local



\### Requisitos



\- .NET SDK 8.0

\- Git

\- Navegador moderno



\### Pasos



```bash

\# 1. Clonar el repositorio

git clone https://github.com/TU\_USUARIO/GraduacionUni.git

cd GraduacionUni



\# 2. Restaurar paquetes

cd src/GraduacionUni.Api

dotnet restore



\# 3. Ejecutar la API

dotnet run

```



La API estará disponible en `http://localhost:5XXX` y Swagger en:



```

http://localhost:5XXX/swagger

```



La base de datos SQLite (`graduacionuni.db`) se crea automáticamente al arrancar.



\### Probar la API



1\. Abrir Swagger: `http://localhost:5XXX/swagger`

2\. Registrar usuario: `POST /api/auth/register`

3\. Copiar el `token` de la respuesta

4\. Clic en \*\*Authorize\*\* → pegar token → \*\*Authorize\*\*

5\. Probar endpoints protegidos



\##  Estructura del proyecto



```

GraduacionUni/

├── db/

│   └── init.sql                  # Script SQL (alternativa a SQLite)

├── docs/                          # Diagramas Mermaid

│   ├── c4-nivel1.mmd

│   ├── c4-nivel2.mmd

│   ├── secuencia-login.mmd

│   ├── secuencia-propuesta.mmd

│   ├── secuencia-revision.mmd

│   └── secuencia-aprobar.mmd

├── src/

│   └── GraduacionUni.Api/

│       ├── Domain/

│       │   └── Entities/          # User.cs, Project.cs

│       ├── Application/

│       │   ├── DTOs/              # LoginRequest, RegisterRequest

│       │   └── Services/          # AuthService, TokenService

│       ├── Infrastructure/

│       │   └── Persistence/       # AppDbContext

│       ├── Controllers/           # AuthController, ProjectsController

│       ├── Program.cs

│       ├── appsettings.json

│       └── GraduacionUni.Api.csproj

├── .gitignore

└── README.md

```



\##  Seguridad



\- ✅ Contraseñas hasheadas con `IPasswordHasher<User>` (nunca en texto plano)

\- ✅ Autenticación JWT con expiración de 8 horas

\- ✅ Uso de DTOs para no exponer entidades

\- ✅ `AsNoTracking()` en consultas de solo lectura

\- ✅ Validación de entrada en controladores

\- ⚠️ \*\*Producción\*\*: usar `dotnet user-secrets` para claves JWT y cadenas de conexión



\##  Decisiones arquitectónicas



| Decisión | Justificación |

|---|---|

| \*\*Modelo C4\*\* | Comunica arquitectura a audiencias técnicas y no técnicas sin sobrecarga UML |

| \*\*API-First\*\* | El contrato HTTP se diseña antes que la UI, permitiendo desarrollo paralelo frontend/backend |

| \*\*SQLite en dev\*\* | Cero configuración, sin instalación pesada, ideal para desarrollo y pruebas |

| \*\*EF Core + DTOs\*\* | Separa dominio de persistencia y expone solo lo necesario |

| \*\*JWT\*\* | Estándar para APIs REST stateless, escalable horizontalmente |

| \*\*Mermaid en README\*\* | Diagramas versionados como código, revisables en PRs |

| \*\*Inyección de dependencias\*\* | Facilita testing, desacopla implementaciones |

| \*\*PasswordHasher de Identity\*\* | Algoritmo PBKDF2 seguro, evita reinventar ruedas |



\##  Referencias



\- \[C4 Model](https://c4model.com/)

\- \[Mermaid Sequence Diagrams](https://mermaid.js.org/syntax/sequenceDiagram.html)

\- \[ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/)

\- \[Entity Framework Core](https://learn.microsoft.com/ef/core/)

\- \[JWT.io](https://jwt.io/)



\##  Autores



\- \[Tu nombre] — Universidad Nacional de Ingeniería

\- \[Compañero de equipo]



\##  Licencia



Proyecto académico — UNI 2026

