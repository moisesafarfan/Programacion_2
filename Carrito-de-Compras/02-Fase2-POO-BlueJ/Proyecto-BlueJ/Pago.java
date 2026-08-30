public class Pago
{
    private int idPago;
    private double monto;
    private String metodoPago;
    private String estado;

    public Pago(int idPago, double monto, String metodoPago)
    {
        this.idPago = idPago;
        this.monto = monto;
        this.metodoPago = metodoPago;
        this.estado = "Pendiente";
    }

    public boolean validarPago()
    {
        return monto > 0 && metodoPago != null && !metodoPago.isEmpty();
    }

    public boolean procesarPago()
    {
        if (validarPago())
        {
            estado = "Aprobado";
            return true;
        }

        estado = "Rechazado";
        return false;
    }

    public String obtenerEstado()
    {
        return estado;
    }
}
