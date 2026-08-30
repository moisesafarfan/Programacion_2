import java.util.ArrayList;

public class Carrito
{
    private ArrayList<DetalleCarrito> detalles;
    private double total;

    public Carrito()
    {
        detalles = new ArrayList<DetalleCarrito>();
        total = 0;
    }

    public void agregarProducto(Producto producto, int cantidad)
    {
        if (cantidad <= 0)
        {
            return;
        }

        for (DetalleCarrito detalle : detalles)
        {
            if (detalle.getProducto().getIdProducto() == producto.getIdProducto())
            {
                detalle.modificarCantidad(detalle.getCantidad() + cantidad);
                calcularTotal();
                return;
            }
        }

        detalles.add(new DetalleCarrito(producto, cantidad));
        calcularTotal();
    }

    public void eliminarProducto(Producto producto)
    {
        for (int i = 0; i < detalles.size(); i++)
        {
            if (detalles.get(i).getProducto().getIdProducto() == producto.getIdProducto())
            {
                detalles.remove(i);
                break;
            }
        }
        calcularTotal();
    }

    public void modificarCantidad(Producto producto, int cantidad)
    {
        for (DetalleCarrito detalle : detalles)
        {
            if (detalle.getProducto().getIdProducto() == producto.getIdProducto())
            {
                detalle.modificarCantidad(cantidad);
                break;
            }
        }
        calcularTotal();
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

    public void mostrarCarrito()
    {
        System.out.println("=== CARRITO ===");

        if (detalles.isEmpty())
        {
            System.out.println("Carrito vacio.");
        }
        else
        {
            for (DetalleCarrito detalle : detalles)
            {
                detalle.mostrarDetalle();
            }
            System.out.println("Total: Q" + calcularTotal());
        }
    }

    public void vaciarCarrito()
    {
        detalles.clear();
        total = 0;
    }

    public ArrayList<DetalleCarrito> getDetalles()
    {
        return new ArrayList<DetalleCarrito>(detalles);
    }
}
