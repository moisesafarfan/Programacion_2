namespace PrestamosBiblioteca.Dominio;

/// <summary>
/// Representa a una persona que puede solicitar libros prestados.
/// </summary>
internal sealed class Lector : Persona
{
    public string Correo { get; }
    public string Telefono { get; }

    public Lector(string codigo, string nombre, string correo, string telefono)
        : base(codigo, nombre)
    {
        Correo = ValidarTexto(correo, nameof(correo));
        Telefono = ValidarTexto(telefono, nameof(telefono));
    }

    public override string ObtenerInformacion() =>
        $"{base.ObtenerInformacion()} | Correo: {Correo} | Telefono: {Telefono}";
}
