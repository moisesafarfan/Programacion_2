namespace VeterinariaControlClinico.Models
{
    /// <summary>
    /// Representa a un médico veterinario que atiende citas y consultas.
    /// </summary>
    public class Veterinario
    {
        public int VeterinarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Especialidad { get; set; }
        public string? Telefono { get; set; }

        public override string ToString() => Nombre;
    }
}
