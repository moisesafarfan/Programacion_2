namespace ControlVeterinario.logica
{
    public class Cita
    {
        public string strIdCita { get; set; }
        public DateTime dtFechaHora { get; set; }
        public string strModalidad { get; set; }
        public double dblPrecio { get; set; }
        public string strEstado { get; set; }

        public Propietario objPropietario { get; set; }
        public Mascota objMascota { get; set; }
        public Veterinario objVeterinario { get; set; }

        public Cita(string strIdCita, DateTime dtFechaHora,
                    string strModalidad, double dblPrecio,
                    Propietario objPropietario, Mascota objMascota,
                    Veterinario objVeterinario)
        {
            this.strIdCita = strIdCita;
            this.dtFechaHora = dtFechaHora;
            this.strModalidad = strModalidad;
            this.dblPrecio = dblPrecio;
            this.strEstado = "Agendada";
            this.objPropietario = objPropietario;
            this.objMascota = objMascota;
            this.objVeterinario = objVeterinario;
        }

        public void actualizarFecha(DateTime dtNuevaFecha)
        {
            dtFechaHora = new DateTime(dtNuevaFecha.Year, dtNuevaFecha.Month,
                dtNuevaFecha.Day, dtFechaHora.Hour, dtFechaHora.Minute, 0);
        }

        public void actualizarHora(int intHora, int intMinuto)
        {
            dtFechaHora = new DateTime(dtFechaHora.Year, dtFechaHora.Month,
                dtFechaHora.Day, intHora, intMinuto, 0);
        }

        public void cambiarModalidad(string strNuevaModalidad)
        {
            strModalidad = strNuevaModalidad;
        }

        public void cancelarCita()
        {
            strEstado = "Cancelada";
        }

        public void reagendarCita(DateTime dtNuevaFechaHora)
        {
            dtFechaHora = dtNuevaFechaHora;
            strEstado = "Reagendada";
        }

        public void finalizarCita()
        {
            strEstado = "Finalizada";
        }

        public void mostrarCita()
        {
            Console.WriteLine($"Cita: {strIdCita}");
            Console.WriteLine($"Fecha: {dtFechaHora:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Mascota: {objMascota.strNombre}");
            Console.WriteLine($"Veterinario: {objVeterinario.strNombre}");
            Console.WriteLine($"Modalidad: {strModalidad}");
            Console.WriteLine($"Precio: Q{dblPrecio:F2}");
            Console.WriteLine($"Estado: {strEstado}");
        }
    }
}
