public class Inventario
{
    private Producto producto;
    private int stock;

    public Inventario(Producto producto, int stock)
    {
        this.producto = producto;
        this.stock = stock;
    }

    public Producto getProducto()
    {
        return producto;
    }

    public int consultarStock()
    {
        return stock;
    }

    public boolean verificarDisponibilidad(int cantidad)
    {
        return cantidad > 0 && cantidad <= stock;
    }

    public void descontarStock(int cantidad)
    {
        if (verificarDisponibilidad(cantidad))
        {
            stock = stock - cantidad;
        }
    }

    public void aumentarStock(int cantidad)
    {
        if (cantidad > 0)
        {
            stock = stock + cantidad;
        }
    }
}
