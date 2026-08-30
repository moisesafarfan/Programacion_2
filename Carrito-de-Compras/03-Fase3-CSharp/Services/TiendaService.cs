using CarritoCompras.Models;

namespace CarritoCompras.Services;

public class TiendaService
{
    public List<Producto> Productos { get; } = new();
    public List<Cliente> Clientes { get; } = new();
    public List<Pedido> Pedidos { get; } = new();

    private int siguienteCliente = 1;
    private int siguientePedido = 1001;
    private int siguientePago = 5001;
    private int siguienteFactura = 9001;

    public TiendaService()
    {
        CargarProductosIniciales();
    }

    private void CargarProductosIniciales()
    {
        Productos.Add(new Producto(1, "Teclado", "Teclado mecanico", 350m, 10));
        Productos.Add(new Producto(2, "Mouse", "Mouse inalambrico", 150m, 20));
        Productos.Add(new Producto(3, "Audifonos", "Audifonos USB", 275m, 8));
        Productos.Add(new Producto(4, "Monitor", "Monitor LED 24 pulgadas", 1200m, 5));
    }

    public Cliente RegistrarCliente(string nombre, string correo, string contrasena)
    {
        var cliente = new Cliente(siguienteCliente++, nombre, correo, contrasena);
        Clientes.Add(cliente);
        return cliente;
    }

    public Cliente? IniciarSesion(string correo, string contrasena)
    {
        return Clientes.FirstOrDefault(c => c.ValidarCredenciales(correo, contrasena));
    }

    public Producto? BuscarProducto(int idProducto)
    {
        return Productos.FirstOrDefault(p => p.IdProducto == idProducto);
    }

    public void MostrarProductos()
    {
        Console.WriteLine("\n======== PRODUCTOS ========");
        foreach (var producto in Productos)
        {
            producto.MostrarInformacion();
        }
        Console.WriteLine("===========================\n");
    }

    public (Pedido? pedido, Pago? pago, Factura? factura) FinalizarCompra(
        Cliente cliente,
        CarritoService carrito,
        string metodoPago)
    {
        if (carrito.ObtenerDetalles().Count == 0)
            return (null, null, null);

        decimal total = carrito.CalcularTotal();
        var pago = new Pago(siguientePago++, total, metodoPago);

        if (!pago.ProcesarPago())
            return (null, pago, null);

        var pedido = new Pedido(
            siguientePedido++,
            cliente,
            carrito.CrearCopiaDetalles(),
            total);

        Pedidos.Add(pedido);

        var factura = new Factura(siguienteFactura++, pedido);

        carrito.VaciarDespuesDeCompra();

        return (pedido, pago, factura);
    }
}
