using Microsoft.Data.Sqlite;
using VeterinariaControlClinico.Database;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.DataAccess
{
    /// <summary>
    /// Operaciones CRUD para la tabla Mascotas.
    /// </summary>
    public class MascotaDAO
    {
        public int Crear(Mascota mascota)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                INSERT INTO Mascotas (DuenoId, Nombre, Especie, Raza, FechaNacimiento, Sexo, Peso)
                VALUES ($duenoId, $nombre, $especie, $raza, $fechaNacimiento, $sexo, $peso);
                SELECT last_insert_rowid();";

            AgregarParametros(comando, mascota);

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        // Trae todas las mascotas junto con el nombre de su dueño (JOIN).
        public List<Mascota> ObtenerTodos()
        {
            var lista = new List<Mascota>();

            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                SELECT m.MascotaId, m.DuenoId, m.Nombre, m.Especie, m.Raza,
                       m.FechaNacimiento, m.Sexo, m.Peso, d.Nombre AS NombreDueno
                FROM Mascotas m
                INNER JOIN Duenos d ON d.DuenoId = m.DuenoId
                ORDER BY m.Nombre;";

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(MapearMascota(lector));
            }

            return lista;
        }

        public Mascota? ObtenerPorId(int mascotaId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                SELECT m.MascotaId, m.DuenoId, m.Nombre, m.Especie, m.Raza,
                       m.FechaNacimiento, m.Sexo, m.Peso, d.Nombre AS NombreDueno
                FROM Mascotas m
                INNER JOIN Duenos d ON d.DuenoId = m.DuenoId
                WHERE m.MascotaId = $id;";
            comando.Parameters.AddWithValue("$id", mascotaId);

            using var lector = comando.ExecuteReader();
            return lector.Read() ? MapearMascota(lector) : null;
        }

        public bool Actualizar(Mascota mascota)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                UPDATE Mascotas
                SET DuenoId = $duenoId, Nombre = $nombre, Especie = $especie,
                    Raza = $raza, FechaNacimiento = $fechaNacimiento,
                    Sexo = $sexo, Peso = $peso
                WHERE MascotaId = $id;";

            AgregarParametros(comando, mascota);
            comando.Parameters.AddWithValue("$id", mascota.MascotaId);

            return comando.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int mascotaId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Mascotas WHERE MascotaId = $id;";
            comando.Parameters.AddWithValue("$id", mascotaId);

            return comando.ExecuteNonQuery() > 0;
        }

        private static void AgregarParametros(SqliteCommand comando, Mascota mascota)
        {
            comando.Parameters.AddWithValue("$duenoId", mascota.DuenoId);
            comando.Parameters.AddWithValue("$nombre", mascota.Nombre);
            comando.Parameters.AddWithValue("$especie", (object?)mascota.Especie ?? DBNull.Value);
            comando.Parameters.AddWithValue("$raza", (object?)mascota.Raza ?? DBNull.Value);
            comando.Parameters.AddWithValue("$fechaNacimiento",
                mascota.FechaNacimiento.HasValue ? mascota.FechaNacimiento.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
            comando.Parameters.AddWithValue("$sexo", (object?)mascota.Sexo ?? DBNull.Value);
            comando.Parameters.AddWithValue("$peso", (object?)mascota.Peso ?? DBNull.Value);
        }

        private static Mascota MapearMascota(SqliteDataReader lector)
        {
            return new Mascota
            {
                MascotaId = lector.GetInt32(0),
                DuenoId = lector.GetInt32(1),
                Nombre = lector.GetString(2),
                Especie = lector.IsDBNull(3) ? null : lector.GetString(3),
                Raza = lector.IsDBNull(4) ? null : lector.GetString(4),
                FechaNacimiento = lector.IsDBNull(5) ? null : DateTime.Parse(lector.GetString(5)),
                Sexo = lector.IsDBNull(6) ? null : lector.GetString(6),
                Peso = lector.IsDBNull(7) ? null : lector.GetDouble(7),
                NombreDueno = lector.GetString(8),
            };
        }
    }
}
