namespace CarritoCompras.Models;

public class Pago
{
    public int IdPago { get; private set; }
    public decimal Monto { get; private set; }
    public string MetodoPago { get; private set; }
    public string Estado { get; private set; }

    public Pago(int idPago, decimal monto, string metodoPago)
    {
        IdPago = idPago;
        Monto = monto;
        MetodoPago = metodoPago;
        Estado = "Pendiente";
    }

    public bool ProcesarPago()
    {
        if (Monto <= 0 || string.IsNullOrWhiteSpace(MetodoPago))
        {
            Estado = "Rechazado";
            return false;
        }

        Estado = "Aprobado";
        return true;
    }
}
