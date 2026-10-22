namespace VeterinariaControlClinico.Models
{
    /// <summary>
    /// Representa un registro del historial clínico (consulta) de una mascota.
    /// </summary>
    public class HistorialClinico
    {
        public int HistorialId { get; set; }
        public int MascotaId { get; set; }
        public int VeterinarioId { get; set; }
        public DateTime Fecha { get; set; }
        public string? Diagnostico { get; set; }
        public string? Tratamiento { get; set; }
        public string? Observaciones { get; set; }

        // Campos auxiliares para mostrar en grillas.
        public string? NombreMascota { get; set; }
        public string? NombreVeterinario { get; set; }
    }
}
