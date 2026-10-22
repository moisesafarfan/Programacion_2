using Microsoft.Data.Sqlite;
using VeterinariaControlClinico.Database;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.DataAccess
{
    /// <summary>
    /// Operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para la tabla Duenos.
    /// </summary>
    public class DuenoDAO
    {
        // ---------- CREATE ----------
        public int Crear(Dueno dueno)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                INSERT INTO Duenos (Nombre, Telefono, Direccion, Email)
                VALUES ($nombre, $telefono, $direccion, $email);
                SELECT last_insert_rowid();";

            comando.Parameters.AddWithValue("$nombre", dueno.Nombre);
            comando.Parameters.AddWithValue("$telefono", (object?)dueno.Telefono ?? DBNull.Value);
            comando.Parameters.AddWithValue("$direccion", (object?)dueno.Direccion ?? DBNull.Value);
            comando.Parameters.AddWithValue("$email", (object?)dueno.Email ?? DBNull.Value);

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        // ---------- READ (todos) ----------
        public List<Dueno> ObtenerTodos()
        {
            var lista = new List<Dueno>();

            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "SELECT DuenoId, Nombre, Telefono, Direccion, Email FROM Duenos ORDER BY Nombre;";

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(MapearDueno(lector));
            }

            return lista;
        }

        // ---------- READ (uno por id) ----------
        public Dueno? ObtenerPorId(int duenoId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "SELECT DuenoId, Nombre, Telefono, Direccion, Email FROM Duenos WHERE DuenoId = $id;";
            comando.Parameters.AddWithValue("$id", duenoId);

            using var lector = comando.ExecuteReader();
            return lector.Read() ? MapearDueno(lector) : null;
        }

        // ---------- UPDATE ----------
        public bool Actualizar(Dueno dueno)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                UPDATE Duenos
                SET Nombre = $nombre, Telefono = $telefono,
                    Direccion = $direccion, Email = $email
                WHERE DuenoId = $id;";

            comando.Parameters.AddWithValue("$nombre", dueno.Nombre);
            comando.Parameters.AddWithValue("$telefono", (object?)dueno.Telefono ?? DBNull.Value);
            comando.Parameters.AddWithValue("$direccion", (object?)dueno.Direccion ?? DBNull.Value);
            comando.Parameters.AddWithValue("$email", (object?)dueno.Email ?? DBNull.Value);
            comando.Parameters.AddWithValue("$id", dueno.DuenoId);

            return comando.ExecuteNonQuery() > 0;
        }

        // ---------- DELETE ----------
        public bool Eliminar(int duenoId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Duenos WHERE DuenoId = $id;";
            comando.Parameters.AddWithValue("$id", duenoId);

            return comando.ExecuteNonQuery() > 0;
        }

        private static Dueno MapearDueno(SqliteDataReader lector)
        {
            return new Dueno
            {
                DuenoId = lector.GetInt32(0),
                Nombre = lector.GetString(1),
                Telefono = lector.IsDBNull(2) ? null : lector.GetString(2),
                Direccion = lector.IsDBNull(3) ? null : lector.GetString(3),
                Email = lector.IsDBNull(4) ? null : lector.GetString(4),
            };
        }
    }
}
