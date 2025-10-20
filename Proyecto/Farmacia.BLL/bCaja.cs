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
    }
}
