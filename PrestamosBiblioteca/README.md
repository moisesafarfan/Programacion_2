# Sistema de Préstamos de Biblioteca

Aplicación de consola desarrollada en **C# con .NET 8** que permite
administrar lectores, libros y préstamos de una biblioteca. La
información se guarda de forma persistente en una base de datos local
**SQLite**, por lo que no requiere instalar ni configurar un servidor de
base de datos.

## 1. Objetivo

El objetivo del proyecto es aplicar conceptos de programación orientada
a objetos, operaciones CRUD y persistencia de datos en una base de datos
relacional. El programa permite registrar lectores y libros, controlar
préstamos y registrar devoluciones, manteniendo la información aunque la
aplicación se cierre.

## 2. Funcionalidades principales

### Lectores

-   Registrar lectores con código, nombre, correo y teléfono.
-   Consultar la lista de lectores.
-   Actualizar los datos de un lector.
-   Eliminar un lector.

### Libros

-   Registrar libros con código, título, autor y cantidad de ejemplares.
-   Consultar libros y la cantidad de ejemplares disponibles.
-   Actualizar la información de un libro.
-   Eliminar un libro.

### Préstamos

-   Registrar un préstamo asociando un lector con un libro.
-   Consultar los préstamos y sus datos relacionados.
-   Actualizar un préstamo existente.
-   Registrar la devolución de un libro.
-   Eliminar un préstamo.
-   Mostrar el estado del préstamo: `Prestado`, `Vencido` o `Devuelto`.
-   Impedir un nuevo préstamo cuando no quedan ejemplares disponibles.
-   Liberar la disponibilidad del ejemplar cuando se registra la
    devolución.

### Programación orientada a objetos

-   Herencia mediante la clase base abstracta `Persona` y la clase
    derivada `Lector`.
-   Polimorfismo mediante la sobrescritura de `ObtenerInformacion()`.
-   Encapsulamiento de los datos mediante propiedades.
-   Validaciones de datos y manejo de excepciones con `try-catch`.

## 3. Tecnologías utilizadas

-   **Lenguaje:** C#
-   **Plataforma:** .NET 8
-   **Tipo de aplicación:** Consola
-   **Base de datos:** SQLite
-   **Proveedor de acceso a datos:** `Microsoft.Data.Sqlite` versión
    `8.0.10`
-   **Lenguaje de consultas:** SQL compatible con SQLite

SQLite almacena la información en un archivo local. No se necesita SQL
Server, MySQL ni otro servicio de base de datos independiente.

## 4. Organización del proyecto

``` text
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
├── README.md
└── .gitignore
```

### Responsabilidad de cada parte

  ------------------------------------------------------------------------------
  Archivo o carpeta                          Responsabilidad
  ------------------------------------------ -----------------------------------
  `Program.cs`                               Punto de entrada. Inicializa la
                                             base de datos, crea el repositorio
                                             e inicia el menú. También controla
                                             errores que impidan arrancar el
                                             programa.

  `Aplicacion/MenuBiblioteca.cs`             Presenta el menú, lee los datos
                                             ingresados por el usuario y llama a
                                             las operaciones del repositorio.

  `Dominio/Persona.cs`                       Clase base abstracta con los datos
                                             comunes `Codigo` y `Nombre`, y el
                                             método virtual
                                             `ObtenerInformacion()`.

  `Dominio/Lector.cs`                        Hereda de `Persona` y agrega
                                             `Correo` y `Telefono`. Sobrescribe
                                             `ObtenerInformacion()` para incluir
                                             los datos del lector.

  `Dominio/Libro.cs`                         Representa un libro y valida sus
                                             campos de texto y la cantidad de
                                             ejemplares.

  `Dominio/Prestamo.cs`                      Representa un préstamo, sus
                                             identificadores y fechas; valida
                                             los identificadores y el orden de
                                             las fechas.

  `Persistencia/InicializadorBaseDatos.cs`   Construye la cadena de conexión,
                                             habilita las llaves foráneas y
                                             ejecuta el script SQL de
                                             inicialización.

  `Persistencia/RepositorioBiblioteca.cs`    Ejecuta consultas y operaciones
                                             CRUD en SQLite. También contiene la
                                             lógica para controlar la
                                             disponibilidad y el flujo de
                                             préstamos.

  `BaseDatos/Crear_Tablas.sql`               Define las tablas, llaves foráneas,
                                             restricciones e índices de la base
                                             de datos.

  `Documentacion/Pruebas.md`                 Contiene pasos para comprobar las
                                             principales funciones del sistema.

  `PrestamosBiblioteca.csproj`               Define el proyecto .NET y la
                                             dependencia
                                             `Microsoft.Data.Sqlite`.
  ------------------------------------------------------------------------------

