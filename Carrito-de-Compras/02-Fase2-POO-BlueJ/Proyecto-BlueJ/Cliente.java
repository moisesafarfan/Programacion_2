public class Cliente
{
    private int idCliente;
    private String nombre;
    private String correo;
    private String contrasena;
    private Carrito carrito;

    public Cliente(int idCliente, String nombre, String correo, String contrasena)
    {
        this.idCliente = idCliente;
        this.nombre = nombre;
        this.correo = correo;
        this.contrasena = contrasena;
        this.carrito = new Carrito();
    }

    public boolean iniciarSesion(String correo, String contrasena)
    {
        return this.correo.equals(correo) && this.contrasena.equals(contrasena);
    }

    public void verCarrito()
    {
        carrito.mostrarCarrito();
    }

    public Carrito getCarrito()
    {
        return carrito;
    }

    public String getNombre()
    {
        return nombre;
    }

    public int getIdCliente()
    {
        return idCliente;
    }
}
