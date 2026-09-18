public class Factura
{
    private String strNumeroFactura;
    private String dtFecha;
    private double dblSubtotal;
    private double dblImpuesto;
    private double dblTotal;
    private String strEstado;

    private Propietario objPropietario;
    private Consulta objConsulta;
    private Pago objPago;

    public double calcularImpuesto() { return 0; }
    public double calcularTotal() { return 0; }
    public boolean registrarPago(Pago objPago) { return false; }
    public void marcarPagada() { }
    public void mostrarFactura() { }
}
