# LoginClean – Starter Kit (.NET 8 + Clean Architecture)

Este proyecto es una base **lista para producción** para sistemas de login, autenticación y gestión de usuarios con JWT, utilizando Clean Architecture, Entity Framework Core y buenas prácticas modernas de .NET.

---

## 🚀 ¿Qué incluye este starter?

- Autenticación y autorización vía JWT
- Arquitectura limpia por capas: Web, Application, Domain, Infrastructure, IoC, Shared
- Swagger para pruebas rápidas y documentación
- Entity Framework Core con migraciones listas
- Control de CORS y OutputCache (desactivable)
- Estructura para usuarios, roles y expiración de tokens
- Configuración lista para ambientes de desarrollo y producción
- **Gestión de roles**: CRUD de roles y asignación de roles a usuarios
- **Protección de endpoints por rol**: solo administradores pueden editar roles
- **Hash de contraseñas seguro** con BCrypt
- DTOs para registro, login, cambio de rol y respuestas

---

## 🗂️ Estructura de carpetas

```text
LoginClean/
│
├── Web/           # API REST y configuración de middleware (Program.cs)
├── Application/   # Lógica de negocio, servicios y DTOs
├── Domain/        # Entidades, modelos y lógica de dominio puro
├── Infrastructure/# Acceso a datos, EF Core, repositorios
├── IoC/           # Inyección de dependencias y servicios
└── Shared/        # Utilidades y constantes globales
```

---

## ⚡ Primeros pasos rápidos

1. **Clonar el repositorio**
    ```bash
    git clone https://dev.azure.com/aumax/Estandarizacion%20IT/_git/axLoginClean
    cd axLoginClean
    ```

2. **Configurar la base de datos**
    - Editar la cadena de conexión en `Web/appsettings.Development.json`:
        ```json
        "ConnectionStrings": {
          "axLoginCleanEntities": "Server=localhost;Database=LoginCleanDb;User Id=usuario;Password=clave;"
        }
        ```
    - Configurar los valores de JWT:
        ```json
        "JwtSettings": {
          "Key": "clave-larga-y-segura",
          "Issuer": "LoginClean",
          "Audience": "LoginCleanUsers"
        }
        ```

3. **Aplicar migraciones**
    ```bash
    dotnet ef database update --project Infrastructure
    ```

4. **Ejecutar la API**
    ```bash
    dotnet run --project Web
    ```

5. **Probar en Swagger**
    - Accedé a [http://localhost:5000](http://localhost:5000) (o el puerto configurado)
    - Usá `/api/auth/login` para loguearte y obtener un JWT.
    - Probá endpoints protegidos usando el botón **Authorize**.

---

## 🔐 Endpoints principales

| Método | Endpoint                  | Autenticación | Rol requerido | Descripción                 |
| ------ | ------------------------- | ------------- | ------------- | --------------------------- |
| POST   | `/api/auth/login`         | ❌            | -             | Login, devuelve JWT         |
| POST   | `/api/auth/register`      | ❌            | -             | Registro de usuario         |
| GET    | `/api/users/me`           | ✅            | -             | Perfil del usuario logueado |
| PUT    | `/user/change-role`       | ✅            | Admin         | Cambiar rol de usuario      |
| POST   | `/role/create`            | ✅            | Admin         | Crear nuevo rol             |
| PUT    | `/role/edit`              | ✅            | Admin         | Editar rol existente        |
| DELETE | `/role/delete`            | ✅            | Admin         | Eliminar rol                |
| GET    | `/role/list`              | ✅            | Admin         | Listar roles                |

---

## 🔒 Seguridad y buenas prácticas

- **Hash de contraseñas con BCrypt**: las contraseñas nunca se guardan en texto plano.
- **JWT con claims de rol**: los tokens incluyen el rol del usuario para proteger endpoints.
- **[Authorize(Roles = "Admin")]**: solo administradores pueden editar roles o cambiar roles de usuarios.
- Usar variables de entorno para las claves sensibles (especialmente JWT Key y cadenas de conexión)
- Cambiar la política de CORS en producción
- Proteger el endpoint de Swagger en ambientes productivos
- Usar migraciones para controlar los cambios de la base de datos
- Manejar excepciones globales y devolver respuestas uniformes (middleware incluido)

---

## 📝 Módulos y DTOs principales

- **Entidades**: `User`, `Role`
- **DTOs**: `RegisterUserDto`, `UserLoginDto`, `UserLoginResponseDto`, `ChangeUserRoleDto`, `RoleCreateDto`, `RoleEditDto`, `RoleResponseDto`
- **Servicios**: `UserService`, `RoleService`, `JwtTokenGenerator`, `PasswordHasher`
- **Repositorios**: `UserRepository`, `RoleRepository`

---

## 📝 To-Do / Ideas para mejorar

- Implementar refresh token y logout (invalidación server-side)
- Auditar logins/acciones de usuarios
- Notificaciones por email para recuperación de contraseña
- Integrar CI/CD (YAML pipeline para Azure DevOps o GitHub Actions)
- Dockerizar el entorno para fácil despliegue

---

## 👨‍💻 Colaboración

- Seguí las convenciones de carpetas/código.
- Documentá métodos y servicios nuevos.

---

## 📣 Contacto

Creado y mantenido por AuMax IT.

---
