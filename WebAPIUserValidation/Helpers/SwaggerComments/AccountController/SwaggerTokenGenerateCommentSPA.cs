namespace APIUserValidation.Helpers.SwaggerComments.AccountController
{
    public static class SwaggerTokenGenerateCommentSPA
    {
        public static class UserAuthorization
        {
            public const string UserAccountTokenSummary = "Autenticación de Usuario";
            public const string UserAccountTokenDescription = @"⚡ Descripción: Este endpoint permite validar usuarios registrados con sus contraseñas. Tras una validación exitosa, genera un token de acceso que se puede utilizar para autenticar y autorizar solicitudes posteriores."
                                                         + "\n\n✅ userName (string): Nombre de usuario."
                                                         + "\n\n✅ password (string): Contraseña del usuario."
                                                         + "\n\n🔑 accessToken (string): Token de acceso generado tras la autenticación exitosa.";
        }
    }
}
