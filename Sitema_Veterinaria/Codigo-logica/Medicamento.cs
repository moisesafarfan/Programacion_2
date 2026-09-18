namespace ControlVeterinario.logica
{
    public class Medicamento
    {
        public string strIdMedicamento { get; set; }
        public string strNombre { get; set; }
        public string strDosis { get; set; }
        public string strFrecuencia { get; set; }
        public string strDuracion { get; set; }
        public string strIndicaciones { get; set; }

        public Medicamento(string strIdMedicamento, string strNombre,
                           string strDosis, string strFrecuencia,
                           string strDuracion, string strIndicaciones)
        {
            this.strIdMedicamento = strIdMedicamento;
            this.strNombre = strNombre;
            this.strDosis = strDosis;
            this.strFrecuencia = strFrecuencia;
            this.strDuracion = strDuracion;
            this.strIndicaciones = strIndicaciones;
        }

        public void actualizarDosis(string strDosis) { this.strDosis = strDosis; }
        public void actualizarFrecuencia(string strFrecuencia) { this.strFrecuencia = strFrecuencia; }
        public void actualizarDuracion(string strDuracion) { this.strDuracion = strDuracion; }

        public void mostrarInformacion()
        {
            Console.WriteLine($"Medicamento: {strNombre}");
            Console.WriteLine($"Dosis: {strDosis}");
            Console.WriteLine($"Frecuencia: {strFrecuencia}");
            Console.WriteLine($"Duracion: {strDuracion}");
            Console.WriteLine($"Indicaciones: {strIndicaciones}");
        }
    }
}
