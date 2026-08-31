using System.Dynamic;
using carrito.logica;

namespace carrito.logica
{
    public class Producto
    {
        public int intStock { get; set; }
        public double dblPrecio { get; set; }
        public string strNombre { get; set; }
        public string strCodigo { get; set; }

        // Constructor
        public Producto(int intStock, double dblPrecio, string strNombre, string strCodigo)
        {
            this.intStock = intStock;
            this.dblPrecio = dblPrecio;
            this.strNombre = strNombre;
            this.strCodigo = strCodigo;
        }

        public bool verificarStock(int intCantidad)
        {
            return intCantidad > 0 && this.intStock >= intCantidad;
        }

        public void mostrarProducto()
        {
            Console.WriteLine($"{strCodigo} {strNombre}");
        }
    }
}


using System;

namespace carrito.logica
{
    public class Producto
    {
        public int intStock { get; set; }
        public double dblPrecio { get; set; }
        public string strNombre { get; set; }
        public string strCodigo { get; set; }

        public Producto(int intStock, double dblPrecio, string strNombre, string strCodigo)
        {
            this.intStock = intStock;
            this.dblPrecio = dblPrecio;
            this.strNombre = strNombre;
            this.strCodigo = strCodigo;
        }

        public bool verificarStock(int intCantidad)
        {
            return intCantidad > 0 && this.intStock >= intCantidad;
        }

        public void mostrarProducto()
        {
            Console.WriteLine($"{strCodigo} {strNombre}");
        }
    }
}

public void MostrarDetalle()

{
    double dblSubtotal = CalcularSubtotal();

    Console.WriteLine($"{objProducto.strNombre} {dblSubtotal}");
}
