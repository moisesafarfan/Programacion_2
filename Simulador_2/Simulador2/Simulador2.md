# Simulador2

`Simulador2` es una nueva clase que utiliza las clases que ya existían
en el proyecto para crear un escenario bancario con varios clientes,
cuentas y tarjetas. No modifica el funcionamiento de las clases
originales; las utiliza para realizar nuevas pruebas.

## Creación del Banco Universitario

Se crea un nuevo objeto de la clase `Banco`:

``` java
Banco objBanco = new Banco("Banco Universitario");
```

El objeto `objBanco` representa el banco que se utilizará durante toda
la simulación.

## Nuevos clientes

Se crean tres objetos de la clase `Cliente`:

``` java
Cliente objAna = new Cliente("Ana Lopez", "001");
Cliente objCarlos = new Cliente("Carlos Perez", "002");
Cliente objMaria = new Cliente("Maria Gomez", "003");
```

Cada objeto representa a un cliente diferente dentro del sistema.

## Nuevas cuentas bancarias

Se crea una cuenta para cada cliente:

``` java
CuentaBancaria objCuentaAna =
    new CuentaBancaria("1001", objAna, 2000.00);

CuentaBancaria objCuentaCarlos =
    new CuentaBancaria("1002", objCarlos, 1200.00);

CuentaBancaria objCuentaMaria =
    new CuentaBancaria("1003", objMaria, 500.00);
```

Cada cuenta recibe su número, el objeto del cliente que será su titular
y su saldo inicial.

Después las cuentas se registran en el banco:

``` java
objBanco.registrarCuenta(objCuentaAna);
objBanco.registrarCuenta(objCuentaCarlos);
objBanco.registrarCuenta(objCuentaMaria);
```

## Nuevas tarjetas

Se crea una tarjeta para cada cuenta:

``` java
Tarjeta objTarjetaAna =
    new Tarjeta("TAR-001", "1234", objCuentaAna);

Tarjeta objTarjetaCarlos =
    new Tarjeta("TAR-002", "2345", objCuentaCarlos);

Tarjeta objTarjetaMaria =
    new Tarjeta("TAR-003", "3456", objCuentaMaria);
```

Cada tarjeta tiene un número, un PIN y queda relacionada con su cuenta
bancaria correspondiente.

Luego se registran las tarjetas en el banco:

``` java
objBanco.registrarTarjeta(objTarjetaAna);
objBanco.registrarTarjeta(objTarjetaCarlos);
objBanco.registrarTarjeta(objTarjetaMaria);
```

## Uso del cajero automático

Se crea un nuevo cajero asociado al Banco Universitario:

``` java
CajeroAutomatico objCajero =
    new CajeroAutomatico(objBanco);
```

A partir de este objeto se realizan las pruebas y operaciones de
`Simulador2`.

## Nuevas pruebas de autenticación

Primero se comprueba que no sea posible operar sin una tarjeta
autenticada. Se intenta consultar el saldo y se captura el error
generado por el cajero:

``` java
try {
    objCajero.consultarSaldo();
}
catch (IllegalStateException e) {
    System.out.println(
        "No se puede operar sin una tarjeta autenticada."
    );
}
```

También se prueba la tarjeta `TAR-001` con un PIN incorrecto y después
con el PIN correcto:

``` java
objCajero.autenticar("TAR-001", "9999");
objCajero.autenticar("TAR-001", "1234");
```

Esto permite comprobar que el cajero solamente permita operar cuando la
autenticación sea correcta.

## Operaciones realizadas

Después de autenticar la tarjeta correspondiente, se realizan las
operaciones solicitadas:

``` java
objCajero.depositar(300.00);
objCajero.retirar(400.00);
objCajero.transferir("1003", 700.00);
objCajero.retirar(1500.00);
objCajero.transferir("1001", 250.00);
```

Las operaciones corresponden a:

-   Depósito de Q300.00 en la cuenta de Ana.
-   Retiro de Q400.00 de la cuenta de Carlos.
-   Transferencia de Q700.00 de Ana hacia María.
-   Intento de retiro de Q1,500.00 de la cuenta de María.
-   Transferencia de Q250.00 de Carlos hacia Ana.

El retiro de Q1,500.00 de María no debe realizarse porque su saldo
disponible es menor al monto solicitado.

## Cambio de tarjeta

Para trabajar con otra cuenta se cierra la sesión actual:

``` java
objCajero.cerrarSesion();
```

Después se autentica la tarjeta del siguiente cliente:

``` java
objCajero.autenticar("TAR-002", "2345");
```

Este procedimiento permite cambiar de usuario sin modificar directamente
las cuentas.

## Saldos finales

Al finalizar todas las operaciones, los saldos esperados son:

-   Ana López: Q1,850.00
-   Carlos Pérez: Q550.00
-   María Gómez: Q1,200.00

Todos los cambios de saldo se realizan mediante los métodos del cajero y
las clases de transacción existentes. En ningún momento se modifica
directamente el saldo de una cuenta.
