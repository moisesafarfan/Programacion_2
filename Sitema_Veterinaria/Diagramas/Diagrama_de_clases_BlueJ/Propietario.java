import java.util.ArrayList;

public class Propietario extends Persona
{
    private ArrayList<Mascota> lstMascotas;

    public Propietario(String strNombre, String strDpi, String strTelefono,
                       String strCorreo, String strDireccion)
    {
        super(strNombre, strDpi, strTelefono, strCorreo, strDireccion);
        lstMascotas = new ArrayList<Mascota>();
    }

    public void actualizarDatos(String strTelefono, String strCorreo) { }
    public void registrarMascota(Mascota objMascota) { }
    public void consultarMascotas() { }
    public void mostrarInformacion() { }
}
