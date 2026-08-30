public class Producto
{
    private int idProducto;
    private String nombre;
    private String descripcion;
    private double precio;

    public Producto(int idProducto, String nombre, String descripcion, double precio)
    {
        this.idProducto = idProducto;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.precio = precio;
    }

    public int getIdProducto()
    {
        return idProducto;
    }

    public String getNombre()
    {
        return nombre;
    }

    public String getDescripcion()
    {
        return descripcion;
    }

    public double obtenerPrecio()
    {
        return precio;
    }

    public void mostrarInformacion()
    {
        System.out.println("ID: " + idProducto);
        System.out.println("Producto: " + nombre);
        System.out.println("Descripcion: " + descripcion);
        System.out.println("Precio: Q" + precio);
    }
}
