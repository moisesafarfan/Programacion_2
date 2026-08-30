import java.util.ArrayList;

public class Pedido
{
    private int idPedido;
    private Cliente cliente;
    private ArrayList<DetalleCarrito> detalles;
    private double total;
    private String estado;

    public Pedido(int idPedido, Cliente cliente, Carrito carrito)
    {
        this.idPedido = idPedido;
        this.cliente = cliente;
        this.detalles = carrito.getDetalles();
        this.total = carrito.calcularTotal();
        this.estado = "Pendiente";
    }

    public double calcularTotal()
    {
        total = 0;

        for (DetalleCarrito detalle : detalles)
        {
            total = total + detalle.calcularSubtotal();
        }

        return total;
    }

    public void confirmarPedido()
    {
        estado = "Confirmado";
    }

    public void cambiarEstado(String estado)
    {
        this.estado = estado;
    }

    public int getIdPedido()
    {
        return idPedido;
    }

    public double getTotal()
    {
        return total;
    }

    public String getEstado()
    {
        return estado;
    }

    public Cliente getCliente()
    {
        return cliente;
    }

    public void mostrarPedido()
    {
        System.out.println("Pedido #" + idPedido);
        System.out.println("Cliente: " + cliente.getNombre());

        for (DetalleCarrito detalle : detalles)
        {
            detalle.mostrarDetalle();
        }

        System.out.println("Total: Q" + total);
        System.out.println("Estado: " + estado);
    }
}
