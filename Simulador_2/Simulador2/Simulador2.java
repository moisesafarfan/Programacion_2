public class Simulador2 {

    public static void main(String[] args) {

        // Crear banco
        Banco objBanco = new Banco("Banco Universitario");

        // Crear clientes
        Cliente objAna = new Cliente("Ana Lopez", "001");
        Cliente objCarlos = new Cliente("Carlos Perez", "002");
        Cliente objMaria = new Cliente("Maria Gomez", "003");

        // Crear cuentas bancarias
        CuentaBancaria objCuentaAna =
            new CuentaBancaria("1001", objAna, 2000.00);

        CuentaBancaria objCuentaCarlos =
            new CuentaBancaria("1002", objCarlos, 1200.00);

        CuentaBancaria objCuentaMaria =
            new CuentaBancaria("1003", objMaria, 500.00);

        // Registrar cuentas en el banco
        objBanco.registrarCuenta(objCuentaAna);
        objBanco.registrarCuenta(objCuentaCarlos);
        objBanco.registrarCuenta(objCuentaMaria);

        // Crear tarjetas
        Tarjeta objTarjetaAna =
            new Tarjeta("TAR-001", "1234", objCuentaAna);

        Tarjeta objTarjetaCarlos =
            new Tarjeta("TAR-002", "2345", objCuentaCarlos);

        Tarjeta objTarjetaMaria =
            new Tarjeta("TAR-003", "3456", objCuentaMaria);

        // Registrar tarjetas en el banco
        objBanco.registrarTarjeta(objTarjetaAna);
        objBanco.registrarTarjeta(objTarjetaCarlos);
        objBanco.registrarTarjeta(objTarjetaMaria);

        // Crear cajero automático
        CajeroAutomatico objCajero =
            new CajeroAutomatico(objBanco);


        // Prueba: intentar operar sin autenticarse
        try {
            objCajero.consultarSaldo();
        }
        catch (IllegalStateException e) {
            System.out.println(
                "No se puede operar sin una tarjeta autenticada."
            );
        }


        // Prueba: PIN incorrecto de Ana
        System.out.println(
            "PIN incorrecto TAR-001: "
            + objCajero.autenticar("TAR-001", "9999")
        );


        // Prueba: PIN correcto de Ana
        System.out.println(
            "PIN correcto TAR-001: "
            + objCajero.autenticar("TAR-001", "1234")
        );


        // 1. Depositar Q300 en la cuenta de Ana
        System.out.println(
            objCajero.depositar(300.00)
        );


        // 2. Retirar Q400 de la cuenta de Carlos
        objCajero.cerrarSesion();

        objCajero.autenticar(
            "TAR-002",
            "2345"
        );

        System.out.println(
            objCajero.retirar(400.00)
        );


        // 3. Transferir Q700 de Ana hacia Maria
        objCajero.cerrarSesion();

        objCajero.autenticar(
            "TAR-001",
            "1234"
        );

        System.out.println(
            objCajero.transferir("1003", 700.00)
        );


        // 4. Intentar retirar Q1500 de Maria
        objCajero.cerrarSesion();

        objCajero.autenticar(
            "TAR-003",
            "3456"
        );

        System.out.println(
            objCajero.retirar(1500.00)
        );


        // 5. Transferir Q250 de Carlos hacia Ana
        objCajero.cerrarSesion();

        objCajero.autenticar(
            "TAR-002",
            "2345"
        );

        System.out.println(
            objCajero.transferir("1001", 250.00)
        );


        // Mostrar saldos finales

        // Ana
        objCajero.cerrarSesion();

        objCajero.autenticar(
            "TAR-001",
            "1234"
        );

        System.out.println(
            "Saldo final Ana: Q"
            + objCajero.consultarSaldo()
        );


        // Carlos
        objCajero.cerrarSesion();

        objCajero.autenticar(
            "TAR-002",
            "2345"
        );

        System.out.println(
            "Saldo final Carlos: Q"
            + objCajero.consultarSaldo()
        );


        // Maria
        objCajero.cerrarSesion();

        objCajero.autenticar(
            "TAR-003",
            "3456"
        );

        System.out.println(
            "Saldo final Maria: Q"
            + objCajero.consultarSaldo()
        );
    }
}