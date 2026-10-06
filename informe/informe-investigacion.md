\# Comparación de Arquitecturas y Herramientas Modernas para Sistemas Web Universitarios API-First



\*\*Universidad Nacional de Ingeniería\*\*

\*\*Dirección de Área de Conocimiento de Tecnologías de Información y Comunicación\*\*

\*\*Programa Ingeniería de Sistemas\*\*



\*\*Asignatura:\*\* Diseño de Sistemas en Internet

\*\*Tema:\*\* Arquitectura de Sistemas Web con Modelo C4, API-First y Mermaid

\*\*Caso de estudio:\*\* Sistema de Gestión de Proyectos de Graduación de la UNI



\*\*Autores:\*\* \[Nombre del equipo]

\*\*Docente:\*\* \[Nombre del docente]

\*\*Fecha:\*\* Octubre 2026



\---



\## Resumen



El presente informe compara enfoques arquitectónicos modernos para el desarrollo de sistemas web universitarios, tomando como caso de estudio el Sistema de Gestión de Proyectos de Graduación de la Universidad Nacional de Ingeniería (UNI). Se analiza el Modelo C4 como alternativa a los diagramas UML tradicionales, la arquitectura API-First frente al desarrollo tradicional basado en pantallas, y el uso de Mermaid.js como herramienta de documentación versionable. Se implementó un prototipo funcional con ASP.NET Core Web API, Entity Framework Core y SQLite, documentado con diagramas C4 y de secuencia integrados en un repositorio GitHub. Los resultados evidencian que la combinación C4 + API-First + Mermaid reduce la fricción de comunicación entre equipos y mejora la mantenibilidad del software.



\*\*Palabras clave:\*\* Modelo C4, API-First, Mermaid.js, ASP.NET Core, arquitectura de software, sistemas universitarios.



\---



\## 1. Introducción



\### 1.1 Contexto



El desarrollo de sistemas web universitarios enfrenta desafíos crecientes: múltiples actores (estudiantes, docentes, coordinadores, administradores), reglas de negocio complejas y la necesidad de integración con sistemas externos (registro académico, correo institucional, repositorios documentales). La documentación arquitectónica tradicional basada en UML ha demostrado ser costosa de mantener y difícil de comunicar a audiencias no técnicas.



\### 1.2 Problema



El Sistema de Gestión de Proyectos de Graduación de la UNI requiere:



\- Documentar la arquitectura de forma clara para todos los interesados

\- Facilitar el desarrollo paralelo entre frontend y backend

\- Versionar la documentación junto con el código

\- Reducir el acoplamiento entre interfaz, lógica de negocio y persistencia



\### 1.3 Objetivos



\*\*Objetivo general:\*\* Comparar arquitecturas y herramientas modernas para sistemas web universitarios API-First.



\*\*Objetivos específicos:\*\*



1\. Analizar el Modelo C4 frente a los diagramas UML tradicionales.

2\. Evaluar las ventajas de la arquitectura API-First frente al desarrollo tradicional.

3\. Justificar el uso de Mermaid.js para documentación versionable.

4\. Implementar un prototipo funcional que demuestre las decisiones arquitectónicas.



\### 1.4 Metodología



Se siguió un enfoque de \*\*investigación aplicada\*\* con las siguientes fases:



1\. \*\*Revisión bibliográfica\*\* de fuentes oficiales (C4 Model, Microsoft Learn, Mermaid.js).

2\. \*\*Diseño arquitectónico\*\* usando C4 (niveles 1 y 2) y diagramas de secuencia.

3\. \*\*Implementación\*\* de un prototipo con ASP.NET Core 8, EF Core y SQLite.

4\. \*\*Documentación\*\* en README.md con diagramas Mermaid versionados.

5\. \*\*Validación\*\* mediante Swagger/OpenAPI y pruebas manuales.



\---



\## 2. Marco teórico



\### 2.1 Modelo C4



El Modelo C4, propuesto por Simon Brown, organiza la arquitectura en cuatro niveles jerárquicos:



| Nivel | Nombre | Audiencia | Pregunta que responde |

|---|---|---|---|

| 1 | Contexto | No técnicos | ¿Quién usa el sistema y con qué interactúa? |

