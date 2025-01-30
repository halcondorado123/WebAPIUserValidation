namespace APIUserValidation.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Aquí va la lógica para encriptar la contraseña
            return BCrypt.Net.BCrypt.HashPassword(password);  // Asegúrate de que BCrypt esté correctamente instalado
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Aquí va la lógica para verificar la contraseña
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);  // Asegúrate de que BCrypt esté correctamente instalado
        }
    }
}
