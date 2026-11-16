using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatorioDW.ValueObjects.Usuario
{
    public class Password
    {
        public string Value { get; init; }

        public Password(string value)
        {
            this.Value = value;
            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                throw new ArgumentException("Requiere una contraseña", nameof(Value));
            }
            if (Value.Length < 8)
            {
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres", nameof(Value));
            }

            bool hasLower = false; //Minuscula
            bool hasUpper = false; //Mayuscula
            bool hasDigit = false; //Digito
            bool hasSpecial = false; //Caracter especial

            foreach (var ch in Value)
            {
                if (char.IsLower(ch)) hasLower = true;
                else if (char.IsUpper(ch)) hasUpper = true;
                else if (char.IsDigit(ch)) hasDigit = true;
                else hasSpecial = true;

                if (hasLower && hasUpper && hasDigit && hasSpecial) break;
            }

            if (!(hasLower && hasUpper && hasDigit && hasSpecial))
            {
                throw new ArgumentException("La contraseña debe contener al menos una letra minúscula, una letra mayúscula, un dígito y un carácter especial", nameof(Value));
            }
        }


        //Verificamos igualdad simple
        public bool Verify(string value) => Value == value;

        //No revelar el valor de la contraseña en ToString

        public override string ToString() => "••••••••";

    }
}
