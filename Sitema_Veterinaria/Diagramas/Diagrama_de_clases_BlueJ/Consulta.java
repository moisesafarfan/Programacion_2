public class Consulta
{
    private String strIdConsulta;
    private String dtFecha;
    private double dblPeso;
    private String strSintomas;
    private String strDiagnostico;
    private String strTratamiento;
    private String strObservaciones;
    private String strEstado;

    private Mascota objMascota;
    private Veterinario objVeterinario;
    private Cita objCita;
    private Receta objReceta;

    public void registrarSintomas(String strSintomas) { }
    public void registrarDiagnostico(String strDiagnostico) { }
    public void registrarTratamiento(String strTratamiento) { }
    public void modificarTratamiento(String strNuevoTratamiento) { }
    public void agregarReceta(Receta objReceta) { }
    public void finalizarConsulta() { }
    public void mostrarConsulta() { }
}
