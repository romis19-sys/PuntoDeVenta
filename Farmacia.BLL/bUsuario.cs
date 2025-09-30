using Farmacia.DAL;
using Farmacia.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.BLL
{
    public class bUsuario
    {
        private static readonly dUsuarios usuarioDal = new dUsuarios();

        public static DataTable listarUsuarios()
        {
            try
            {
                return usuarioDal.ListarUsuarios();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al listar los resgistros");
            }
        }

        public static DataTable buscarUsuarios(string nombre)
        {
            try
            {
                return usuarioDal.buscarUsuarios(nombre);
            }
            catch (Exception )
            {
                throw new ApplicationException("Error al buscar los resgistros");
            }
        }

        public static DataTable listarRol()
        {
            try
            {
                return usuarioDal.listarRol();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al listar registros.");
            }
        }

        private static resultadoOperacion validarUsuario(OUsuario usuario)
        {

            if (string.IsNullOrWhiteSpace(usuario.Nombreusuario))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese la identificcación del usuario.",
                    campoInvalido = "Usuario"
                };

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el nombre del usuario.",
                    campoInvalido = "nombre"
                };

            if (string.IsNullOrWhiteSpace(usuario.apellido))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el apellido del usuario.",
                    campoInvalido = "apellido"
                };

            if (string.IsNullOrWhiteSpace(usuario.telefono))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el teledono del usuario.",
                    campoInvalido = "telefono"
                };

            if (!Validar.emailValidado(usuario.email))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el email del usuario.",
                    campoInvalido = "email"
                };

            if (!Validar.claveSegura(usuario.clave))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese una contraseña segura",
                    campoInvalido = "clave"
                };
            return new resultadoOperacion { esValido = true };
        }

        public static resultadoOperacion registrarUsuario(OUsuario usuario)
        {
            var validacion = validarUsuario(usuario);
            if (!validacion.esValido)
                return validacion;

            try
            {
                bool resultado = usuarioDal.registrarUsuarios(usuario);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro almacenado satisfactoriamente"
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido =false,
                        mensaje = "No se puedo crear el registro. Verfique los datos"
                    };
                }
            }
            catch (Exception)
            {
                return new resultadoOperacion
                {
                    esValido =false,
                    mensaje = "Ocurrió un error inesperado al guardar el registro"
                };
            }
        }

        public static resultadoOperacion editarUsuario(OUsuario usuario)
        {
            var validacion = validarUsuario(usuario);
            if (!validacion.esValido)
                return validacion;

            try
            {
                bool resultado = usuarioDal.editarUsuarios(usuario);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro editado satisfactoriamente"
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido = false,
                        mensaje = "No se puedo editar el registro. Verfique los datos"
                    };
                }
            }
            catch (Exception)
            {
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ocurrió un error inesperado al editar el registro"
                };
            }
        }

        public static string eliminarUsuario(int idUsuario)
        {

            if (idUsuario <= 0)
                return "Debe espesificar un regustro valido";

            try
            {
                bool resultado = usuarioDal.eliminarUsuarios(idUsuario);
                
                if (resultado)
                {
                    return "Registro eliminado de forma exitosa";
                }
                else
                {
                    return "No se pudo eliminar el registro";
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Error inesperado al eliminar el registro.");
            }
            
        }

    }
}
