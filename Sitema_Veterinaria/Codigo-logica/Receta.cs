namespace ControlVeterinario.logica
{
    public class Receta
    {
        public string strIdReceta { get; set; }
        public DateTime dtFecha { get; set; }
        public string strIndicacionesGenerales { get; set; }
        public string strObservaciones { get; set; }
        public List<Medicamento> lstMedicamentos { get; set; }

        public Receta(string strIdReceta, DateTime dtFecha,
                      string strIndicacionesGenerales, string strObservaciones)
        {
            this.strIdReceta = strIdReceta;
            this.dtFecha = dtFecha;
            this.strIndicacionesGenerales = strIndicacionesGenerales;
            this.strObservaciones = strObservaciones;
            lstMedicamentos = new List<Medicamento>();
        }

        public void agregarMedicamento(Medicamento objMedicamento) { lstMedicamentos.Add(objMedicamento); }
        public void eliminarMedicamento(Medicamento objMedicamento) { lstMedicamentos.Remove(objMedicamento); }
        public void actualizarIndicaciones(string strIndicaciones) { strIndicacionesGenerales = strIndicaciones; }

        public void mostrarReceta()
        {
            Console.WriteLine($"Receta: {strIdReceta}");
            Console.WriteLine($"Fecha: {dtFecha:dd/MM/yyyy}");
            Console.WriteLine($"Indicaciones: {strIndicacionesGenerales}");
            foreach (Medicamento objMedicamento in lstMedicamentos)
            {
                objMedicamento.mostrarInformacion();
                Console.WriteLine("--------------------");
            }
        }
    }
}
