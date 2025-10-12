using Farmacia.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.DAL
{
    public class dLaboratorio
    {
        // listar los laboratorios existentes
        public DataTable listarLaboratorio()
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_laboratorio_Listar", cn))
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

        // buscar los laboratorios
        public DataTable buscarLaboratorio(string laboratorio)
        // en el video del profesor sale que puso valor pero nosotros tenemos laboratorio y para que vaya con
        // logica le puse el mismo nombre que tenia en el sp
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_laboratorio_buscar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@laboratorio", laboratorio);
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

        // agregar un nuevo laboratorio
        public bool registrarLaboratorio(oLaboratorio laboratorios)
        {
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_laboratorio_crear", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@laboratorio", laboratorios.laboratorio);
                    cmd.Parameters.AddWithValue("@telefono", laboratorios.telefono);
                    cmd.Parameters.AddWithValue("@contacto", laboratorios.contacto);

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

        // editar el laboratorio
        public bool editarLaboratorio(oLaboratorio laboratorios)
        {
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_laboratorio_actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdLaboratorio", laboratorios.idLaboratorio);
                    cmd.Parameters.AddWithValue("@laboratorio", laboratorios.laboratorio);
                    cmd.Parameters.AddWithValue("@telefono", laboratorios.telefono);
                    cmd.Parameters.AddWithValue("@contacto", laboratorios.contacto);

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
                throw new ApplicationException("Error al editar el registro.");
            }
        }

        // eliminar el laboratorio
        public bool eliminarLaboratorio(int idLaboratorio)
        {
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_laboratorio_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdLaboratorio", idLaboratorio);

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
