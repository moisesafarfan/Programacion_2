namespace ControlVeterinario.logica
{
    public class Propietario : Persona
    {
        public List<Mascota> lstMascotas { get; set; }

        public Propietario(string strNombre, string strDpi, string strTelefono,
                           string strCorreo, string strDireccion)
            : base(strNombre, strDpi, strTelefono, strCorreo, strDireccion)
        {
            lstMascotas = new List<Mascota>();
        }

        public void actualizarDatos(string strTelefono, string strCorreo)
        {
            this.strTelefono = strTelefono;
            this.strCorreo = strCorreo;
        }

        public void registrarMascota(Mascota objMascota)
        {
            lstMascotas.Add(objMascota);
        }

        public void consultarMascotas()
        {
            foreach (Mascota objMascota in lstMascotas)
            {
                objMascota.mostrarInformacion();
                Console.WriteLine("--------------------");
            }
        }

        public override void mostrarInformacion()
        {
            base.mostrarInformacion();
            Console.WriteLine($"Cantidad de mascotas: {lstMascotas.Count}");
        }
    }
}
