using Farmacia.Entity;
using Sistema.DAL;
using Sistema.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.BLL
{
    public class bCaja
    {
        private static readonly dCaja cajaDal = new dCaja();


        public static DataTable ListarCaja()
        {
            try
            {
                return cajaDal.listarCaja();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al cargar registros.");
            }
        }

  

        public static DataTable buscarCajaPorFecha(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                return cajaDal.buscarCajaPorFecja(fechaInicio, fechaFinal);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al buscar registros.");
            }
        }

        public static DataTable buscarCaja(int idUsuario)
        {
            try
            {
                return cajaDal.buscarCaja(idUsuario);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al buscar registros.");
            }
        }

        private static resultadoOperacion validarAbrirCaja(oCaja Caja)
        {
            if (Caja.idUsuario == 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "No se ha especificado el usuario.",
                    campoInvalido = "idUsuario"
                };

            if (Caja.numeroCaja == 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Es necesario especificar el número de la caja.",
                    campoInvalido = "numeroCaja"
                };

            if (Caja.montoApertura < 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El monto para abrir la caja debe ser un número mayor a cero.",
                    campoInvalido = "montoApertura"
                };

            return new resultadoOperacion { esValido = true };
        }

        public static resultadoOperacion abirCaja(oCaja caja)
        {
            var validacion = validarAbrirCaja(caja);
            if (!validacion.esValido)
                return validacion;

            try
            {
                int idGenerado;
                bool resultado = cajaDal.abrirCaja(caja, out idGenerado);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro almacenado satisfactoriamente.",
                        idCaja = idGenerado
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido = false,
                        mensaje = "No se puedo abrir la caja. Verfique los datos."
                    };
                }
            }

            catch (Exception)
            {
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ocurrió un error inesperado al abir la caja."
                };
            }
        }
        public static DataTable SumarTotales(int idUsuario)
        {
            //try
            //{
                return cajaDal.SumarTotales(idUsuario);
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al sumar totales.");
            //}
        }

        private static resultadoOperacion validarCerrarCaja(oCaja Caja)
        {
            if (Caja.idCaja == 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "No se ha especificad la caja a cerrar.",
                    campoInvalido = "idCaja"
                };

            if (Caja.totalEfectivo <= 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El total en efectivo debe ser un número mayor a cero.",
                    campoInvalido = "totalEfectivo"
                };

            if (Caja.totalTarjeta < 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El total en tarjeta debe ser un número mayor a cero.",
                    campoInvalido = "totalTarjeta"
                };

            if (Caja.totalEntregado < 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El total a entregar debe ser un número mayor a cero.",
                    campoInvalido = "totalEntregado"
                };

            if (string.IsNullOrWhiteSpace(Caja.codigo))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Debe especificar el código del supervisor que autoriza el cierre de caja.",
                    campoInvalido = "codigo"
                };

            return new resultadoOperacion { esValido = true };
        }

        public static resultadoOperacion CerrarCaja(oCaja caja)
        {
            var validacion = validarCerrarCaja(caja);
            if (!validacion.esValido)
                return validacion;

            //try
            //{
                string supervisorDevuelto;
                bool resultado = cajaDal.CerrarCaja(caja, out supervisorDevuelto);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "La caja fue cerrada satisfactoriamente.",
                        supervisor = supervisorDevuelto
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido = false,
                        mensaje = "No se puedo cerrar la caja. Verfique los datos."
                    };
                }
            //}

            //catch (Exception)
            //{
            //    return new resultadoOperacion
            //    {
            //        esValido = false,
            //        mensaje = "Ocurrió un error inesperado al cerrar la caja."
            //    };
            //}
        }
    }
}
