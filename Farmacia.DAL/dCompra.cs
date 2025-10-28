using Farmacia.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public class dCompra
    {
        public DataTable CargarProveedores()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = GestorConexion.ObtenerConexion();
                SqlCommand Comando = new SqlCommand("sp_laboratorio_Listar", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                SqlCon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
            }
        }
        // lista
        public DataTable listarCompras()
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Listar_Compras", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        lista.Load(dr);
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al listar los registros.");
            }
            return lista;
        }
        // busca
        public DataTable buscarCompra(DateTime fechaInicio, DateTime fechaFinal)
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Buscar_Compras", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFinal", fechaFinal);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        lista.Load(dr);
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al buscar los registros.");
            }

            return lista;
        }
        // guarda
        public bool registrarVentas(oCompra compras, out string compraGenerada)
        {
            compraGenerada = string.Empty;
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Registrar_Compra", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdLaboratorio", compras.idLaboratorio);
                    cmd.Parameters.AddWithValue("@IdUsuario", compras.idUsuario);
                    cmd.Parameters.AddWithValue("@SubTotal", compras.subtotal);
                    cmd.Parameters.AddWithValue("@Impuesto", compras.impuesto);
                    cmd.Parameters.AddWithValue("@TotalGeneral", compras.totalGeneral);
                    cmd.Parameters.AddWithValue("@NCompra", compras.NCompra);
                    cmd.Parameters.AddWithValue("@MontoEfectivo", compras.montoEfectivo);
                    cmd.Parameters.AddWithValue("@MontoTarjeta", compras.montoTarjeta);
                    cmd.Parameters.AddWithValue("@Cambio", compras.cambio);
                    cmd.Parameters.AddWithValue("@Detalles", compras.detalles);

                    SqlParameter respuesta = new SqlParameter("@Respuesta", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(respuesta);

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    int resultado = Convert.ToInt32(respuesta.Value);
                    return resultado == 1;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al agregar el registro.");
            }
        }
    }
}