| 2 | Contenedores | Técnicos + arquitectos | ¿Qué aplicaciones y almacenes forman el sistema? |

| 3 | Componentes | Desarrolladores | ¿Cómo se descompone cada contenedor? |

| 4 | Código | Desarrolladores | ¿Cómo se implementa cada componente? |



\*\*Ventaja frente a UML:\*\* el Modelo C4 es más simple, cada nivel tiene una audiencia clara, y no requiere aprender notación compleja. El sitio oficial señala que para muchos equipos los niveles 1 y 2 son suficientes.



\### 2.2 Arquitectura API-First



API-First propone \*\*diseñar primero el contrato HTTP\*\* que define cómo los consumidores interactúan con el backend. Antes de programar la pantalla, el equipo identifica:



\- Recursos (ej. `/api/projects`)

\- Operaciones (GET, POST, PATCH, DELETE)

\- Entradas y salidas (DTOs)

\- Códigos de error (400, 401, 403, 404, 409)



\*\*Ventajas:\*\* desarrollo paralelo, contratos claros, mejor testabilidad, versionado explícito.



\### 2.3 Mermaid.js



Mermaid es un lenguaje de descripción de diagramas basado en texto. Sus ventajas:



\- ✅ \*\*Versionable:\*\* se almacena como código en Git

\- ✅ \*\*Revisable:\*\* los cambios se ven en Pull Requests

\- ✅ \*\*Renderizable:\*\* GitHub, GitLab, VS Code lo muestran nativamente

\- ✅ \*\*Multi-formato:\*\* soporta C4, secuencia, clases, estados, Gantt, etc.



\---



\## 3. Análisis comparativo



\### 3.1 Modelo C4 vs. UML tradicional



| Criterio | UML tradicional | Modelo C4 |

|---|---|---|

| Complejidad | Alta (14 tipos de diagramas) | Baja (4 niveles) |

| Audiencia | Técnica | Mixta (técnica y no técnica) |

| Curva de aprendizaje | Pronunciada | Suave |

| Herramientas | Rational Rose, Enterprise Architect | Structurizr, Mermaid, PlantUML |

| Versionado | Difícil (XML/binario) | Fácil (texto plano) |

| Costo de mantenimiento | Alto | Bajo |

| Adopción en equipos ágiles | Baja | Alta |



\*\*Conclusión:\*\* el Modelo C4 es más adecuado para proyectos ágiles y equipos multidisciplinarios.



\### 3.2 API-First vs. desarrollo tradicional



| Criterio | Tradicional (UI primero) | API-First |

|---|---|---|

| Orden de desarrollo | Pantalla → lógica → datos | Contrato → lógica → pantalla |

| Acoplamiento frontend/backend | Alto | Bajo |

| Desarrollo paralelo | Difícil | Natural |

| Testabilidad | Limitada | Alta (contrato independiente) |

| Documentación | Manual, desactualizada | Automática (OpenAPI) |

| Cambios de UI | Afectan backend | No afectan backend |



\*\*Conclusión:\*\* API-First es superior para proyectos con múltiples clientes (web, móvil, integraciones).



\### 3.3 Mermaid vs. otras herramientas de diagramación



| Herramienta | Formato | Versionable | Render en GitHub | Curva de aprendizaje |

|---|---|---|---|---|

| Mermaid.js | Texto | ✅ Sí | ✅ Nativo | Baja |

| PlantUML | Texto | ✅ Sí | ⚠️ Con extensión | Media |

| Draw.io | XML | ⚠️ Parcial | ❌ No | Baja |

| Lucidchart | Binario/Cloud | ❌ No | ❌ No | Baja |

| Structurizr | DSL | ✅ Sí | ⚠️ Con setup | Media |



\*\*Conclusión:\*\* Mermaid ofrece el mejor equilibrio entre simplicidad, versionado y renderizado nativo.



\### 3.4 Tecnologías backend comparadas



| Tecnología | Lenguaje | Rendimiento | Ecosistema | Curva aprendizaje | Costo |

|---|---|---|---|---|---|

| \*\*ASP.NET Core\*\* | C# | Alto | Excelente (Microsoft) | Media | Gratis |

| Spring Boot | Java | Alto | Excelente | Alta | Gratis |

