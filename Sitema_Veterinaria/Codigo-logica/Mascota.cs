namespace ControlVeterinario.logica
{
    public class Mascota
    {
        public string strNombre { get; set; }
        public string strEspecie { get; set; }
        public string strRaza { get; set; }
        public string strSexo { get; set; }
        public DateTime dtFechaNacimiento { get; set; }
        public double dblPeso { get; set; }
        public double dblAltura { get; set; }
        public string strColor { get; set; }

        public Mascota(string strNombre, string strEspecie, string strRaza,
                       string strSexo, DateTime dtFechaNacimiento,
                       double dblPeso, double dblAltura, string strColor)
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

        public void actualizarPeso(double dblNuevoPeso)
        {
            if (dblNuevoPeso > 0)
            {
                this.dblPeso = dblNuevoPeso;
            }
        }

        public void actualizarAltura(double dblNuevaAltura)
        {
            if (dblNuevaAltura > 0)
            {
                this.dblAltura = dblNuevaAltura;
            }
        }

        public void actualizarDatos(string strNuevaRaza, string strNuevoColor)
        {
            this.strRaza = strNuevaRaza;
            this.strColor = strNuevoColor;
        }

        public void mostrarInformacion()
        {
            Console.WriteLine($"Nombre: {strNombre}");
            Console.WriteLine($"Especie: {strEspecie}");
            Console.WriteLine($"Raza: {strRaza}");
            Console.WriteLine($"Sexo: {strSexo}");
            Console.WriteLine($"Fecha nacimiento: {dtFechaNacimiento:dd/MM/yyyy}");
            Console.WriteLine($"Peso: {dblPeso} kg");
            Console.WriteLine($"Altura: {dblAltura}");
            Console.WriteLine($"Color: {strColor}");
        }
    }
}
