using Microsoft.Data.Sqlite;

namespace PrestamosBiblioteca.Persistencia;

/// <summary>
/// Crea la base de datos SQLite y sus tablas la primera vez que se ejecuta el programa.
/// </summary>
internal sealed class InicializadorBaseDatos
{
    private readonly string _rutaScript;

    public string CadenaConexion { get; }

    public InicializadorBaseDatos()
    {
        var rutaBaseDatos = Path.Combine(Environment.CurrentDirectory, "PrestamosBiblioteca.sqlite");
        _rutaScript = Path.Combine(AppContext.BaseDirectory, "BaseDatos", "Crear_Tablas.sql");

        var constructor = new SqliteConnectionStringBuilder
        {
            DataSource = rutaBaseDatos,
            ForeignKeys = true
        };

        CadenaConexion = constructor.ToString();
    }

    public void Inicializar()
    {
        if (!File.Exists(_rutaScript))
            throw new FileNotFoundException("No se encontro el script para crear las tablas.", _rutaScript);

        var sql = File.ReadAllText(_rutaScript);

        using var conexion = new SqliteConnection(CadenaConexion);
        conexion.Open();

        using var comando = conexion.CreateCommand();
        comando.CommandText = sql;
        comando.ExecuteNonQuery();
    }
}
