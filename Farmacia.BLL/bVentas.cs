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
    public class bVentas
    {
        private static readonly dVentas ventasDal = new dVentas();

        public static DataTable listarVentas()
        {
            try
            {
                return ventasDal.listarVentas();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar registros, Detalles: " + ex.Message);
            }
        }

        public static DataTable buscarVentas(DateTime fechaInicio, DateTime fechaFinalo)
        {
            try
            {
                return ventasDal.buscarVenta(fechaInicio, fechaFinalo);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el registro, Detalles: " + ex.Message);
            }
        }

        private static resultadoOperacion validarVentas(oVentas Ventas)
        {
            if (Ventas.detalles == null || Ventas.detalles.Rows.Count == 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Debe agregar al menos un producto al carrito de compras.",
                };

            if (Ventas.totalGeneral <= 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El total de la venta debe ser mayor a cero.",
                    campoInvalido = "totalGeneral"
                };

            if (Ventas.impuesto > Ventas.totalGeneral)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El impuesto no puede ser mayor al total facturado.",
                    campoInvalido = "totalGeneral"
                };

            return new resultadoOperacion { esValido = true };
        }

        public static resultadoOperacion registrarVentas(oVentas Ventas)
        {
            var validacion = validarVentas(Ventas);
            if (!validacion.esValido)
                return validacion;

            try
            {
                string ventaGenerado;
                bool resultado = ventasDal.registrarVentas(Ventas, out ventaGenerado);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro almacenado satisfactoriamente.",
                        numeroVenta = ventaGenerado,
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
            }

            catch (Exception ex)
            {
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ocurrió un error inesperado al guardar el registro, Detalles: " + ex.Message
                };
            }
        }
    }
}
