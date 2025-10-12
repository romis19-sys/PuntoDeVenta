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
    public class bLaboratorio
    {
        private static readonly dLaboratorio laboratorioDal = new dLaboratorio();

        // listar
        public static DataTable listarLaboratorios()
        {
            try
            {
                return laboratorioDal.listarLaboratorio();
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al listar el registro.");
            }
        }

        // buscar
        public static DataTable buscarLaboratorios(string laboratorio)
        {
            try
            {
                return laboratorioDal.buscarLaboratorio(laboratorio);
            }
            catch (Exception)
            {
                throw new ApplicationException("Error al buscar  el registro.");
            }
        }

        // agregar
        private static resultadoOperacion validarLaboratorio(oLaboratorio laboratorios)
        {
            if (string.IsNullOrWhiteSpace(laboratorios.laboratorio))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el nombre del laboratorio",
                    campoInvalido = "Laboratorio"
                };
            if (string.IsNullOrWhiteSpace(laboratorios.telefono))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el telefono laboratorio",
                    campoInvalido = "Telefono"
                };
            if (string.IsNullOrWhiteSpace(laboratorios.contacto))
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ingrese el contacto del laboratorio",
                    campoInvalido = "Contacto"
                };
            return new resultadoOperacion { esValido = true }; 

        }
        public static resultadoOperacion registrarLaboratorio(oLaboratorio laboratorios)
        {
            var validacion = validarLaboratorio(laboratorios);
            if (!validacion.esValido)
                return validacion;

            try
            {
                bool resultado = laboratorioDal.registrarLaboratorio(laboratorios);
                
                if(resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro almacenado sastifactoriamente"
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido = false,
                        mensaje = "No se pudo crear el registro. Verifique los datos"
                    };
                }
            }
            catch (Exception)
            {
                return new resultadoOperacion
                {
                    esValido = false,
                    mensaje = "Ocurrió un error inesperado al guardar el registro"
                };
            }
        }

        // actualizar
        public static resultadoOperacion editarLaboratorio(oLaboratorio laboratorios)
        {
            var validacion = validarLaboratorio(laboratorios);
            if (!validacion.esValido)
                return validacion;

            try
            {
                bool resultado = laboratorioDal.editarLaboratorio(laboratorios);

                if (resultado)
                {
                    return new resultadoOperacion
                    {
                        esValido = true,
                        mensaje = "Registro actualizado sastifactoriamente"
                    };
                }
                else
                {
                    return new resultadoOperacion
                    {
                        esValido = false,
                        mensaje = "No se pudo actualizar el registro. Verifique los datos"
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

        // eliminar
        public static string eliminarLaboratorio(int idLaboratorio)
        {
            if (idLaboratorio <= 0)
                return "Debe especificar un registro válido";

            try
            {
                bool resultado = laboratorioDal.eliminarLaboratorio(idLaboratorio);

                if(resultado)
                {
                    return "Registro eliminado con éxito.";
                }
                else
                {
                    return "No se pudo eliminar el registro";
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Error inesperado al eliminar el registro");
            }
        }

    }
}
