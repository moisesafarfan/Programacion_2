namespace VeterinariaControlClinico.Models
{
    /// <summary>
    /// Representa al dueño (cliente) de una o varias mascotas.
    /// </summary>
    public class Dueno
    {
        public int DuenoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }

        public override string ToString() => Nombre;
    }
}
