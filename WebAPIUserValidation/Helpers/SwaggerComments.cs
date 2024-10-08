namespace APIUserValidation.Helpers
{
    public static class SwaggerComments
    {
        public static class Clients
        {
            public const string GetAllUsersSummary = "Método funcional para buscar todos los usuarios registrados";
            public const string GetAllUsersDescription = @" Este endpoint permite a los desarrolladores recuperar una lista completa de todos los usuarios registrados en el sistema. Utilizando una solicitud GET a la ruta /api/users, 
este método responde con un objeto JSON que contiene un arreglo de usuarios. Cada usuario está representado por un objeto que incluye información relevante como id, nombre, correo electrónico y fecha de registro. Este método es 
esencial para funcionalidades administrativas y de gestión de usuarios, permitiendo a los administradores visualizar y gestionar la base de datos de usuarios de manera eficiente. Se recomienda implementar paginación si la cantidad de 
usuarios es considerable para mejorar el rendimiento y la experiencia del usuario.";

            public const string GetUserByIdSummary = "Método funcional para buscar registro de usuario por ID único";
            public const string GetUserByIdDescription = @" Este endpoint permite a los desarrolladores recuperar un usuario específico a través de su ID. Utilizando una solicitud GET a la ruta /api/users/{id}, este método responde con un objeto JSON que contiene
la información del usuario.";

            public const string CreateUserSummary = "Genera un nuevo registro de usuario";
            public const string CreateUserDescription = @" Este endpoint permite a los desarrolladores crear un nuevo usuario en el sistema. Se utiliza una solicitud POST a la ruta /api/users, con un objeto JSON en el cuerpo de la solicitud que contiene la 
información del usuario a crear.";


            public const string BulkUserSummary = "Genera un registro masivo de usuariosv";
            public const string BulkUserDescription = @" Este endpoint permite a los desarrolladores crear de manera masiva usuarios en el sistema.";
            
            public const string UpdateUserSummary = "Genera actualización a un registro de usuario ya existente - Toma como parámetro de ID";
            public const string UpdateUserDescription = "Este endpoint permite a los desarrolladores actualizar la información de un usuario existente.\n" +
                "Se utiliza una solicitud PUT a la ruta /api/users/{id}, con un objeto JSON en el cuerpo\n" + "de la solicitud que contiene la nueva información del usuario.\n\n" +
                "Para realizar la actualización exitosamente, se recomienda acceder al método de consulta\n" + "GetClientByID, copiar el archivo JSON del usuario consultado y modificar únicamente los siguientes items:\n" +
                "* RolID\n" + "* Identification\n" + "* IdentificationNumber\n" + "* ClientName\n" +  "* ClientLastName\n" + "* RelatId\n" + "* Age\n" + "* Birthday";

            public const string DeleteUserSummary = "Método funcional para borrar registro de usuario por ID único";
            public const string DeleteUserDescription = @"Este endpoint permite a los desarrolladores eliminar un usuario específico a través de su ID. Se utiliza una solicitud DELETE a la ruta /api/users/{id}.";

        }
    }
}
