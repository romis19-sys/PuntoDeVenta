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
    public class bPresentacion
    {
        private static readonly dPresentacion PresentacionDal = new dPresentacion();

        public static DataTable listarPresentacion()
        {
            try
            {
                return PresentacionDal.listarPresentacion();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al listar registros.");
            }
        }

        public static DataTable buscarPresentacion(string valor)
        {
            try
            {
                return PresentacionDal.buscarPresentacion(valor);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al buscar registros.");
            }
        }

        private static resultadoOperacion validarPresentacion(oPresentacion presentacion)
        {
            if (string.IsNullOrWhiteSpace(presentacion.nombrePresentacion))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el nombre del Presentacion.",
                    campoInvalido = "nombrePresentacion"
                };

            //if (!Validar.soloLetras(presentacion.nombrePresentacion))
            //    return new resultadoOperacion
            //    {
            //        esValido = false,
            //        mensaje = "Ingrese el nombre del Presentacion.",
            //        campoInvalido = "nombrePresentacion"
            //    };

            //if (string.IsNullOrWhiteSpace(Presentacion.descripcionPresentacion))
            //    return new resultadoOperacion
            //    {
            //        esValido = false,
            //        mensaje = "Ingrese la descripción para la Presentacion.",
            //        campoInvalido = "descripcionPresentacion"
            //    };

            return new resultadoOperacion { esValido = true };
        }

        public static resultadoOperacion registrarPresentacion(oPresentacion presentacion)
        {
            var validacion = validarPresentacion(presentacion);
            if (!validacion.esValido)
                return validacion;

            //try
            //{
                bool resultado = PresentacionDal.registrarPresentacion(presentacion);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro almacenado satisfactoriamente."
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido = false,
                        mensaje = "No se puedo crear el registro. Verfique los datos."
                    };
                }
            //}

            //catch (Exception)
            //{
            //    return new resultadoOperacion
            //    {
            //        esValido = false,
            //        mensaje = "Ocurrió un error inesperado al guardar el registro."
            //    };
            //}
        }

        public static resultadoOperacion editarPresentacion(oPresentacion presentacion)
        {
            var validacion = validarPresentacion(presentacion);
            if (!validacion.esValido)
                return validacion;

            try
            {
                bool resultado = PresentacionDal.editarPresentacion(presentacion);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro actualizado satisfactoriamente."
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido = false,
                        mensaje = "No se puedo editar el registro. Verfique los datos."
                    };
                }
            }

            catch (Exception)
            {
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ocurrió un error inesperado al editar el registro."
                };
            }
        }

        public static string eliminarPresentacion(int idPresentacion)
        {
            if (idPresentacion <= 0)
                return "Debe especificar un registro válido";

            try
            {
                bool resultado = PresentacionDal.eliminarPresentacion(idPresentacion);

                if (resultado)
                {
                    return "Registro eliminado con éxito.";
                }
                else
                {
                    return "No se puedo eliminar el registro.";
                }
            }

            catch (Exception)
            {
                throw new ApplicationException("Error inesperado al eliminar el registro.");
            }
        }
    }
}
