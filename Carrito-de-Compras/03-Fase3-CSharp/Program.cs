using CarritoCompras.Models;
using CarritoCompras.Services;

var tienda = new TiendaService();
var carrito = new CarritoService();
Cliente? clienteActual = null;

bool salir = false;

while (!salir)
{
    MostrarMenuPrincipal(clienteActual);

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            RegistrarCliente();
            break;

        case "2":
            IniciarSesion();
            break;

        case "3":
            tienda.MostrarProductos();
            Pausa();
            break;

        case "4":
            BuscarProducto();
            break;

        case "5":
            AgregarAlCarrito();
            break;

        case "6":
            carrito.MostrarCarrito();
            Pausa();
            break;

        case "7":
            ModificarCantidad();
            break;

        case "8":
            EliminarDelCarrito();
            break;

        case "9":
            FinalizarCompra();
            break;

        case "0":
            salir = true;
            Console.WriteLine("Gracias por utilizar el sistema.");
            break;

        default:
            Console.WriteLine("Opcion no valida.");
            Pausa();
            break;
    }
}

void MostrarMenuPrincipal(Cliente? cliente)
{
    Console.Clear();
    Console.WriteLine("======================================");
    Console.WriteLine("       SISTEMA CARRITO DE COMPRAS");
    Console.WriteLine("======================================");
    Console.WriteLine(cliente is null
        ? "Usuario: No ha iniciado sesion"
        : $"Usuario: {cliente.Nombre}");
    Console.WriteLine("--------------------------------------");
    Console.WriteLine("1. Registrarse");
    Console.WriteLine("2. Iniciar sesion");
    Console.WriteLine("3. Ver productos");
    Console.WriteLine("4. Buscar producto");
    Console.WriteLine("5. Agregar producto al carrito");
    Console.WriteLine("6. Visualizar carrito");
    Console.WriteLine("7. Modificar cantidad");
    Console.WriteLine("8. Eliminar producto del carrito");
    Console.WriteLine("9. Confirmar compra / pagar");
    Console.WriteLine("0. Salir");
    Console.WriteLine("--------------------------------------");
    Console.Write("Seleccione una opcion: ");
}

void RegistrarCliente()
{
    Console.Clear();
    Console.WriteLine("=== REGISTRO ===");

    Console.Write("Nombre: ");
    string nombre = Console.ReadLine() ?? "";

    Console.Write("Correo: ");
    string correo = Console.ReadLine() ?? "";

    Console.Write("Contrasena: ");
    string contrasena = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(nombre) ||
        string.IsNullOrWhiteSpace(correo) ||
        string.IsNullOrWhiteSpace(contrasena))
    {
        Console.WriteLine("Todos los campos son obligatorios.");
        Pausa();
        return;
    }

    clienteActual = tienda.RegistrarCliente(nombre, correo, contrasena);
    Console.WriteLine("Cliente registrado e inicio de sesion realizado.");
    Pausa();
}

void IniciarSesion()
{
    Console.Clear();
    Console.WriteLine("=== INICIAR SESION ===");

    Console.Write("Correo: ");
    string correo = Console.ReadLine() ?? "";

    Console.Write("Contrasena: ");
    string contrasena = Console.ReadLine() ?? "";

    clienteActual = tienda.IniciarSesion(correo, contrasena);

    Console.WriteLine(clienteActual is null
        ? "Credenciales incorrectas."
        : $"Bienvenido, {clienteActual.Nombre}.");

    Pausa();
}

void BuscarProducto()
{
    Console.Clear();
    Console.WriteLine("=== BUSCAR PRODUCTO ===");

    int id = LeerEntero("Ingrese ID del producto: ");
    Producto? producto = tienda.BuscarProducto(id);

    if (producto is null)
    {
        Console.WriteLine("Producto no encontrado.");
    }
    else
    {
        producto.MostrarInformacion();
    }

    Pausa();
}

void AgregarAlCarrito()
{
    if (!RequiereSesion())
        return;

    tienda.MostrarProductos();

    int id = LeerEntero("ID del producto: ");
    Producto? producto = tienda.BuscarProducto(id);

    if (producto is null)
    {
        Console.WriteLine("Producto no encontrado.");
        Pausa();
        return;
    }

    int cantidad = LeerEntero("Cantidad: ");

    if (carrito.AgregarItem(producto, cantidad))
    {
        Console.WriteLine("Producto agregado correctamente.");
        Console.WriteLine($"Stock restante: {producto.Stock}");
    }
    else
    {
        Console.WriteLine("No hay existencias suficientes o la cantidad no es valida.");
    }

    Pausa();
}

void ModificarCantidad()
{
    if (!RequiereSesion())
        return;

    carrito.MostrarCarrito();

    int id = LeerEntero("ID del producto a modificar: ");
    int cantidad = LeerEntero("Nueva cantidad: ");

    Console.WriteLine(carrito.ModificarCantidad(id, cantidad)
        ? "Cantidad modificada correctamente."
        : "No se pudo modificar. Revise producto, cantidad o stock.");

    Pausa();
}

void EliminarDelCarrito()
{
    if (!RequiereSesion())
        return;

    carrito.MostrarCarrito();

    int id = LeerEntero("ID del producto a eliminar: ");

    Console.WriteLine(carrito.EliminarItem(id)
        ? "Producto eliminado del carrito."
        : "Producto no encontrado en el carrito.");

    Pausa();
}

void FinalizarCompra()
{
    if (!RequiereSesion())
        return;

    carrito.MostrarCarrito();

    if (carrito.ObtenerDetalles().Count == 0)
    {
        Pausa();
        return;
    }

    Console.Write("Metodo de pago (Tarjeta/Efectivo): ");
    string metodo = Console.ReadLine() ?? "";

    Console.Write("Confirmar compra (S/N): ");
    string respuesta = (Console.ReadLine() ?? "").Trim().ToUpper();

    if (respuesta != "S")
    {
        Console.WriteLine("Compra cancelada.");
        Pausa();
        return;
    }

    var resultado = tienda.FinalizarCompra(clienteActual!, carrito, metodo);

    if (resultado.pedido is null || resultado.factura is null)
    {
        Console.WriteLine("No fue posible completar el pago.");
        Pausa();
        return;
    }

    Console.WriteLine($"Pago: {resultado.pago!.Estado}");
    Console.WriteLine($"Pedido #{resultado.pedido.IdPedido} confirmado.");
    resultado.factura.Mostrar();

    Pausa();
}

bool RequiereSesion()
{
    if (clienteActual is not null)
        return true;

    Console.WriteLine("Debe registrarse o iniciar sesion primero.");
    Pausa();
    return false;
}

int LeerEntero(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);

        if (int.TryParse(Console.ReadLine(), out int numero))
            return numero;

        Console.WriteLine("Ingrese un numero valido.");
    }
}

void Pausa()
{
    Console.WriteLine("\nPresione ENTER para continuar...");
    Console.ReadLine();
}
