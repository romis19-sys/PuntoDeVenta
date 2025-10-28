using Farmacia.DAL;
using Sistema.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DAL
{
    public class dCaja
    {

        public DataTable listarCaja()
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Listar_Caja", cn))
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
                throw new ApplicationException("Error al cargar los registros.");
            }

            return lista;
        }

       

        public DataTable buscarCajaPorFecja(DateTime fechInicio, DateTime fechaFinal)
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Buscar_Caja_por_Fecha", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", fechInicio);
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
        public DataTable buscarCaja(int idUsuario)
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Buscar_Caja", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
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


        public bool abrirCaja(oCaja Caja, out int idGenerado)
        {
            idGenerado = 0;

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Abrir_Caja", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", Caja.idUsuario);
                    cmd.Parameters.AddWithValue("@NumeroCaja", Caja.numeroCaja);
                    cmd.Parameters.AddWithValue("@MontoApertura", Caja.montoApertura);

                    SqlParameter idCaja = new SqlParameter("@IdCaja", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idCaja);

                    SqlParameter respuesta = new SqlParameter("@Respuesta", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(respuesta);

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    int resultado = Convert.ToInt32(respuesta.Value);
                    idGenerado = Convert.ToInt32(idCaja.Value);

                    return resultado == 1;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al abrir la caja.");
            }
        }

        public DataTable SumarTotales(int idUsuario)
        {
            DataTable lista = new DataTable();

            //try
            //{
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Sumar_Totales", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        lista.Load(dr);
                    }
                }
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al sumar totales.");
            //}

            return lista;
        }

        public bool CerrarCaja(oCaja Caja, out string supervisor)
        {

            //try
            //{
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Cerrar_Caja", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCaja", Caja.idCaja);
                    cmd.Parameters.AddWithValue("@Efectivo", Caja.totalEfectivo);
                    cmd.Parameters.AddWithValue("@Tarjeta", Caja.totalTarjeta);
                    cmd.Parameters.AddWithValue("@Cambio", Caja.totalCambio);
                    cmd.Parameters.AddWithValue("@Entregado", Caja.totalEntregado);
                    cmd.Parameters.AddWithValue("@Codigo", Caja.codigo);
                    cmd.Parameters.AddWithValue("@Observacion", Caja.observacion);

                    SqlParameter supervisorDevuelto = new SqlParameter("@Supervisor", SqlDbType.NVarChar, 100)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(supervisorDevuelto);

                    SqlParameter respuesta = new SqlParameter("@Respuesta", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(respuesta);

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    int resultado = Convert.ToInt32(respuesta.Value);
                    supervisor = supervisorDevuelto.Value.ToString();

                    return resultado == 1;
                }
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al cerrar la caja.");
            //}
        }
    }
}
