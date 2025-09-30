using Farmacia.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public static class GestorConexion
    {
        private static readonly string carpeta =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "POS");

        private static readonly string archivo = Path.Combine(carpeta, "conexion.json");

        /// <summary>
        /// Guardar la conficuración de conexión en un archivo JSON
        /// </summary>
        /// 

        public static void GuardarConexion(DatosConexion datosConexion)
        {
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Convertir el objeto datosConexion en una cadena de texto => JSON
            var json = JsonSerializer.Serialize(datosConexion, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(archivo, json);
        }

        /// <summary>
        /// Cargar la configuraciín desde el archivo JSON
        /// </summary>
        /// 

        public static DatosConexion CargarDatosConexion()
        {
            if (!File.Exists(archivo))
                return new DatosConexion();

            var json = File.ReadAllText(archivo);
            return JsonSerializer.Deserialize<DatosConexion>(json);
        }

        /// <summary>
        /// Devolver la cadena de conexión lista para usar desde nuestros métodos
        /// </summary>
        /// 

        public static SqlConnection ObtenerConexion()
        {
            var datosConexion = CargarDatosConexion();

            if (string.IsNullOrWhiteSpace(datosConexion?.servidor) ||
               string.IsNullOrWhiteSpace(datosConexion?.baseDatos) ||
               string.IsNullOrWhiteSpace(datosConexion?.usuario) ||
               string.IsNullOrWhiteSpace(datosConexion?.clave))
            {
                throw new InvalidOperationException("La conexión al servidor no está configurada.");
            }

            return new SqlConnection(datosConexion.CadenaConexion);
        }

        /// <summary>
        /// Probrar la cadena de conexión con los datos especificados
        /// </summary>
        /// 

        public static Boolean ProbarConexion(DatosConexion datosConexion, out string error)
        {
            try
            {
                using (var cn = new SqlConnection(datosConexion.CadenaConexion))
                {
                    cn.Open();
                    error = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
