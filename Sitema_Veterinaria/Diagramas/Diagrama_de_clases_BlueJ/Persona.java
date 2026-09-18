public class Persona
{
    protected String strNombre;
    protected String strDpi;
    protected String strTelefono;
    protected String strCorreo;
    protected String strDireccion;

    public Persona(String strNombre, String strDpi, String strTelefono,
                   String strCorreo, String strDireccion)
    {
        this.strNombre = strNombre;
        this.strDpi = strDpi;
        this.strTelefono = strTelefono;
        this.strCorreo = strCorreo;
        this.strDireccion = strDireccion;
    }

    public void mostrarInformacion() { }
}
