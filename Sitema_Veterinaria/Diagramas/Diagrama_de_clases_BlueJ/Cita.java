public class Cita
{
    private String strIdCita;
    private String dtFechaHora;
    private String strModalidad;
    private double dblPrecio;
    private String strEstado;

    private Propietario objPropietario;
    private Mascota objMascota;
    private Veterinario objVeterinario;

    public Cita(String strIdCita, String dtFechaHora, String strModalidad,
                double dblPrecio, Propietario objPropietario,
                Mascota objMascota, Veterinario objVeterinario)
    {
        this.strIdCita = strIdCita;
        this.dtFechaHora = dtFechaHora;
        this.strModalidad = strModalidad;
        this.dblPrecio = dblPrecio;
        this.objPropietario = objPropietario;
        this.objMascota = objMascota;
        this.objVeterinario = objVeterinario;
    }

    public void actualizarFecha(String dtNuevaFecha) { }
    public void actualizarHora(int intHora, int intMinuto) { }
    public void cambiarModalidad(String strNuevaModalidad) { }
    public void cancelarCita() { }
    public void reagendarCita(String dtNuevaFechaHora) { }
    public void finalizarCita() { }
    public void mostrarCita() { }
}
