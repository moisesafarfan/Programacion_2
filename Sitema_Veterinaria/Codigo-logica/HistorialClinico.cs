namespace ControlVeterinario.logica
{
    public class HistorialClinico
    {
        public string strIdHistorial { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public string strObservacionesGenerales { get; set; }
        public Mascota objMascota { get; set; }
        public List<Consulta> lstConsultas { get; set; }

        public HistorialClinico(string strIdHistorial,
                                Mascota objMascota,
                                string strObservacionesGenerales)
        {
            this.strIdHistorial = strIdHistorial;
            this.dtFechaCreacion = DateTime.Now;
            this.objMascota = objMascota;
            this.strObservacionesGenerales = strObservacionesGenerales;
            lstConsultas = new List<Consulta>();
        }

        public void agregarConsulta(Consulta objConsulta) { lstConsultas.Add(objConsulta); }

        public Consulta? buscarConsulta(string strIdConsulta)
        {
            foreach (Consulta objConsulta in lstConsultas)
            {
                if (objConsulta.strIdConsulta == strIdConsulta) return objConsulta;
            }
            return null;
        }

        public void mostrarHistorial()
        {
            Console.WriteLine($"Historial: {strIdHistorial}");
            Console.WriteLine($"Mascota: {objMascota.strNombre}");
            Console.WriteLine($"Creado: {dtFechaCreacion:dd/MM/yyyy}");
            Console.WriteLine($"Observaciones: {strObservacionesGenerales}");
            foreach (Consulta objConsulta in lstConsultas)
            {
                Console.WriteLine("--------------------");
                objConsulta.mostrarConsulta();
            }
        }
    }
}
