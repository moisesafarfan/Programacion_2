using VeterinariaControlClinico.Database;
using VeterinariaControlClinico.Forms;

namespace VeterinariaControlClinico
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Antes de mostrar cualquier formulario, se asegura que la
            // base de datos SQLite exista y tenga sus tablas creadas.
            DatabaseHelper.InicializarBaseDeDatos();

            ApplicationConfiguration.Initialize();
            Application.Run(new FormPrincipal());
        }
    }
}
