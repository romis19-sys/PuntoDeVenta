using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.Entity
{
    public class Validar
    {
        public static bool emailValidado(string email)
        {
            try
            {
                var correo = new System.Net.Mail.MailAddress(email);
                return correo.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool claveSegura(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
                return false;

            // La contraseña debe tener al menos 7 caracteres
            if (clave.Length < 7)
                return false;

            // Debe contener al menos una mayúscula
            if (!System.Text.RegularExpressions.Regex.IsMatch(clave, "[A-Z]"))
                return false;

            // Debe contener al menos una minúscula
            if (!System.Text.RegularExpressions.Regex.IsMatch(clave, "[a-z]"))
                return false;

            // Debe contener al menos un número
            if (!System.Text.RegularExpressions.Regex.IsMatch(clave, "[0-9]"))
                return false;

            // Debe contener al menos un carácter especial
            if (!System.Text.RegularExpressions.Regex.IsMatch(clave, "[!@#$%^&*(),.?\":{}|<>]"))
                return false;

            return true;
        }
    }
}

