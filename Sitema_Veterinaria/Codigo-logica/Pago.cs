namespace ControlVeterinario.logica
{
    public class Pago
    {
        public string strIdPago { get; set; }
        public DateTime dtFecha { get; set; }
        public double dblMonto { get; set; }
        public string strMetodoPago { get; set; }
        public string strReferencia { get; set; }

        public Pago(string strIdPago, DateTime dtFecha, double dblMonto,
                    string strMetodoPago, string strReferencia)
        {
            this.strIdPago = strIdPago;
            this.dtFecha = dtFecha;
            this.dblMonto = dblMonto;
            this.strMetodoPago = strMetodoPago;
            this.strReferencia = strReferencia;
        }

        public bool validarMonto(double dblTotalFactura) { return dblMonto >= dblTotalFactura; }

        public void mostrarPago()
        {
            Console.WriteLine($"Pago: {strIdPago}");
            Console.WriteLine($"Fecha: {dtFecha:dd/MM/yyyy}");
            Console.WriteLine($"Monto: Q{dblMonto:F2}");
            Console.WriteLine($"Metodo: {strMetodoPago}");
            Console.WriteLine($"Referencia: {strReferencia}");
        }
    }
}
