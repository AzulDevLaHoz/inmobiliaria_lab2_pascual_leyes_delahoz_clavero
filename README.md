Markdown
<div align="center">

# 🏢 Sistema de Gestión Inmobiliaria

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/dotnet/csharp/)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-6C217F?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)

<p align="center">
  Plataforma web desarrollada para la administración integral de inmuebles, propietarios, inquilinos, reservas, pagos y usuarios del sistema.
</p>

</div>

---

## 👥 Integrantes del Equipo

| Nombre y Apellido |
| :--- |
| 🧑‍💻 **Patricio Pascual** |
| 🧑‍💻 **Azul De La Hoz** |
| 🧑‍💻 **Leandro Leyes** |
| 🧑‍💻 **Juan Clavero** |

---

## 🚀 Estado del Proyecto e Funcionalidades Implementadas

El sistema se encuentra **completamente desarrollado y funcional**. Cuenta con autenticación por cookies, control de roles (Administrador / Empleado), validaciones de seguridad (tokens anti-falsificación CSRF) y flujo completo de **ABM / CRUD** (Alta, Baja, Modificación y Consulta) para todos los módulos:

- [x] **Propietarios:** Registro de datos personales, edición de información, listado paginado y gestión de estado.
- [x] **Inquilinos:** Gestión integral de inquilinos asociados a los alquileres.
- [x] **Inmuebles:** Registro detallado de inmuebles, estado de disponibilidad, asignación de propietarios y actualización de fotos/portadas.
- [x] **Reservas:** Control de contratos de alquiler, validación de fechas solapadas y restricciones de modificación según fechas de finalización y pagos asociados.
- [x] **Pagos:** Registro y control de pagos asociados a las reservas de alquiler.
- [x] **Usuarios y Avatares:** Gestión de cuentas de usuario, subida/cambio de avatar y modificación de clave con hashing seguro (`PasswordHasher`). *Solo accesible para rol Administrador*.

---

## 🔐 Usuarios y Credenciales de Prueba

La base de datos incluye los siguientes usuarios predeterminados para realizar pruebas de acceso según el rol:

| Rol | Correo Electrónico | Contraseña | Permisos principales |
| :--- | :--- | :--- | :--- |
| **Administrador** | `admin@ulp.com` | `1234` | Acceso total al sistema, incluida la gestión de Usuarios y roles. |
| **Empleado** | `empleado@promedio.com` | `1234` | Operación general (Inmuebles, Propietarios, Inquilinos, Reservas y Pagos). Exceptuando Bajas de entidades|

> ⚠️ **Nota:** El módulo de **Usuarios** está restringido exclusivamente a usuarios con rol **Administrador**. Los empleados únicamente pueden ver/editar su propio perfil y cambiar su avatar o contraseña.

---

## 🛠️ Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de contar con el siguiente software instalado:

* 🔹 **[.NET SDK](https://dotnet.microsoft.com/download)** `v8.0` o superior.
* 🔹 **[XAMPP Control Panel](https://www.apachefriends.org/es/index.html)** (con servicio **MySQL** habilitado).
* 🔹 **[DBeaver](https://dbeaver.io/)** o **phpMyAdmin** (para la administración visual de la base de datos).

---

## 🗄️ Configuración e Instalación de la Base de Datos

### Configuración Paso a Paso

1. **Iniciar el servicio de MySQL:**
   * Abre el **XAMPP Control Panel**.
   * Haz clic en **Start** en el módulo **MySQL** hasta que el indicador se ponga en verde (puerto por defecto `3306`).

2. **Ejecutar el Script SQL:**
   * Abre tu gestor de base de datos preferido (DBeaver, phpMyAdmin, etc.).
   * Ejecuta el archivo `.sql` incluido en el proyecto (dentro de la carpeta `DataBase/inmobiliaria_lab2.sql`).

   > ℹ️ **Nota:** No es necesario crear la base de datos manualmente. El script incluye la instrucción `CREATE DATABASE` de forma automática.

3. **Verificar Cadena de Conexión:**
   El archivo `appsettings.json` ubicado en la raíz del proyecto cuenta con las credenciales locales predeterminadas de XAMPP:

```json
{
  "ConnectionStrings": {
    "MySql": "Server=localhost;User=root;Password=;Database=inmobiliaria_lab2"
  }
}

---

## 💻 Ejecución de la Aplicación

1. **Navegar a la carpeta raíz del proyecto:**
   ```bash
   cd inmboiliarialab2_pascual_leyes_delahoz_clavero
   ```

2. **Iniciar la aplicación:**
   ```bash
   dotnet run
   ```

3. **Acceso desde el navegador:**
   Ingresa a la URL indicada por la consola (por ejemplo: `http://localhost:5253`).

---

## 📐 Diagrama Entidad - Relación (DER)

![alt text](Diagrama.jpeg)
 

 LINK Repositorio  : https://github.com/AzulDevLaHoz/inmobiliaria_lab2_pascual_leyes_delahoz_clavero