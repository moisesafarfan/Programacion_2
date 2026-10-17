namespace PrestamosBiblioteca.Dominio;

/// <summary>
/// Representa el préstamo de un libro a un lector.
/// </summary>
internal sealed class Prestamo
{
    public int LectorId { get; }
    public int LibroId { get; }
    public DateTime FechaPrestamo { get; }
    public DateTime FechaLimite { get; }
    public DateTime? FechaDevolucion { get; }

    public bool EstaActivo => !FechaDevolucion.HasValue;

    public Prestamo(
        int lectorId,
        int libroId,
        DateTime fechaPrestamo,
        DateTime fechaLimite,
        DateTime? fechaDevolucion = null)
    {
        if (lectorId <= 0)
            throw new ArgumentOutOfRangeException(nameof(lectorId), "El Id del lector debe ser mayor que cero.");

        if (libroId <= 0)
            throw new ArgumentOutOfRangeException(nameof(libroId), "El Id del libro debe ser mayor que cero.");

        fechaPrestamo = fechaPrestamo.Date;
        fechaLimite = fechaLimite.Date;
        fechaDevolucion = fechaDevolucion?.Date;

        if (fechaLimite < fechaPrestamo)
            throw new ArgumentException("La fecha limite no puede ser anterior a la fecha de prestamo.");

        if (fechaDevolucion.HasValue && fechaDevolucion.Value < fechaPrestamo)
            throw new ArgumentException("La fecha de devolucion no puede ser anterior a la fecha de prestamo.");

        LectorId = lectorId;
        LibroId = libroId;
        FechaPrestamo = fechaPrestamo;
        FechaLimite = fechaLimite;
        FechaDevolucion = fechaDevolucion;
    }
}