| Node.js/Express | JavaScript | Medio | Excelente (npm) | Baja | Gratis |

| Django | Python | Medio | Bueno | Baja | Gratis |

| Laravel | PHP | Medio | Bueno | Baja | Gratis |



\*\*Justificación de ASP.NET Core:\*\* tipado fuerte, async/await nativo, EF Core maduro, integración con Identity, y soporte de herramientas empresariales.



\### 3.5 Bases de datos comparadas



| BD | Tipo | Ventaja principal | Desventaja |

|---|---|---|---|

| \*\*SQLite\*\* | Embebida | Cero configuración | No escalable horizontalmente |

| SQL Server | Relacional | Robusta, herramientas MS | Licencia costosa |

| PostgreSQL | Relacional | Open source, potente | Requiere instalación |

| MongoDB | NoSQL | Flexible | Sin integridad referencial fuerte |



\*\*Decisión:\*\* SQLite para desarrollo (cero fricción), SQL Server/PostgreSQL para producción.



\---



\## 4. Implementación del prototipo



\### 4.1 Arquitectura C4



\*\*C4 Nivel 1 (Contexto):\*\* se identificaron 4 actores (Estudiante, Tutor, Coordinador, Administrador) y 3 sistemas externos (correo institucional, almacenamiento de documentos, registro académico).



\*\*C4 Nivel 2 (Contenedores):\*\* se definieron 5 contenedores:



\- Aplicación Web (React/Angular)

\- API REST (ASP.NET Core)

\- Base de Datos (SQLite/SQL Server)

\- Gestor de Documentos

\- Servicio de Notificaciones



\### 4.2 Secuencias API-First



Se documentaron 4 flujos críticos:



1\. Login con emisión de JWT

2\. Registro de propuesta de proyecto

3\. Revisión por parte del tutor

4\. Aprobación por parte del coordinador



\### 4.3 Endpoints implementados



| Método | Endpoint | Rol requerido |

|---|---|---|

| POST | `/api/auth/register` | — |

| POST | `/api/auth/login` | — |

| GET | `/api/projects/mine` | Autenticado |

| POST | `/api/projects` | Autenticado |

| GET | `/api/projects/{id}` | Autenticado |

| PATCH | `/api/projects/{id}/status` | Tutor/Coord/Admin |

| POST | `/api/projects/{id}/reviews` | Tutor/Coord/Admin |

| GET | `/api/projects/{id}/reviews` | Autenticado |



\### 4.4 Estructura del código



Se aplicó \*\*Clean Architecture ligera\*\*:



\- `Domain/` — entidades (User, Project, Review)

\- `Application/` — DTOs y servicios

\- `Infrastructure/` — persistencia (AppDbContext)

\- `Controllers/` — puntos de entrada HTTP



\### 4.5 Seguridad



\- Contraseñas hasheadas con `IPasswordHasher<User>` (PBKDF2)

\- Autenticación JWT con expiración de 8 horas

\- Autorización por roles con `\[Authorize(Roles = "...")]`

\- DTOs para no exponer entidades directamente

\- `AsNoTracking()` en consultas de solo lectura



\---



\## 5. Resultados



\### 5.1 Validación funcional



El prototipo se validó mediante Swagger/OpenAPI:



\- ✅ Registro y login de usuarios funcional

\- ✅ JWT emitido y validado correctamente

\- ✅ 8 endpoints operativos

\- ✅ Control de acceso por roles

\- ✅ Base de datos SQLite creada automáticamente

\- ✅ Documentación Mermaid renderizada en GitHub



\### 5.2 Métricas de calidad



| Métrica | Valor |

|---|---|

| Endpoints implementados | 8 |

| Contenedores C4 | 5 |

| Diagramas de secuencia | 4 |

| Líneas de código C# | \~400 |

| Tiempo de arranque | <2 segundos |

| Dependencias NuGet | 6 |



\### 5.3 Lecciones aprendidas



1\. \*\*Versiones de paquetes:\*\* NuGet instala por defecto la versión más reciente, que puede ser incompatible con el framework objetivo. Es necesario especificar versiones explícitas.

2\. \*\*Mermaid C4:\*\* requiere sintaxis estricta (sin comentarios `//`, con comillas correctas).

