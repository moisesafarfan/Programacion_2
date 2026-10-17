namespace PrestamosBiblioteca.Dominio;

/// <summary>
/// Representa un libro disponible en la biblioteca.
/// </summary>
internal sealed class Libro
{
    public string Codigo { get; }
    public string Titulo { get; }
    public string Autor { get; }
    public int Ejemplares { get; }

    public Libro(string codigo, string titulo, string autor, int ejemplares)
    {
        Codigo = ValidarTexto(codigo, nameof(codigo));
        Titulo = ValidarTexto(titulo, nameof(titulo));
        Autor = ValidarTexto(autor, nameof(autor));

        if (ejemplares < 0)
            throw new ArgumentOutOfRangeException(nameof(ejemplares), "La cantidad de ejemplares no puede ser negativa.");

        Ejemplares = ejemplares;
    }

    private static string ValidarTexto(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"El campo '{campo}' es obligatorio.");

        return valor.Trim();
    }
}
