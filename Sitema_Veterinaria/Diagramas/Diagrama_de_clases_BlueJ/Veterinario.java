public class Veterinario extends Persona
{
    private int intIdVeterinario;
    private String strNumeroColegiado;

    public Veterinario(int intIdVeterinario, String strNumeroColegiado,
                       String strNombre, String strDpi, String strTelefono,
                       String strCorreo, String strDireccion)
    {
        super(strNombre, strDpi, strTelefono, strCorreo, strDireccion);
        this.intIdVeterinario = intIdVeterinario;
        this.strNumeroColegiado = strNumeroColegiado;
    }

    public void actualizarDatos(String strTelefono, String strCorreo) { }
    public void mostrarInformacion() { }
}
