public class DetalleCarrito
{
    private Producto producto;
    private int cantidad;
    private double subtotal;

    public DetalleCarrito(Producto producto, int cantidad)
    {
        this.producto = producto;
        this.cantidad = cantidad;
        calcularSubtotal();
    }

    public Producto getProducto()
    {
        return producto;
    }

    public int getCantidad()
    {
        return cantidad;
    }

    public double calcularSubtotal()
    {
        subtotal = producto.obtenerPrecio() * cantidad;
        return subtotal;
    }

    public void modificarCantidad(int cantidad)
    {
        if (cantidad > 0)
        {
            this.cantidad = cantidad;
            calcularSubtotal();
        }
    }

    public void mostrarDetalle()
    {
        System.out.println(producto.getNombre() + " x " + cantidad + " = Q" + subtotal);
    }
}
