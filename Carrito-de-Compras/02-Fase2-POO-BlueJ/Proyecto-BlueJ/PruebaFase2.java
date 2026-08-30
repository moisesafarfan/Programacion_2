public class PruebaFase2
{
    public static void main(String[] args)
    {
        Producto teclado = new Producto(1, "Teclado", "Teclado mecanico", 350.00);
        Producto mouse = new Producto(2, "Mouse", "Mouse inalambrico", 150.00);

        Inventario inventarioTeclado = new Inventario(teclado, 10);
        Inventario inventarioMouse = new Inventario(mouse, 20);

        Cliente cliente = new Cliente(1, "Cliente Demo", "cliente@correo.com", "1234");

        if (inventarioTeclado.verificarDisponibilidad(2))
        {
            cliente.getCarrito().agregarProducto(teclado, 2);
        }

        if (inventarioMouse.verificarDisponibilidad(1))
        {
            cliente.getCarrito().agregarProducto(mouse, 1);
        }

        cliente.verCarrito();

        Pedido pedido = new Pedido(1001, cliente, cliente.getCarrito());

        Pago pago = new Pago(5001, pedido.getTotal(), "Tarjeta");

        if (pago.procesarPago())
        {
            inventarioTeclado.descontarStock(2);
            inventarioMouse.descontarStock(1);

            pedido.confirmarPedido();

            Factura factura = new Factura(9001, pedido, "29/08/2026");
            factura.generarFactura();
            factura.mostrarFactura();
        }
    }
}
