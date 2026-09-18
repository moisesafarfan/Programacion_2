namespace ControlVeterinario.logica
{
    public class Consulta
    {
        public string strIdConsulta { get; set; }
        public DateTime dtFecha { get; set; }
        public double dblPeso { get; set; }
        public string strSintomas { get; set; }
        public string strDiagnostico { get; set; }
        public string strTratamiento { get; set; }
        public string strObservaciones { get; set; }
        public string strEstado { get; set; }

        public Mascota objMascota { get; set; }
        public Veterinario objVeterinario { get; set; }
        public Cita objCita { get; set; }
        public Receta? objReceta { get; set; }

        public Consulta(string strIdConsulta, DateTime dtFecha,
                        double dblPeso, string strSintomas,
                        Mascota objMascota, Veterinario objVeterinario,
                        Cita objCita)
        {
            this.strIdConsulta = strIdConsulta;
            this.dtFecha = dtFecha;
            this.dblPeso = dblPeso;
            this.strSintomas = strSintomas;
            this.strDiagnostico = "";
            this.strTratamiento = "";
            this.strObservaciones = "";
            this.strEstado = "En proceso";
            this.objMascota = objMascota;
            this.objVeterinario = objVeterinario;
            this.objCita = objCita;
            this.objReceta = null;
        }

        public void registrarSintomas(string strSintomas) { this.strSintomas = strSintomas; }
        public void registrarDiagnostico(string strDiagnostico) { this.strDiagnostico = strDiagnostico; }
        public void registrarTratamiento(string strTratamiento) { this.strTratamiento = strTratamiento; }
        public void modificarTratamiento(string strNuevoTratamiento) { strTratamiento = strNuevoTratamiento; }
        public void agregarReceta(Receta objReceta) { this.objReceta = objReceta; }

        public void finalizarConsulta()
        {
            strEstado = "Finalizada";
            objMascota.actualizarPeso(dblPeso);
            objCita.finalizarCita();
        }

        public void mostrarConsulta()
        {
            Console.WriteLine($"Consulta: {strIdConsulta}");
            Console.WriteLine($"Fecha: {dtFecha:dd/MM/yyyy}");
            Console.WriteLine($"Mascota: {objMascota.strNombre}");
            Console.WriteLine($"Veterinario: {objVeterinario.strNombre}");
            Console.WriteLine($"Sintomas: {strSintomas}");
            Console.WriteLine($"Diagnostico: {strDiagnostico}");
            Console.WriteLine($"Tratamiento: {strTratamiento}");
            Console.WriteLine($"Estado: {strEstado}");
        }
    }
}
