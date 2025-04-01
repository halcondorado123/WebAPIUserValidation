using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Services.Handlers
{
    public class PasswordHashHandler
    {
        private static int _iterationCount = 100000;
        private static RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public static string HashPassword(string password)
        {

            int saltSize = 128 / 8;
            var salt = new byte[saltSize];
            _randomNumberGenerator.GetBytes(salt);
            var subkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, _iterationCount, 256 / 8);

            var outputBytes = new byte[12 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01;
            WriteNetworkByteOrder(outputBytes, 1, (uint)KeyDerivationPrf.HMACSHA512);
            WriteNetworkByteOrder(outputBytes, 5, (uint)_iterationCount);
            WriteNetworkByteOrder(outputBytes, 1, (uint)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            try
            {
                var hashedPassword = Convert.FromBase64String(hash);

                // Extraer valores almacenados
                var keyDerivationPrf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
                var iterationCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
                var saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

                if (saltLength < 128 / 8) return false; // Validar tamaño del salt

                // Extraer el salt
                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(hashedPassword, 13, salt, 0, saltLength);

                // Extraer la clave derivada (hash de la contraseña original)
                int subkeyLength = hashedPassword.Length - (13 + saltLength);
                byte[] storedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(hashedPassword, 13 + saltLength, storedSubkey, 0, subkeyLength);

                // Recalcular el hash con la contraseña ingresada
                byte[] generatedSubkey = KeyDerivation.Pbkdf2(password, salt, keyDerivationPrf, iterationCount, subkeyLength);

                // Comparar subkeys de forma segura
                return CryptographicOperations.FixedTimeEquals(storedSubkey, generatedSubkey);
            }
            catch
            {
                return false; // Evitar que excepciones filtren información
            }
        }



        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value);
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset] << 24)) |
                   ((uint)(buffer[offset + 1] << 16)) |
                   ((uint)(buffer[offset + 2] << 8)) |
                   ((uint)(buffer[offset + 3]));
        }
    }
}
