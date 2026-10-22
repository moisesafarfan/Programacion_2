using Microsoft.Data.Sqlite;
using VeterinariaControlClinico.Database;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.DataAccess
{
    /// <summary>
    /// Operaciones CRUD para la tabla Citas.
    /// </summary>
    public class CitaDAO
    {
        public int Crear(Cita cita)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                INSERT INTO Citas (MascotaId, VeterinarioId, FechaHora, Motivo, Estado)
                VALUES ($mascotaId, $veterinarioId, $fechaHora, $motivo, $estado);
                SELECT last_insert_rowid();";

            AgregarParametros(comando, cita);

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public List<Cita> ObtenerTodos()
        {
            var lista = new List<Cita>();

            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                SELECT c.CitaId, c.MascotaId, c.VeterinarioId, c.FechaHora, c.Motivo, c.Estado,
                       m.Nombre AS NombreMascota, v.Nombre AS NombreVeterinario
                FROM Citas c
                INNER JOIN Mascotas m ON m.MascotaId = c.MascotaId
                INNER JOIN Veterinarios v ON v.VeterinarioId = c.VeterinarioId
                ORDER BY c.FechaHora;";

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(MapearCita(lector));
            }

            return lista;
        }

        public Cita? ObtenerPorId(int citaId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                SELECT c.CitaId, c.MascotaId, c.VeterinarioId, c.FechaHora, c.Motivo, c.Estado,
                       m.Nombre AS NombreMascota, v.Nombre AS NombreVeterinario
                FROM Citas c
                INNER JOIN Mascotas m ON m.MascotaId = c.MascotaId
                INNER JOIN Veterinarios v ON v.VeterinarioId = c.VeterinarioId
                WHERE c.CitaId = $id;";
            comando.Parameters.AddWithValue("$id", citaId);

            using var lector = comando.ExecuteReader();
            return lector.Read() ? MapearCita(lector) : null;
        }

        public bool Actualizar(Cita cita)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                UPDATE Citas
                SET MascotaId = $mascotaId, VeterinarioId = $veterinarioId,
                    FechaHora = $fechaHora, Motivo = $motivo, Estado = $estado
                WHERE CitaId = $id;";

            AgregarParametros(comando, cita);
            comando.Parameters.AddWithValue("$id", cita.CitaId);

            return comando.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int citaId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM Citas WHERE CitaId = $id;";
            comando.Parameters.AddWithValue("$id", citaId);

            return comando.ExecuteNonQuery() > 0;
        }

        private static void AgregarParametros(SqliteCommand comando, Cita cita)
        {
            comando.Parameters.AddWithValue("$mascotaId", cita.MascotaId);
            comando.Parameters.AddWithValue("$veterinarioId", cita.VeterinarioId);
            comando.Parameters.AddWithValue("$fechaHora", cita.FechaHora.ToString("yyyy-MM-dd HH:mm"));
            comando.Parameters.AddWithValue("$motivo", (object?)cita.Motivo ?? DBNull.Value);
            comando.Parameters.AddWithValue("$estado", cita.Estado);
        }

        private static Cita MapearCita(SqliteDataReader lector)
        {
            return new Cita
            {
                CitaId = lector.GetInt32(0),
                MascotaId = lector.GetInt32(1),
                VeterinarioId = lector.GetInt32(2),
                FechaHora = DateTime.Parse(lector.GetString(3)),
                Motivo = lector.IsDBNull(4) ? null : lector.GetString(4),
                Estado = lector.GetString(5),
                NombreMascota = lector.GetString(6),
                NombreVeterinario = lector.GetString(7),
            };
        }
    }
}
