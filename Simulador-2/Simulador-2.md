Simulador2 es una nueva clase que utiliza las clases que ya existíanen el proyecto para crear un escenario bancario con varios clientes,cuentas y tarjetas. No modifica el funcionamiento de las clasesoriginales; las utiliza para realizar nuevas pruebas.

Creación del Banco Universitario

Se crea un nuevo objeto de la clase Banco:

Banco objBanco = new Banco("Banco Universitario");

El objeto objBanco representa el banco que se utilizará durante todala simulación.

Nuevos clientes

Se crean tres objetos de la clase Cliente:

Cliente objAna = new Cliente("Ana Lopez", "001");
Cliente objCarlos = new Cliente("Carlos Perez", "002");
Cliente objMaria = new Cliente("Maria Gomez", "003");

Cada objeto representa a un cliente diferente dentro del sistema.

Nuevas cuentas bancarias

Se crea una cuenta para cada cliente:

CuentaBancaria objCuentaAna =
    new CuentaBancaria("1001", objAna, 2000.00);

CuentaBancaria objCuentaCarlos =
    new CuentaBancaria("1002", objCarlos, 1200.00);

CuentaBancaria objCuentaMaria =
    new CuentaBancaria("1003", objMaria, 500.00);

Cada cuenta recibe su número, el objeto del cliente que será su titulary su saldo inicial.

Después las cuentas se registran en el banco:

objBanco.registrarCuenta(objCuentaAna);
objBanco.registrarCuenta(objCuentaCarlos);
objBanco.registrarCuenta(objCuentaMaria);

Nuevas tarjetas

Se crea una tarjeta para cada cuenta:

Tarjeta objTarjetaAna =
    new Tarjeta("TAR-001", "1234", objCuentaAna);

Tarjeta objTarjetaCarlos =
    new Tarjeta("TAR-002", "2345", objCuentaCarlos);

Tarjeta objTarjetaMaria =
    new Tarjeta("TAR-003", "3456", objCuentaMaria);

Cada tarjeta tiene un número, un PIN y queda relacionada con su cuentabancaria correspondiente.

Luego se registran las tarjetas en el banco:

objBanco.registrarTarjeta(objTarjetaAna);
objBanco.registrarTarjeta(objTarjetaCarlos);
objBanco.registrarTarjeta(objTarjetaMaria);

Uso del cajero automático

Se crea un nuevo cajero asociado al Banco Universitario:

CajeroAutomatico objCajero =
    new CajeroAutomatico(objBanco);

A partir de este objeto se realizan las pruebas y operaciones deSimulador2.

Nuevas pruebas de autenticación

Primero se comprueba que no sea posible operar sin una tarjetaautenticada. Se intenta consultar el saldo y se captura el errorgenerado por el cajero:

try {
    objCajero.consultarSaldo();
}
catch (IllegalStateException e) {
    System.out.println(
        "No se puede operar sin una tarjeta autenticada."
    );
}

También se prueba la tarjeta TAR-001 con un PIN incorrecto y despuéscon el PIN correcto:

objCajero.autenticar("TAR-001", "9999");
objCajero.autenticar("TAR-001", "1234");

Esto permite comprobar que el cajero solamente permita operar cuando laautenticación sea correcta.

Operaciones realizadas

Después de autenticar la tarjeta correspondiente, se realizan lasoperaciones solicitadas:

objCajero.depositar(300.00);
objCajero.retirar(400.00);
objCajero.transferir("1003", 700.00);
objCajero.retirar(1500.00);
objCajero.transferir("1001", 250.00);

Las operaciones corresponden a:

Depósito de Q300.00 en la cuenta de Ana.

Retiro de Q400.00 de la cuenta de Carlos.

Transferencia de Q700.00 de Ana hacia María.

Intento de retiro de Q1,500.00 de la cuenta de María.

Transferencia de Q250.00 de Carlos hacia Ana.

El retiro de Q1,500.00 de María no debe realizarse porque su saldodisponible es menor al monto solicitado.

Cambio de tarjeta

Para trabajar con otra cuenta se cierra la sesión actual:

objCajero.cerrarSesion();

Después se autentica la tarjeta del siguiente cliente:

objCajero.autenticar("TAR-002", "2345");

Este procedimiento permite cambiar de usuario sin modificar directamentelas cuentas.

Saldos finales

Al finalizar todas las operaciones, los saldos esperados son:

Ana López: Q1,850.00

Carlos Pérez: Q550.00

María Gómez: Q1,200.00

Todos los cambios de saldo se realizan mediante los métodos del cajero ylas clases de transacción existentes. En ningún momento se modificadirectamente el saldo de una cuenta.
