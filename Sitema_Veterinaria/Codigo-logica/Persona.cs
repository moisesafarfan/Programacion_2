namespace ControlVeterinario.logica
{
    public class Persona
    {
        public string strNombre { get; set; }
        public string strDpi { get; set; }
        public string strTelefono { get; set; }
        public string strCorreo { get; set; }
        public string strDireccion { get; set; }

        public Persona(string strNombre, string strDpi, string strTelefono,
                       string strCorreo, string strDireccion)
        {
            this.strNombre = strNombre;
            this.strDpi = strDpi;
            this.strTelefono = strTelefono;
            this.strCorreo = strCorreo;
            this.strDireccion = strDireccion;
        }

        public virtual void mostrarInformacion()
        {
            Console.WriteLine($"Nombre: {strNombre}");
            Console.WriteLine($"DPI: {strDpi}");
            Console.WriteLine($"Telefono: {strTelefono}");
            Console.WriteLine($"Correo: {strCorreo}");
            Console.WriteLine($"Direccion: {strDireccion}");
        }
    }
}
