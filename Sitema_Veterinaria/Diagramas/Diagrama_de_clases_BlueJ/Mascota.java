public class Mascota
{
    private String strNombre;
    private String strEspecie;
    private String strRaza;
    private String strSexo;
    private String dtFechaNacimiento;
    private double dblPeso;
    private double dblAltura;
    private String strColor;

    public Mascota(String strNombre, String strEspecie, String strRaza,
                   String strSexo, String dtFechaNacimiento,
                   double dblPeso, double dblAltura, String strColor)
    {
        this.strNombre = strNombre;
        this.strEspecie = strEspecie;
        this.strRaza = strRaza;
        this.strSexo = strSexo;
        this.dtFechaNacimiento = dtFechaNacimiento;
        this.dblPeso = dblPeso;
        this.dblAltura = dblAltura;
        this.strColor = strColor;
    }

    public void actualizarPeso(double dblNuevoPeso) { }
    public void actualizarAltura(double dblNuevaAltura) { }
    public void actualizarDatos(String strNuevaRaza, String strNuevoColor) { }
    public void mostrarInformacion() { }
}
