using Farmacia.DAL;
using Farmacia.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.BLL
{
    public class bFarmaco
    {
        private static readonly dFarmaco FarmacoDal = new dFarmaco();

        public static DataTable listarTodosFarmacos()
        {
            try
            {
                return FarmacoDal.listarTodosFarmacos();
                //return FarmacoDal.listarFarmacos();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al listar registros.");
            }
        }

        public static DataTable listarFarmacos()
        {
            try
            {
                return FarmacoDal.listarFarmacos();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al listar registros.");
            }
        }

        public static DataTable buscarTodosFarmaco(int filtro, string valor)
        {
            try
            {
                return FarmacoDal.buscarTodosFarmacos(filtro, valor);
                //return FarmacoDal.listarFarmacos();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al buscar registros.");
            }
        }

        public static DataTable buscarFarmacoVenta(int filtro, string valor)
        {
            //try
            //{
                return FarmacoDal.buscarFarmacosVenta(filtro, valor);
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al buscar registros.");
            //}
        }

        public static DataTable buscarFarmaco(int filtro, string valor)
        {
            //try
            //{
            return FarmacoDal.buscarFarmacos(filtro, valor);
            //}
            //catch (Exception)
            //{
            //    throw new ApplicationException("Error al buscar registros.");
            //}
        }

        private static resultadoOperacion validarFarmaco(oFarmaco farmaco)
        {
            if (string.IsNullOrWhiteSpace(farmaco.codigo))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el código del fármaco.",
                    campoInvalido = "codigo"
                };

            if (string.IsNullOrWhiteSpace(farmaco.nombreFarmaco))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el nombre del fármaco.",
                    campoInvalido = "nombreFarmaco"
                };

            if (string.IsNullOrWhiteSpace(farmaco.descripcion))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese la descripción del fármaco.",
                    campoInvalido = "descripcion"
                };

            if (farmaco.idLaboratorio == 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Seleccione un laboratorio.",
                    campoInvalido = "idLaboratorio"
                };

            if (farmaco.stock <= 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El stock debe ser mayor a cero.",
                    campoInvalido = "stock"
                };

            if (farmaco.precioCompra <= 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El precio de compra debe ser mayor a cero.",
                    campoInvalido = "precioCompra"
                };

            if (farmaco.precioVenta <= 0)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El precio de venta debe ser mayor a cero.",
                    campoInvalido = "precioVenta"
                };

            if (farmaco.precioCompra >= farmaco.precioVenta)
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "El precio de compra debe ser menor que el precio de venta.",
                    campoInvalido = "precioVenta"
                };
            if (string.IsNullOrWhiteSpace(farmaco.nombreFarmaco))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el nombre del fármaco.",
                    campoInvalido = "nombreFarmaco"
                };

            return new resultadoOperacion { esValido = true };

        }

        public static resultadoOperacion registrarFarmaco(oFarmaco farmaco)
        {
            var validacion = validarFarmaco(farmaco);
            if (!validacion.esValido)
                return validacion;

            //try
            //{
                bool resultado = FarmacoDal.registrarFarmaco(farmaco);

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

        public static resultadoOperacion editarFarmaco(oFarmaco farmaco)
        {
            var validacion = validarFarmaco(farmaco);
            if (!validacion.esValido)
                return validacion;

            try
            {
                bool resultado = FarmacoDal.editarFarmaco(farmaco);

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

        public static string eliminarFarmaco(int idFarmaco)
        {
            if (idFarmaco <= 0)
                return "Debe especificar un registro válido";

            try
            {
                bool resultado = FarmacoDal.eliminarFarmaco(idFarmaco);

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
