namespace CarritoCompras.Models;

public class Producto
{
    public int IdProducto { get; private set; }
    public string Nombre { get; private set; }
    public string Descripcion { get; private set; }
    public decimal Precio { get; private set; }
    public int Stock { get; private set; }

    public Producto(int idProducto, string nombre, string descripcion, decimal precio, int stock)
    {
        IdProducto = idProducto;
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
        Stock = stock;
    }

    public bool HayStock(int cantidad)
    {
        return cantidad > 0 && Stock >= cantidad;
    }

    public bool DescontarStock(int cantidad)
    {
        if (!HayStock(cantidad))
            return false;

        Stock -= cantidad;
        return true;
    }

    public void AumentarStock(int cantidad)
    {
        if (cantidad > 0)
            Stock += cantidad;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"{IdProducto}. {Nombre} | Q{Precio:0.00} | Stock: {Stock}");
        Console.WriteLine($"   {Descripcion}");
    }
}
