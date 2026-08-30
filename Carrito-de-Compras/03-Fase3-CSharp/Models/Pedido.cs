namespace CarritoCompras.Models;

public class Pedido
{
    public int IdPedido { get; private set; }
    public Cliente Cliente { get; private set; }
    public List<DetalleCarrito> Detalles { get; private set; }
    public decimal Total { get; private set; }
    public string Estado { get; private set; }

    public Pedido(int idPedido, Cliente cliente, List<DetalleCarrito> detalles, decimal total)
    {
        IdPedido = idPedido;
        Cliente = cliente;
        Detalles = detalles;
        Total = total;
        Estado = "Confirmado";
    }
}
