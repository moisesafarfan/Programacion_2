# Guía rápida de pruebas

Esta guía sirve para verificar el funcionamiento del proyecto antes de subirlo a GitHub.

## Prueba 1: Crear lector

Seleccionar la opción `1` e ingresar, por ejemplo:

```text
Codigo: L001
Nombre: Juan Perez
Correo: juan@correo.com
Telefono: 5555-1111
```

Luego seleccionar `2` y verificar que el lector aparezca en la lista.

## Prueba 2: Crear libro

Seleccionar la opción `5`:

```text
Codigo: LIB001
Titulo: El Principito
Autor: Antoine de Saint-Exupery
Cantidad de ejemplares: 2
```

Seleccionar `6` y verificar que `Disponibles` sea `2`.

## Prueba 3: Crear préstamo

Seleccionar la opción `9`.

El sistema mostrará los lectores y libros registrados. Ingresar una fecha límite futura y los Id correspondientes.

Después seleccionar `10` y verificar que el estado sea `Prestado`.

Al seleccionar nuevamente `6`, el libro deberá mostrar un ejemplar disponible menos.

## Prueba 4: Registrar devolución

Seleccionar la opción `12` e ingresar el Id del préstamo.

Después:

- En `10`, el préstamo debe aparecer como `Devuelto`.
- En `6`, el ejemplar debe volver a estar disponible.

## Prueba 5: Validar disponibilidad

Crear un libro con `1` ejemplar y registrar un préstamo activo para ese libro.

Intentar registrar un segundo préstamo del mismo libro. El sistema debe mostrar un error indicando que no hay ejemplares disponibles.

## Prueba 6: Polimorfismo

Seleccionar la opción `14`.

El programa crea una referencia de tipo `Persona` que contiene un objeto `Lector` y ejecuta la versión sobrescrita de `ObtenerInformacion()`.
