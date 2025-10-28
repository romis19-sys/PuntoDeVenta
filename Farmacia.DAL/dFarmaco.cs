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
    public class dFarmaco
    {
        public DataTable listarTodosFarmacos()
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Listar_Todos_Farmacos", cn))
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

        public DataTable listarFarmacos()
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_farmaco_Listar", cn))
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

        //Este lo use para buscar farmacos que son mas campos
        public DataTable buscarTodosFarmacos(int filtro, string valor)
        {
            DataTable lista = new DataTable();

            //try
            //{
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Buscar_Todos_Farmaco", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Filtro", filtro);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        lista.Load(dr);
                    }
                }
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al buscar los registros.");
            //}

            return lista;
        }

        // Esto se ocupo para buscar farmacos con menos campos se uso para la compra
        public DataTable buscarFarmacos(int filtro, string valor)
        {
            DataTable lista = new DataTable();

            //try
            //{
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Farmaco_Buscar_Compra", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Filtro", filtro);
                    cmd.Parameters.AddWithValue("@Valor", valor);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        lista.Load(dr);
                    }
                }
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al buscar los registros.");
            //}

            return lista;
        }
        //este es para la venta
        public DataTable buscarFarmacosVenta(int filtro, string valor)
        {
            DataTable lista = new DataTable();

            //try
            //{
            using (SqlConnection cn = GestorConexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Farmaco_Buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Filtro", filtro);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    lista.Load(dr);
                }
            }
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al buscar los registros.");
            //}

            return lista;
        }

        public bool registrarFarmaco(oFarmaco farmaco)
        {
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Farmaco_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Codigo", farmaco.codigo);
                    cmd.Parameters.AddWithValue("@Farmaco", farmaco.nombreFarmaco);
                    cmd.Parameters.AddWithValue("@IdLaboratorio", farmaco.idLaboratorio);
                    cmd.Parameters.AddWithValue("@Stock", farmaco.stock);
                    cmd.Parameters.AddWithValue("@Descripcion", farmaco.descripcion);
                    cmd.Parameters.AddWithValue("@IdPresentacion", farmaco.idPresentacion);
                    cmd.Parameters.AddWithValue("@TipoVenta", farmaco.tipoVenta);
                    cmd.Parameters.AddWithValue("@PrecioCompra", farmaco.precioCompra);
                    cmd.Parameters.AddWithValue("@PrecioVenta", farmaco.precioVenta);

                    if (farmaco.fechaCaducacion.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@FechaCaducacion", farmaco.fechaCaducacion);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@FechaCaducacion", DBNull.Value);
                    }


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
                throw new ApplicationException("Error al registrar el fármaco.");
            }
        }

        public bool editarFarmaco(oFarmaco farmaco)
        {
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Farmaco_Actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdFarmaco", farmaco.idFarmaco);
                    cmd.Parameters.AddWithValue("@Codigo", farmaco.codigo);
                    cmd.Parameters.AddWithValue("@Farmaco", farmaco.nombreFarmaco);
                    cmd.Parameters.AddWithValue("@IdLaboratorio", farmaco.idLaboratorio);
                    cmd.Parameters.AddWithValue("@Stock", farmaco.stock);
                    cmd.Parameters.AddWithValue("@Descripcion", farmaco.descripcion);
                    cmd.Parameters.AddWithValue("@IdPresentacion", farmaco.idPresentacion);
                    cmd.Parameters.AddWithValue("@TipoVenta", farmaco.tipoVenta);
                    cmd.Parameters.AddWithValue("@PrecioCompra", farmaco.precioCompra);
                    cmd.Parameters.AddWithValue("@PrecioVenta", farmaco.precioVenta);

                    if (farmaco.fechaCaducacion.HasValue)
                        cmd.Parameters.AddWithValue("@FechaCaducacion", farmaco.fechaCaducacion);
                    else
                        cmd.Parameters.AddWithValue("@FechaCaducacion", DBNull.Value);

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
                throw new ApplicationException("Error al actualizar el fármaco.");
            }
        }

        public bool eliminarFarmaco(int idFarmaco)
        {
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Farmaco_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFarmaco", idFarmaco);

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
                throw new ApplicationException("Error al eliminar el registro.");
            }
        }
    }
}
