namespace CarritoCompras.Models;

public class Cliente
{
    public int IdCliente { get; private set; }
    public string Nombre { get; private set; }
    public string Correo { get; private set; }
    private string Contrasena { get; set; }

    public Cliente(int idCliente, string nombre, string correo, string contrasena)
    {
        IdCliente = idCliente;
        Nombre = nombre;
        Correo = correo;
        Contrasena = contrasena;
    }

    public bool ValidarCredenciales(string correo, string contrasena)
    {
        return Correo.Equals(correo, StringComparison.OrdinalIgnoreCase)
               && Contrasena == contrasena;
    }
}
