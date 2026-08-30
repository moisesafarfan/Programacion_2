namespace CarritoCompras.Models;

public class DetalleCarrito
{
    public Producto Producto { get; private set; }
    public int Cantidad { get; private set; }

    public decimal Subtotal => Producto.Precio * Cantidad;

    public DetalleCarrito(Producto producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }

    public void AumentarCantidad(int cantidad)
    {
        if (cantidad > 0)
            Cantidad += cantidad;
    }

    public void CambiarCantidad(int nuevaCantidad)
    {
        if (nuevaCantidad > 0)
            Cantidad = nuevaCantidad;
    }
}
