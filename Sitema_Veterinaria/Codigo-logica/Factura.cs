namespace ControlVeterinario.logica
{
    public class Factura
    {
        public string strNumeroFactura { get; set; }
        public DateTime dtFecha { get; set; }
        public double dblSubtotal { get; set; }
        public double dblImpuesto { get; set; }
        public double dblTotal { get; set; }
        public string strEstado { get; set; }

        public Propietario objPropietario { get; set; }
        public Consulta objConsulta { get; set; }
        public Pago? objPago { get; set; }

        public Factura(string strNumeroFactura,
                       Propietario objPropietario,
                       Consulta objConsulta,
                       double dblSubtotal)
        {
            this.strNumeroFactura = strNumeroFactura;
            this.dtFecha = DateTime.Now;
            this.objPropietario = objPropietario;
            this.objConsulta = objConsulta;
            this.dblSubtotal = dblSubtotal;
            this.strEstado = "Pendiente";
            this.objPago = null;
            calcularTotal();
        }

        public double calcularImpuesto()
        {
            dblImpuesto = dblSubtotal * 0.12;
            return dblImpuesto;
        }

        public double calcularTotal()
        {
            calcularImpuesto();
            dblTotal = dblSubtotal + dblImpuesto;
            return dblTotal;
        }

        public bool registrarPago(Pago objPago)
        {
            if (objPago.validarMonto(dblTotal))
            {
                this.objPago = objPago;
                marcarPagada();
                return true;
            }
            return false;
        }

        public void marcarPagada() { strEstado = "Pagada"; }

        public void mostrarFactura()
        {
            Console.WriteLine($"Factura: {strNumeroFactura}");
            Console.WriteLine($"Propietario: {objPropietario.strNombre}");
            Console.WriteLine($"Subtotal: Q{dblSubtotal:F2}");
            Console.WriteLine($"IVA: Q{dblImpuesto:F2}");
            Console.WriteLine($"Total: Q{dblTotal:F2}");
            Console.WriteLine($"Estado: {strEstado}");
        }
    }
}