## 5. Modelo de datos

La base de datos contiene tres tablas principales:

### `Lectores`

  Campo        Descripción
  ------------ ---------------------------------------------------------
  `Id`         Identificador generado automáticamente; clave primaria.
  `Codigo`     Código del lector.
  `Nombre`     Nombre completo.
  `Correo`     Correo electrónico.
  `Telefono`   Número de teléfono.

### `Libros`

  Campo          Descripción
  -------------- ---------------------------------------------------------
  `Id`           Identificador generado automáticamente; clave primaria.
  `Codigo`       Código del libro.
  `Titulo`       Título del libro.
  `Autor`        Nombre del autor almacenado como texto.
  `Ejemplares`   Cantidad total de ejemplares registrados.

### `Prestamos`

  -----------------------------------------------------------------------
  Campo                               Descripción
  ----------------------------------- -----------------------------------
  `Id`                                Identificador generado
                                      automáticamente; clave primaria.

  `LectorId`                          Referencia al lector que solicita
                                      el libro.

  `LibroId`                           Referencia al libro prestado.

  `FechaPrestamo`                     Fecha en que se registra el
                                      préstamo.

  `FechaLimite`                       Fecha límite de devolución.

  `FechaDevolucion`                   Fecha de devolución; permanece nula
                                      mientras el préstamo está activo.
  -----------------------------------------------------------------------

Relaciones principales:

``` text
Lectores (1) ─────── (N) Prestamos (N) ─────── (1) Libros
```

Un lector puede tener varios préstamos a lo largo del tiempo, y un libro
puede aparecer en varios registros de préstamo. Las llaves foráneas
mantienen la relación entre las tablas.

> **Nota sobre autores:** el autor se almacena como texto en `Libros`.
> El diseño actual no incluye una tabla independiente para administrar
> autores.

## 6. Reglas y validaciones

-   Los campos de texto solicitados por el menú no pueden quedar vacíos.
-   La cantidad de ejemplares de un libro no puede ser negativa.
-   La fecha límite no puede ser anterior a la fecha del préstamo.
-   La fecha de devolución no puede ser anterior a la fecha del
    préstamo.
-   Solo se registra un préstamo si el libro existe y tiene ejemplares
    disponibles.
-   La disponibilidad se calcula restando a los ejemplares totales los
    préstamos que aún no tienen fecha de devolución.
-   El estado se calcula como `Devuelto` si existe una fecha de
    devolución; de lo contrario, es `Vencido` si pasó la fecha límite o
    `Prestado` si continúa dentro del plazo.
-   Las operaciones de creación y actualización de préstamos usan
    transacciones para confirmar los cambios de forma controlada.
-   Las consultas que reciben valores utilizan parámetros SQL en lugar
    de concatenar los datos ingresados.
-   Las llaves foráneas están habilitadas. El esquema define
    `ON DELETE CASCADE`, por lo que eliminar un lector o un libro
    también elimina sus préstamos asociados.

## 7. Variables y propiedades principales

Las propiedades siguientes representan la información principal que
maneja cada clase:

  --------------------------------------------------------------------------
  Clase                      Propiedades             Uso
  -------------------------- ----------------------- -----------------------
  `Persona`                  `Codigo`, `Nombre`      Datos comunes de una
                                                     persona.

  `Lector`                   `Correo`, `Telefono`    Datos específicos del
                                                     lector; hereda `Codigo`
                                                     y `Nombre`.

  `Libro`                    `Codigo`, `Titulo`,     Datos descriptivos del
                             `Autor`, `Ejemplares`   libro y cantidad de
                                                     ejemplares.

  `Prestamo`                 `LectorId`, `LibroId`,  Relaciona al lector con
                             `FechaPrestamo`,        el libro y registra el
                             `FechaLimite`,          ciclo del préstamo.
                             `FechaDevolucion`,      
                             `EstaActivo`            

  `MenuBiblioteca`           `_repositorio`,         Referencia al
                             `_continuar`            repositorio y control
                                                     del ciclo del menú.

  `RepositorioBiblioteca`    `_cadenaConexion`       Cadena utilizada para
                                                     conectarse a SQLite.

  `InicializadorBaseDatos`   `CadenaConexion`,       Cadena de conexión y
                             `_rutaScript`           ruta del archivo SQL de
                                                     inicialización.
  --------------------------------------------------------------------------

