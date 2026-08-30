namespace CarritoCompras.Models;

public class Factura
{
    public int NumeroFactura { get; private set; }
    public Pedido Pedido { get; private set; }
    public DateTime Fecha { get; private set; }

    public Factura(int numeroFactura, Pedido pedido)
    {
        NumeroFactura = numeroFactura;
        Pedido = pedido;
        Fecha = DateTime.Now;
    }

    public void Mostrar()
    {
        Console.WriteLine("\n========== FACTURA ==========");
        Console.WriteLine($"Factura No.: {NumeroFactura}");
        Console.WriteLine($"Fecha: {Fecha:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Cliente: {Pedido.Cliente.Nombre}");
        Console.WriteLine($"Pedido No.: {Pedido.IdPedido}");

        foreach (var detalle in Pedido.Detalles)
        {
            Console.WriteLine(
                $"{detalle.Producto.Nombre} x {detalle.Cantidad} = Q{detalle.Subtotal:0.00}");
        }

        Console.WriteLine($"TOTAL: Q{Pedido.Total:0.00}");
        Console.WriteLine("=============================\n");
    }
}
