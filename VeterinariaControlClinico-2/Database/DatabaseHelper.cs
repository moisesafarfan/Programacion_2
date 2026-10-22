using Microsoft.Data.Sqlite;

namespace VeterinariaControlClinico.Database
{
    /// <summary>
    /// Clase encargada de centralizar el acceso al motor de base de datos
    /// SQLite: construye la cadena de conexión y crea el esquema (tablas)
    /// si todavía no existe.
    /// </summary>
    public static class DatabaseHelper
    {
        // El archivo .db se crea junto al ejecutable de la aplicación.
        private static readonly string RutaBaseDeDatos =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VeterinariaControlClinico.db");

        private static readonly string CadenaConexion =
            $"Data Source={RutaBaseDeDatos}";

        /// <summary>
        /// Devuelve una nueva conexión abierta hacia la base de datos SQLite.
        /// El llamador es responsable de hacer "using" para cerrarla.
        /// </summary>
        public static SqliteConnection ObtenerConexion()
        {
            var conexion = new SqliteConnection(CadenaConexion);
            conexion.Open();

            // Activa las llaves foráneas en cada conexión (SQLite las trae
            // desactivadas por defecto).
            using var comandoPragma = conexion.CreateCommand();
            comandoPragma.CommandText = "PRAGMA foreign_keys = ON;";
            comandoPragma.ExecuteNonQuery();

            return conexion;
        }

        /// <summary>
        /// Crea el esquema completo del sistema (tablas) si no existe.
        /// Se debe llamar una sola vez, al iniciar la aplicación.
        /// </summary>
        public static void InicializarBaseDeDatos()
        {
            using var conexion = ObtenerConexion();
            using var comando = conexion.CreateCommand();

            comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS Duenos (
                    DuenoId     INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre      TEXT    NOT NULL,
                    Telefono    TEXT,
                    Direccion   TEXT,
                    Email       TEXT
                );

                CREATE TABLE IF NOT EXISTS Veterinarios (
                    VeterinarioId  INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre         TEXT    NOT NULL,
                    Especialidad   TEXT,
                    Telefono       TEXT
                );

                CREATE TABLE IF NOT EXISTS Mascotas (
                    MascotaId       INTEGER PRIMARY KEY AUTOINCREMENT,
                    DuenoId         INTEGER NOT NULL,
                    Nombre          TEXT    NOT NULL,
                    Especie         TEXT,
                    Raza            TEXT,
                    FechaNacimiento TEXT,
                    Sexo            TEXT,
                    Peso            REAL,
                    FOREIGN KEY (DuenoId) REFERENCES Duenos (DuenoId)
                        ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Citas (
                    CitaId         INTEGER PRIMARY KEY AUTOINCREMENT,
                    MascotaId      INTEGER NOT NULL,
                    VeterinarioId  INTEGER NOT NULL,
                    FechaHora      TEXT    NOT NULL,
                    Motivo         TEXT,
                    Estado         TEXT    NOT NULL DEFAULT 'Programada',
                    FOREIGN KEY (MascotaId) REFERENCES Mascotas (MascotaId)
                        ON DELETE CASCADE,
                    FOREIGN KEY (VeterinarioId) REFERENCES Veterinarios (VeterinarioId)
                        ON DELETE RESTRICT
                );

                CREATE TABLE IF NOT EXISTS HistorialClinico (
                    HistorialId    INTEGER PRIMARY KEY AUTOINCREMENT,
                    MascotaId      INTEGER NOT NULL,
                    VeterinarioId  INTEGER NOT NULL,
                    Fecha          TEXT    NOT NULL,
                    Diagnostico    TEXT,
                    Tratamiento    TEXT,
                    Observaciones  TEXT,
                    FOREIGN KEY (MascotaId) REFERENCES Mascotas (MascotaId)
                        ON DELETE CASCADE,
                    FOREIGN KEY (VeterinarioId) REFERENCES Veterinarios (VeterinarioId)
                        ON DELETE RESTRICT
                );
            ";

            comando.ExecuteNonQuery();
        }
    }
}
