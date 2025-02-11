namespace APIUserValidation.Helpers
{
    public static class SwaggerCommentsSPA
    {
        public static class Clients
        {
            public const string GetUsersSummary = "Obtener usuarios";
            public const string GetUsersDescription = @"⚡ Descripción: Este endpoint permite obtener todos los usuarios registrados en la base de datos mediante busqueda global, con soporte para paginación."
                                                           + "\n\n✅ page (int) : Número de página(por defecto: 1)"
                                                           + "\n\n✅ size (int) : Cantidad de registros por página (Valor por defecto: 10)";

            public const string GetUsersByIdSummary = "Obtener usuarios por parametro idPerson";
            public const string GetUsersByIdDescription = @"⚡ Descripción: Este endpoint permite obtener un usuario en especifico registrado en la base de datos mediante busqueda por parametros de entrada"
                                                           + "\n\n✅ person ID(int) : ID de cada persona-usuario.";

            public const string GetUsersByParametersSummary = "Obtener usuarios por parametros (Documento de identidad / Email)";
            public const string GetUsersByParametersDescription = @"⚡ Descripción: Este endpoint permite obtener un usuario en especifico registrado en la base de datos, mediante busqueda por parametros de entrada, opcionales sea el caso."
                                                                     + "\n\n✅ email (string): Correo electrónico personal del usuario.";


            public const string CreateUserSummary = "Crear nuevos usuarios";
            public const string CreateUserDescription = @"⚡ Descripción: Este endpoint permite crear un nuevo registro de usuario en la base de datos mediante los datos solicitados en el cuerpo de esta petición."
                                                               + "\n\n🚨 Atención: \n\nTodos los datos requeridos deben ser proporcionados en su totalidad; de lo contrario, se generará un error.Puede cargar los registros en formato JSON para su procesamiento."
                                                               + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia."
                                                               + "\n\n✅ identificationNumber (int): Número de identificación del usuario."
                                                               + "\n\n✅ clientName (string): Nombre del usuario."
                                                               + "\n\n✅ clientLastName (string): Apellido del usuario."
                                                               + "\n\n✅ genderId (int): Donde (1) = Masculino, (2) = Femenino, (3) = No responde."
                                                               + "\n\n✅ birthday (Date): Fecha de nacimiento del usuario. Es importante respetar el mismo formato para evitar errores al cargar la información del "
                                                               + "usuario. (Ejemplo: 2025-02-07T20:03:12.148Z). Se debe tener en cuenta que esto generará la edad real de la persona, por lo cual se encuentra "
                                                               + "parametrizada en un rango de 2 hasta los 95 años de edad, si ingresa una fecha menor o mayor a la estipulada arrojará error."
                                                               + "\n\n✅ email (string): Correo electrónico personal del usuario."
                                                               + "\n\n✅ phone (string): Teléfono personal del usuario."
                                                               + "\n\n✅ rolId (int): Donde (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                               + "\n\n✅ statusId (int): Donde (1) = Activo, (2) = Inactivo, (3) = Suspendido, (4) = Bloqueado, (5) = Supervisor, (6) = Baneado."
                                                               + "\n\n✅ userName (string): Nombre de usuario."
                                                               + "\n\n✅ password (string): contraseña establecida por el usuario";

            public const string BulkInsertUsersSummary = "Creación de usuarios masivos";
            public const string BulkInsertUsersDescription = @"⚡ Descripción: Este endpoint permite crear múltiples registros de usuarios en una sola transacción dentro de la base de datos, utilizando los datos proporcionados en el cuerpo de la petición."
                                                               + "\n\n🚨 Atención: \n\nTodos los datos requeridos deben ser proporcionados en su totalidad; de lo contrario, se generará un error. Puede cargar los registros en formato JSON para su procesamiento."
                                                               + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia."
                                                               + "\n\n✅ identificationNumber (int): Número de identificación del usuario."
                                                               + "\n\n✅ clientName (string): Nombre del usuario."
                                                               + "\n\n✅ clientLastName (string): Apellido del usuario."
                                                               + "\n\n✅ genderId (int): Donde (1) = Masculino, (2) = Femenino, (3) = No responde."
                                                               + "\n\n✅ birthday (Date): Fecha de nacimiento del usuario. Es importante respetar el mismo formato para evitar errores al cargar la información del usuario. \n(Ejemplo: 2025-02-07T20:03:12.148Z)."
                                                               + "\n\n✅ email (string): Correo electrónico personal del usuario."
                                                               + "\n\n✅ phone (string): Teléfono personal del usuario."
                                                               + "\n\n✅ rolId (int): Donde (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                               + "\n\n✅ statusId (int): Donde (1) = Activo, (2) = Inactivo, (3) = Suspendido, (4) = Bloqueado, (5) = Supervisor, (6) = Baneado."
                                                               + "\n\n✅ userName (string): Nombre de usuario."
                                                               + "\n\n✅ password (string): contraseña establecida por el usuario";

            public const string InsertUserToExistingPersonSummary = "Crear usuario en base a una persona ya registrada";
            public const string InsertUserToExistingPersonDescription = @"⚡ Descripción: Este endpoint permite insertar la información de usuario a una persona ya registrada, con fines de completar el registro correspondiente:"
                                                               + "\n\n🚨 Atención: \n\nTodos los datos requeridos deben ser proporcionados en su totalidad; de lo contrario, se generará un error.Puede cargar los registros en formato JSON para su procesamiento."
                                                               + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia."
                                                               + "\n\n✅ identificationNumber (int): Número de identificación del usuario."
                                                               + "\n\n✅ userName (string): Nombre de usuario."
                                                               + "\n\n✅ password (string): contraseña establecida por el usuario"
                                                               + "\n\n✅ rolId (int): Donde (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                               + "\n\n✅ statusId (int): Donde (1) = Activo, (2) = Inactivo, (3) = Suspendido, (4) = Bloqueado, (5) = Supervisor, (6) = Baneado.";
           
            public const string UpdateUserSummary = "Actualizar usuario en base a una persona ya registrado";
            public const string UpdateUserDescription = @"⚡ Descripción: Este endpoint permite actualizar los registros de usuario existente en base de datos:"
                                                               + "\n\n🚨 Atención: \n\nTodos los datos requeridos deben ser proporcionados en su totalidad; de lo contrario, se generará un error.Puede cargar los registros en formato JSON para su procesamiento."
                                                               + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia."
                                                               + "\n\n✅ identificationNumber (int): Número de identificación del usuario."
                                                               + "\n\n✅ clientName (string): Nombre del usuario."
                                                               + "\n\n✅ clientLastName (string): Apellido del usuario."
                                                               + "\n\n✅ genderId (int): Donde (1) = Masculino, (2) = Femenino, (3) = No responde."
                                                               + "\n\n✅ birthday (Date): Fecha de nacimiento del usuario. Es importante respetar el mismo formato para evitar errores al cargar la información del usuario. \n(Ejemplo: 2025-02-07T20:03:12.148Z)."
                                                               + "\n\n✅ email (string): Correo electrónico personal del usuario."
                                                               + "\n\n✅ phone (string): Teléfono personal del usuario."
                                                               + "\n\n✅ rolId (int): Donde (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                               + "\n\n✅ statusId (int): Donde (1) = Activo, (2) = Inactivo, (3) = Suspendido, (4) = Bloqueado, (5) = Supervisor, (6) = Baneado."
                                                               + "\n\n✅ userName (string): Nombre de usuario."
                                                               + "\n\n✅ password (string): contraseña establecida por el usuario";

            public const string DeleteUserSummary = "Eliminar usuario de la base de datos";
            public const string DeleteUserDescription = @"⚡ Descripción: Este endpoint permite eliminar los registros de usuario existente en base de datos:"
                                                           + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia"
                                                           + " y el número del documento de identidad, importante, ambos deben coincidir de acuerdo al registro de lo contrario no traera el usuario correspondiente";

            public const string ValidateUserSummary = "Validación de los usuarios registrados";
            public const string ValidateUserDescription = @"⚡ Descripción: Este endpoint permite realizar la validación de usuarios con contraseñas registrados previamente, este proceso puede vincularse a generación de funcionalidades para users"
                                                               + "\n\n✅ userName (string): Nombre de usuario."
                                                               + "\n\n✅ password (string): contraseña establecida por el usuario";
        }
    }
}