3\. \*\*SQLite vs SQL Server:\*\* para prototipos académicos, SQLite elimina toda fricción de instalación.

4\. \*\*JWT + Identity:\*\* `IPasswordHasher` de Identity ofrece hashing seguro sin reinventar la rueda.



\---



\## 6. Conclusiones



1\. \*\*El Modelo C4 es superior a UML\*\* para documentar arquitecturas modernas: es más simple, tiene audiencias claras y se versiona fácilmente como texto.

2\. \*\*API-First facilita el desarrollo paralelo\*\* y mejora la testabilidad al definir contratos explícitos antes de la implementación.

3\. \*\*Mermaid.js es la mejor opción\*\* para diagramas versionables: renderiza nativamente en GitHub, se almacena como texto y tiene sintaxis simple.

4\. \*\*ASP.NET Core + EF Core + SQLite\*\* constituye una combinación ideal para prototipos académicos: tipado fuerte, sin fricción de instalación y con soporte empresarial.

5\. \*\*La combinación C4 + API-First + Mermaid\*\* reduce la deuda técnica documental y facilita la incorporación de nuevos miembros al equipo.



\---



\## 7. Recomendaciones



1\. \*\*Para producción:\*\* migrar de SQLite a SQL Server o PostgreSQL.

2\. \*\*Gestión de secretos:\*\* usar `dotnet user-secrets` o Azure Key Vault.

3\. \*\*Autenticación:\*\* agregar refresh tokens y revocación.

4\. \*\*Observabilidad:\*\* integrar Serilog + Application Insights.

5\. \*\*Tests:\*\* agregar pruebas unitarias (xUnit) y de integración.

6\. \*\*CI/CD:\*\* configurar GitHub Actions para build + test automático.

7\. \*\*Frontend:\*\* implementar cliente React/Angular que consuma el contrato OpenAPI.



\---



\## 8. Referencias



1\. Brown, S. (2024). \*The C4 model for visualising software architecture\*. https://c4model.com/

2\. Mermaid.js. (2024). \*Sequence diagrams\*. https://mermaid.js.org/syntax/sequenceDiagram.html

3\. Microsoft. (2024). \*Create a controller-based Web API with ASP.NET Core\*. https://learn.microsoft.com/aspnet/core/web-api/

4\. Microsoft. (2024). \*Dependency injection into controllers\*. https://learn.microsoft.com/aspnet/core/mvc/controllers/dependency-injection

5\. Microsoft. (2024). \*Entity Framework Core documentation\*. https://learn.microsoft.com/ef/core/

6\. Fielding, R. (2000). \*Architectural Styles and the Design of Network-based Software Architectures\*. Tesis doctoral, UC Irvine.

7\. Fowler, M. (2003). \*Patterns of Enterprise Application Architecture\*. Addison-Wesley.

8\. IETF. (2015). \*RFC 7519: JSON Web Token (JWT)\*. https://datatracker.ietf.org/doc/html/rfc7519

9\. OpenAPI Initiative. (2024). \*OpenAPI Specification 3.0\*. https://spec.openapis.org/oas/v3.0.0

10\. Newman, S. (2021). \*Building Microservices\* (2nd ed.). O'Reilly Media.



\---



\## Anexos



\### Anexo A: Estructura del repositorio



```

GraduacionUni/

├── db/

│   ├── init.sql

│   └── init-sqlite.sql

├── docs/

│   ├── c4-nivel1.mmd

│   ├── c4-nivel2.mmd

│   ├── secuencia-login.mmd

│   ├── secuencia-propuesta.mmd

│   ├── secuencia-revision.mmd

│   └── secuencia-aprobar.mmd

├── informe/

│   └── informe-investigacion.md

├── src/

│   └── GraduacionUni.Api/

├── requests.http

├── README.md

└── .gitignore

```



\### Anexo B: Capturas de pantalla



\*(Ver carpeta `informe/capturas/`)\*



1\. Mermaid Live Editor con C4 Nivel 1

2\. Mermaid Live Editor con C4 Nivel 2

3\. README.md renderizado en GitHub

4\. Swagger UI con endpoints

5\. Login exitoso con JWT

6\. Creación de proyecto



\### Anexo C: Repositorio GitHub



https://github.com/VioletGutierrez/GraduacionUni-2026

