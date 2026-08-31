namespace Carrito.logica
{
    public class Carrito
    {
        public List<Detalle> lstDetalle { get; set; }

        // Constructor
        public Carrito()
        {
            this.lstDetalle = new List<Detalle>();
        }

        public bool agregarProducto(int intCantidad, Producto objProducto)
        {
            // Validaciones
            if (!objProducto.verificarStock(intCantidad))
            {
                Console.WriteLine("No hay stock");
                return false;
            }

            Detalle objDetalle = new Detalle(objProducto, intCantidad);

            lstDetalle.Add(objDetalle);

            return true;
        }
    }
}

//De lo contrario

istDetalle.add(new=Detalle
(intCantidad, objProducto));  

//mostrar carrito
public void mostrarCarrito()
{
    if (istDetalle.cout == 0)
    {
        return;  }
        foreach (var item in lstDetalle)
        { item.MostrarDetalle();}
    
}
