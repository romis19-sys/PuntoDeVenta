using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.Entity;

namespace Farmacia.DAL
{
    public class dVentas
    {
        public DataTable listarVentas()
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Listar_Ventas", cn))
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

        public DataTable buscarVenta(DateTime fechaInicio, DateTime fechaFinal)
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Buscar_Ventas", cn))
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

        public bool registrarVentas(oVentas ventas, out string VentaGenerado)
        {
            VentaGenerado = string.Empty;
            try
            {


                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Registrar_Venta", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", ventas.idUsuario);
                    cmd.Parameters.AddWithValue("@Cliente", ventas.cliente);
                    //cmd.Parameters.AddWithValue("@NVenta", ventas.NVenta);
                    cmd.Parameters.AddWithValue("@SubTotal", ventas.subTotal);
                    cmd.Parameters.AddWithValue("@TotalGeneral", ventas.totalGeneral);
                    cmd.Parameters.AddWithValue("@MontoEfectivo", ventas.montoEfectivo);
                    cmd.Parameters.AddWithValue("@MontoTarjeta", ventas.montoTargeta);
                    cmd.Parameters.AddWithValue("@Canbio", ventas.cambio);
                    cmd.Parameters.AddWithValue("@Detalles", ventas.detalles);

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
