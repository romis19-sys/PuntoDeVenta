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
    public class bCompras
    {
        #region "Primera Prueba"
        public static DataTable CargarProveedores()
        {
            dCompra Datos = new dCompra();
            return Datos.CargarProveedores();
        }

        private static readonly dCompra comprasDal = new dCompra();
        // lista
        public static DataTable listarCompra()
        {
            try
            {
                return comprasDal.listarCompras();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar registros, Detalles: " + ex.Message);
            }
        }
        // busca
        public static DataTable buscarcompras(DateTime fechaInicio, DateTime fechaFinalo)
        {
            try
            {
                return comprasDal.buscarCompra(fechaInicio, fechaFinalo);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar el registro, Detalles: " + ex.Message);
            }
        }
        // valida
        private static resultadoOperacion validarCompra(oCompra Compras)
        {
            if (Compras.detalles == null || Compras.detalles.Rows.Count == 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Debe agregar al menos un producto al carrito de compras.",
                };

            if (Compras.totalGeneral <= 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El total de la venta debe ser mayor a cero.",
                    campoInvalido = "totalGeneral"
                };

            if (Compras.impuesto > Compras.totalGeneral)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El impuesto no puede ser mayor al total facturado.",
                    campoInvalido = "totalGeneral"
                };
            return new resultadoOperacion { esValido = true };
        }
        // registra
        public static resultadoOperacion registrarCompra(oCompra Compras)
        {
            var validacion = validarCompra(Compras);
            if (!validacion.esValido)
                return validacion;

            try
            {
                string compraGenerada;
                bool resultado = comprasDal.registrarVentas(Compras, out compraGenerada);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro almacenado satisfactoriamente.",
                        numeroCompra = compraGenerada,
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
                    mensaje = "Ocurrió un error inesperado al guardar el registro, Detalles: "
                };
            }
        }
        #endregion
    }
}
