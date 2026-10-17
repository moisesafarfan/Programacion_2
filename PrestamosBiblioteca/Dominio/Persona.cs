namespace PrestamosBiblioteca.Dominio;

/// <summary>
/// Clase base para representar a una persona dentro del sistema.
/// Se utiliza para demostrar herencia y polimorfismo con la clase Lector.
/// </summary>
internal abstract class Persona
{
    public string Codigo { get; }
    public string Nombre { get; }

    protected Persona(string codigo, string nombre)
    {
        Codigo = ValidarTexto(codigo, nameof(codigo));
        Nombre = ValidarTexto(nombre, nameof(nombre));
    }

    public virtual string ObtenerInformacion() =>
        $"Codigo: {Codigo} | Nombre: {Nombre}";

    protected static string ValidarTexto(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"El campo '{campo}' es obligatorio.");

        return valor.Trim();
    }
}
