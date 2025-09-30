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
    public class dUsuarios
    {
        public DataTable ListarUsuarios()
        {
            DataTable lista = new DataTable();
            
            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Listar_Usuarios", cn))
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
                throw new ApplicationException("Error al cargar el formulario");
            }
        
            return lista;
        }

        public DataTable buscarUsuarios(string nombre)
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_BuscarUsuarios", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        lista.Load(dr);
                    }
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al Buscar un registro");
            }

            return lista;
        }

        public DataTable listarRol()
        {
            DataTable lista = new DataTable();

            try
            {
                using (SqlConnection cn = GestorConexion.ObtenerConexion())
                using (SqlCommand cmd = new SqlCommand("sp_Listar_rol", cn))
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

        public bool registrarUsuarios(OUsuario usuario)
        {
            using(SqlConnection cn = GestorConexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuarios_Insertar", cn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdRol", usuario.idRol);
                    cmd.Parameters.AddWithValue("@Usuario", usuario.Nombreusuario);
                    cmd.Parameters.AddWithValue("@Nombres", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", usuario.apellido);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.telefono);
                    cmd.Parameters.AddWithValue("@Email", usuario.email);
                    cmd.Parameters.AddWithValue("@Clave", usuario.clave);

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
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
                
            }
        }

        public bool editarUsuarios(OUsuario usuario)
        {
            using (SqlConnection cn = GestorConexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuarios_Actualizar", cn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.idUsuario);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.idRol);
                    cmd.Parameters.AddWithValue("@Usuario", usuario.Nombreusuario);  
                    cmd.Parameters.AddWithValue("@Nombres", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", usuario.apellido);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.telefono);
                    cmd.Parameters.AddWithValue("@Email", usuario.email);
                    cmd.Parameters.AddWithValue("@Clave", usuario.clave);

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
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }

            }
        }

        public bool eliminarUsuarios(int idUsuario)
        {
            using (SqlConnection cn = GestorConexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_Usuarios_eliminar", cn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

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
                catch (Exception )
                {
                    throw new ApplicationException("Error al Eliminar un registro.");
                }

            }
        }


    }
}
