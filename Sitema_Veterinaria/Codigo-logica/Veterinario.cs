namespace ControlVeterinario.logica
{
    public class Veterinario : Persona
    {
        public int intIdVeterinario { get; set; }
        public string strNumeroColegiado { get; set; }

        public Veterinario(int intIdVeterinario, string strNumeroColegiado,
                           string strNombre, string strDpi, string strTelefono,
                           string strCorreo, string strDireccion)
            : base(strNombre, strDpi, strTelefono, strCorreo, strDireccion)
        {
            this.intIdVeterinario = intIdVeterinario;
            this.strNumeroColegiado = strNumeroColegiado;
        }

        public void actualizarDatos(string strTelefono, string strCorreo)
        {
            this.strTelefono = strTelefono;
            this.strCorreo = strCorreo;
        }

        public override void mostrarInformacion()
        {
            base.mostrarInformacion();
            Console.WriteLine($"ID Veterinario: {intIdVeterinario}");
            Console.WriteLine($"No. Colegiado: {strNumeroColegiado}");
        }
    }
}
