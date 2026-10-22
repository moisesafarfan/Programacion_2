using Microsoft.Data.Sqlite;
using VeterinariaControlClinico.Database;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.DataAccess
{
    /// <summary>
    /// Operaciones CRUD para la tabla Veterinarios.
    /// </summary>
    public class VeterinarioDAO
    {
        public int Crear(Veterinario veterinario)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                INSERT INTO Veterinarios (Nombre, Especialidad, Telefono)
                VALUES ($nombre, $especialidad, $telefono);
                SELECT last_insert_rowid();";

            comando.Parameters.AddWithValue("$nombre", veterinario.Nombre);
            comando.Parameters.AddWithValue("$especialidad", (object?)veterinario.Especialidad ?? DBNull.Value);
            comando.Parameters.AddWithValue("$telefono", (object?)veterinario.Telefono ?? DBNull.Value);

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public List<Veterinario> ObtenerTodos()
        {
            var lista = new List<Veterinario>();

            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "SELECT VeterinarioId, Nombre, Especialidad, Telefono FROM Veterinarios ORDER BY Nombre;";

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(MapearVeterinario(lector));
            }

            return lista;
        }

        public Veterinario? ObtenerPorId(int veterinarioId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "SELECT VeterinarioId, Nombre, Especialidad, Telefono FROM Veterinarios WHERE VeterinarioId = $id;";
            comando.Parameters.AddWithValue("$id", veterinarioId);

            using var lector = comando.ExecuteReader();
            return lector.Read() ? MapearVeterinario(lector) : null;
        }

        public bool Actualizar(Veterinario veterinario)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                UPDATE Veterinarios
                SET Nombre = $nombre, Especialidad = $especialidad, Telefono = $telefono
                WHERE VeterinarioId = $id;";

            comando.Parameters.AddWithValue("$nombre", veterinario.Nombre);
            comando.Parameters.AddWithValue("$especialidad", (object?)veterinario.Especialidad ?? DBNull.Value);
            comando.Parameters.AddWithValue("$telefono", (object?)veterinario.Telefono ?? DBNull.Value);
            comando.Parameters.AddWithValue("$id", veterinario.VeterinarioId);

            return comando.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int veterinarioId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Veterinarios WHERE VeterinarioId = $id;";
            comando.Parameters.AddWithValue("$id", veterinarioId);

            return comando.ExecuteNonQuery() > 0;
        }

        private static Veterinario MapearVeterinario(SqliteDataReader lector)
        {
            return new Veterinario
            {
                VeterinarioId = lector.GetInt32(0),
                Nombre = lector.GetString(1),
                Especialidad = lector.IsDBNull(2) ? null : lector.GetString(2),
                Telefono = lector.IsDBNull(3) ? null : lector.GetString(3),
            };
        }
    }
}
