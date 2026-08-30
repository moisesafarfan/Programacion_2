public class Factura
{
    private int numeroFactura;
    private Pedido pedido;
    private String fecha;
    private double total;

    public Factura(int numeroFactura, Pedido pedido, String fecha)
    {
        this.numeroFactura = numeroFactura;
        this.pedido = pedido;
        this.fecha = fecha;
        this.total = pedido.getTotal();
    }

    public void generarFactura()
    {
        System.out.println("Factura generada correctamente.");
    }

    public void mostrarFactura()
    {
        System.out.println("=== FACTURA ===");
        System.out.println("Numero: " + numeroFactura);
        System.out.println("Fecha: " + fecha);
        System.out.println("Cliente: " + pedido.getCliente().getNombre());
        System.out.println("Pedido: " + pedido.getIdPedido());
        System.out.println("Total: Q" + total);
    }
}