Los identificadores `Id` se generan en la base de datos mediante
`INTEGER PRIMARY KEY AUTOINCREMENT`; por eso el usuario trabaja con los
identificadores que aparecen en las listas.

## 8. Opciones del menú

    Opción Operación
  -------- ----------------------------------------
       `1` Crear lector
       `2` Listar lectores
       `3` Actualizar lector
       `4` Eliminar lector
       `5` Crear libro
       `6` Listar libros y ejemplares disponibles
       `7` Actualizar libro
       `8` Eliminar libro
       `9` Crear préstamo
      `10` Listar préstamos y estado
      `11` Actualizar préstamo
      `12` Registrar devolución
      `13` Eliminar préstamo
      `14` Demostrar polimorfismo
       `0` Salir

## 9. Requisitos e instalación

### Requisitos

-   SDK de **.NET 8**.
-   Visual Studio 2022 o Visual Studio Code con las herramientas de
    .NET.
-   Acceso a Internet la primera vez que se restauren los paquetes, si
    no están disponibles localmente.

### Ejecutar con Visual Studio

1.  Abrir `PrestamosBiblioteca.sln`.
2.  Esperar a que se restauren las dependencias NuGet.
3.  Ejecutar el proyecto con **F5** o con el botón **Iniciar**.

### Ejecutar desde una terminal

Abrir la terminal en la carpeta que contiene
`PrestamosBiblioteca.csproj` y ejecutar:

``` bash
dotnet restore
dotnet run
```

El programa ejecuta `BaseDatos/Crear_Tablas.sql` al iniciar y crea las
tablas si todavía no existen. La base de datos se guarda en un archivo
llamado `PrestamosBiblioteca.sqlite` en el directorio de trabajo desde
el que se ejecuta la aplicación.

No es necesario crear manualmente la base de datos ni instalar un
servidor SQLite.

## 10. Flujo recomendado para comprobar el sistema

1.  Seleccionar `1` y registrar un lector.
2.  Seleccionar `2` para consultar su `Id`.
3.  Seleccionar `5` y registrar un libro con al menos un ejemplar.
4.  Seleccionar `6` para consultar el `Id` del libro y sus ejemplares
    disponibles.
5.  Seleccionar `9`, ingresar el `Id` del lector, el `Id` del libro y la
    fecha límite.
6.  Seleccionar `10` para verificar el préstamo y su estado.
7.  Seleccionar `6` y comprobar que la disponibilidad disminuyó.
8.  Seleccionar `12` e ingresar el `Id` del préstamo para registrar la
    devolución.
9.  Consultar nuevamente las opciones `10` y `6`; el préstamo debe
    aparecer como `Devuelto` y el ejemplar debe estar disponible otra
    vez.
10. Cerrar y abrir el programa y comprobar que los datos continúan
    guardados.

Para más casos de prueba, consultar
[`Documentacion/Pruebas.md`](Documentacion/Pruebas.md).

## 11. Alcance actual

El proyecto es una aplicación de consola académica. Incluye
administración de lectores, libros y préstamos, pero no incorpora
autenticación de usuarios, interfaz gráfica, multas, reservas ni un
catálogo independiente de autores.

## 12. Publicación en GitHub

Desde la carpeta raíz del proyecto, se puede inicializar el repositorio
local y conectarlo con el repositorio creado en GitHub:

``` bash
git init
git add .
git commit -m "Sistema de prestamos de biblioteca en C# y SQLite"
git branch -M main
git remote add origin https://github.com/TU-USUARIO/PrestamosBiblioteca.git
git push -u origin main
```

Reemplazar `TU-USUARIO` por el usuario de GitHub y usar la URL real del
repositorio. Antes de entregar, verificar que el repositorio contenga el
código fuente, este README y los archivos de documentación, y que no se
haya subido una base de datos local con datos de prueba.
