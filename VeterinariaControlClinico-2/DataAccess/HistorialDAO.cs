using Microsoft.Data.Sqlite;
using VeterinariaControlClinico.Database;
using VeterinariaControlClinico.Models;

namespace VeterinariaControlClinico.DataAccess
{
    /// <summary>
    /// Operaciones CRUD para la tabla HistorialClinico.
    /// </summary>
    public class HistorialDAO
    {
        public int Crear(HistorialClinico historial)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                INSERT INTO HistorialClinico (MascotaId, VeterinarioId, Fecha, Diagnostico, Tratamiento, Observaciones)
                VALUES ($mascotaId, $veterinarioId, $fecha, $diagnostico, $tratamiento, $observaciones);
                SELECT last_insert_rowid();";

            AgregarParametros(comando, historial);

            return Convert.ToInt32(comando.ExecuteScalar());
        }

        public List<HistorialClinico> ObtenerTodos()
        {
            var lista = new List<HistorialClinico>();

            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                SELECT h.HistorialId, h.MascotaId, h.VeterinarioId, h.Fecha,
                       h.Diagnostico, h.Tratamiento, h.Observaciones,
                       m.Nombre AS NombreMascota, v.Nombre AS NombreVeterinario
                FROM HistorialClinico h
                INNER JOIN Mascotas m ON m.MascotaId = h.MascotaId
                INNER JOIN Veterinarios v ON v.VeterinarioId = h.VeterinarioId
                ORDER BY h.Fecha DESC;";

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(MapearHistorial(lector));
            }

            return lista;
        }

        // Historial filtrado por una mascota específica (útil para la ficha clínica).
        public List<HistorialClinico> ObtenerPorMascota(int mascotaId)
        {
            var lista = new List<HistorialClinico>();

            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                SELECT h.HistorialId, h.MascotaId, h.VeterinarioId, h.Fecha,
                       h.Diagnostico, h.Tratamiento, h.Observaciones,
                       m.Nombre AS NombreMascota, v.Nombre AS NombreVeterinario
                FROM HistorialClinico h
                INNER JOIN Mascotas m ON m.MascotaId = h.MascotaId
                INNER JOIN Veterinarios v ON v.VeterinarioId = h.VeterinarioId
                WHERE h.MascotaId = $mascotaId
                ORDER BY h.Fecha DESC;";
            comando.Parameters.AddWithValue("$mascotaId", mascotaId);

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(MapearHistorial(lector));
            }

            return lista;
        }

        public HistorialClinico? ObtenerPorId(int historialId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                SELECT h.HistorialId, h.MascotaId, h.VeterinarioId, h.Fecha,
                       h.Diagnostico, h.Tratamiento, h.Observaciones,
                       m.Nombre AS NombreMascota, v.Nombre AS NombreVeterinario
                FROM HistorialClinico h
                INNER JOIN Mascotas m ON m.MascotaId = h.MascotaId
                INNER JOIN Veterinarios v ON v.VeterinarioId = h.VeterinarioId
                WHERE h.HistorialId = $id;";
            comando.Parameters.AddWithValue("$id", historialId);

            using var lector = comando.ExecuteReader();
            return lector.Read() ? MapearHistorial(lector) : null;
        }

        public bool Actualizar(HistorialClinico historial)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                UPDATE HistorialClinico
                SET MascotaId = $mascotaId, VeterinarioId = $veterinarioId, Fecha = $fecha,
                    Diagnostico = $diagnostico, Tratamiento = $tratamiento, Observaciones = $observaciones
                WHERE HistorialId = $id;";

            AgregarParametros(comando, historial);
            comando.Parameters.AddWithValue("$id", historial.HistorialId);

            return comando.ExecuteNonQuery() > 0;
        }

        public bool Eliminar(int historialId)
        {
            using var conexion = DatabaseHelper.ObtenerConexion();
            using var comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM HistorialClinico WHERE HistorialId = $id;";
            comando.Parameters.AddWithValue("$id", historialId);

            return comando.ExecuteNonQuery() > 0;
        }

        private static void AgregarParametros(SqliteCommand comando, HistorialClinico historial)
        {
            comando.Parameters.AddWithValue("$mascotaId", historial.MascotaId);
            comando.Parameters.AddWithValue("$veterinarioId", historial.VeterinarioId);
            comando.Parameters.AddWithValue("$fecha", historial.Fecha.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("$diagnostico", (object?)historial.Diagnostico ?? DBNull.Value);
            comando.Parameters.AddWithValue("$tratamiento", (object?)historial.Tratamiento ?? DBNull.Value);
            comando.Parameters.AddWithValue("$observaciones", (object?)historial.Observaciones ?? DBNull.Value);
        }

        private static HistorialClinico MapearHistorial(SqliteDataReader lector)
        {
            return new HistorialClinico
            {
                HistorialId = lector.GetInt32(0),
                MascotaId = lector.GetInt32(1),
                VeterinarioId = lector.GetInt32(2),
                Fecha = DateTime.Parse(lector.GetString(3)),
                Diagnostico = lector.IsDBNull(4) ? null : lector.GetString(4),
                Tratamiento = lector.IsDBNull(5) ? null : lector.GetString(5),
                Observaciones = lector.IsDBNull(6) ? null : lector.GetString(6),
                NombreMascota = lector.GetString(7),
                NombreVeterinario = lector.GetString(8),
            };
        }
    }
}
