using CarritoCompras.Models;

namespace CarritoCompras.Services;

public class CarritoService
{
    // Persistencia en memoria usando una lista generica.
    private readonly List<DetalleCarrito> carritoActual = new();

    public IReadOnlyList<DetalleCarrito> ObtenerDetalles()
    {
        return carritoActual.AsReadOnly();
    }

    // CREATE
    public bool AgregarItem(Producto producto, int cantidad)
    {
        if (!producto.HayStock(cantidad))
            return false;

        var existente = carritoActual
            .FirstOrDefault(d => d.Producto.IdProducto == producto.IdProducto);

        if (existente is not null)
        {
            existente.AumentarCantidad(cantidad);
        }
        else
        {
            carritoActual.Add(new DetalleCarrito(producto, cantidad));
        }

        // Se sigue el ejemplo de la presentacion del curso:
        // el stock se actualiza al agregar al carrito.
        producto.DescontarStock(cantidad);
        return true;
    }

    // READ
    public void MostrarCarrito()
    {
        Console.WriteLine("\n========== CARRITO ==========");

        if (carritoActual.Count == 0)
        {
            Console.WriteLine("El carrito esta vacio.");
        }
        else
        {
            for (int i = 0; i < carritoActual.Count; i++)
            {
                var detalle = carritoActual[i];
                Console.WriteLine(
                    $"{i + 1}. {detalle.Producto.Nombre} | " +
                    $"Cantidad: {detalle.Cantidad} | " +
                    $"Subtotal: Q{detalle.Subtotal:0.00}");
            }

            Console.WriteLine($"TOTAL: Q{CalcularTotal():0.00}");
        }

        Console.WriteLine("=============================\n");
    }

    // UPDATE
    public bool ModificarCantidad(int idProducto, int nuevaCantidad)
    {
        if (nuevaCantidad <= 0)
            return false;

        var detalle = carritoActual
            .FirstOrDefault(d => d.Producto.IdProducto == idProducto);

        if (detalle is null)
            return false;

        int diferencia = nuevaCantidad - detalle.Cantidad;

        if (diferencia > 0)
        {
            if (!detalle.Producto.HayStock(diferencia))
                return false;

            detalle.Producto.DescontarStock(diferencia);
        }
        else if (diferencia < 0)
        {
            detalle.Producto.AumentarStock(-diferencia);
        }

        detalle.CambiarCantidad(nuevaCantidad);
        return true;
    }

    // DELETE
    public bool EliminarItem(int idProducto)
    {
        var detalle = carritoActual
            .FirstOrDefault(d => d.Producto.IdProducto == idProducto);

        if (detalle is null)
            return false;

        // Regresamos al inventario las unidades reservadas en el carrito.
        detalle.Producto.AumentarStock(detalle.Cantidad);
        carritoActual.Remove(detalle);
        return true;
    }

    public decimal CalcularTotal()
    {
        return carritoActual.Sum(d => d.Subtotal);
    }

    public List<DetalleCarrito> CrearCopiaDetalles()
    {
        return carritoActual
            .Select(d => new DetalleCarrito(d.Producto, d.Cantidad))
            .ToList();
    }

    public void VaciarDespuesDeCompra()
    {
        // No se devuelve stock porque la compra ya fue confirmada.
        carritoActual.Clear();
    }
}
