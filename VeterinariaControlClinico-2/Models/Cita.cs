namespace VeterinariaControlClinico.Models
{
    /// <summary>
    /// Representa una cita programada entre una mascota y un veterinario.
    /// </summary>
    public class Cita
    {
        public int CitaId { get; set; }
        public int MascotaId { get; set; }
        public int VeterinarioId { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Motivo { get; set; }
        public string Estado { get; set; } = "Programada";

        // Campos auxiliares para mostrar en grillas.
        public string? NombreMascota { get; set; }
        public string? NombreVeterinario { get; set; }
    }
}
