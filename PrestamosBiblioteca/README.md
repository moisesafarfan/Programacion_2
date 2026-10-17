# Sistema de Préstamos de Biblioteca

Proyecto de consola desarrollado en **C#** con persistencia en **SQLite**.

El sistema administra lectores, libros y préstamos. La base de datos se crea automáticamente la primera vez que se ejecuta el programa.

## Funcionalidades

### Lectores
- Crear lector.
- Listar lectores.
- Actualizar lector.
- Eliminar lector.

### Libros
- Crear libro.
- Listar libros.
- Actualizar libro.
- Eliminar libro.
- Mostrar la cantidad de ejemplares disponibles.

### Préstamos
- Registrar préstamo.
- Listar préstamos.
- Actualizar préstamo.
- Registrar devolución.
- Eliminar préstamo.
- Mostrar el estado: `Prestado`, `Vencido` o `Devuelto`.
- Evitar préstamos cuando no hay ejemplares disponibles.

### Programación orientada a objetos
- Clase base `Persona`.
- Clase derivada `Lector`.
- Herencia.
- Sobrescritura de `ObtenerInformacion()`.
- Demostración de polimorfismo desde el menú.
- Encapsulamiento mediante propiedades.
- Manejo de excepciones con `try-catch`.

## Estructura

```text
PrestamosBiblioteca/
├── Aplicacion/
│   └── MenuBiblioteca.cs
├── BaseDatos/
│   └── Crear_Tablas.sql
├── Dominio/
│   ├── Persona.cs
│   ├── Lector.cs
│   ├── Libro.cs
│   └── Prestamo.cs
├── Persistencia/
│   ├── InicializadorBaseDatos.cs
│   └── RepositorioBiblioteca.cs
├── Documentacion/
│   └── Pruebas.md
├── Program.cs
├── PrestamosBiblioteca.csproj
├── PrestamosBiblioteca.sln
└── README.md
```

## Requisitos

- .NET 8 SDK.
- Visual Studio 2022, Visual Studio Code o cualquier editor compatible con .NET.
- Conexión a Internet la primera vez para restaurar el paquete `Microsoft.Data.Sqlite`.

## Ejecutar con Visual Studio

1. Abrir `PrestamosBiblioteca.sln`.
2. Esperar a que Visual Studio restaure los paquetes NuGet.
3. Presionar **F5** o el botón **Iniciar**.

## Ejecutar desde terminal

Desde la carpeta del proyecto:

```bash
dotnet restore
dotnet run
```

Al iniciar, se genera automáticamente el archivo:

```text
PrestamosBiblioteca.sqlite
```

No es necesario crear manualmente la base de datos.

## Base de datos

Se utilizan tres tablas principales:

- `Lectores`
- `Libros`
- `Prestamos`

Los préstamos se relacionan con lectores y libros mediante llaves foráneas. Si se elimina un lector o un libro, sus préstamos asociados se eliminan mediante `ON DELETE CASCADE`.

> Nota: en las fotografías de clase aparece una propuesta con una tabla separada para Autor. En los archivos `.cs` proporcionados para la tarea, `Autor` se utiliza directamente como texto dentro de `Libros`; este proyecto sigue los archivos entregados para mantener compatibilidad.

## Subir a GitHub

Crear un repositorio vacío en GitHub y luego ejecutar dentro de esta carpeta:

```bash
git init
git add .
git commit -m "Proyecto sistema de prestamos de biblioteca"
git branch -M main
git remote add origin https://github.com/TU-USUARIO/PrestamosBiblioteca.git
git push -u origin main
```

Después se entrega en la plataforma el enlace del repositorio.
