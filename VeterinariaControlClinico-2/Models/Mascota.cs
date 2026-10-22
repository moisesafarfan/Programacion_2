namespace VeterinariaControlClinico.Models
{
    /// <summary>
    /// Representa a una mascota (paciente) asociada a un dueño.
    /// </summary>
    public class Mascota
    {
        public int MascotaId { get; set; }
        public int DuenoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Especie { get; set; }
        public string? Raza { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public double? Peso { get; set; }

        // Campo auxiliar para mostrar el nombre del dueño en las grillas
        // sin tener que hacer un JOIN manual en cada pantalla.
        public string? NombreDueno { get; set; }

        public override string ToString() => Nombre;
    }
}
