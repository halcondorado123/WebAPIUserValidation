namespace APIUserValidation.Helpers.SwaggerComments.ClientControlles
{
    public static class SwaggerClientsCommentsSPA
    {
        public static class Clients
        {
            public const string GetClientsSummary = "Obtener Clientes";
            public const string GetClientsDescription = @"⚡ Descripción: Este endpoint permite obtener todos los clientes registrados en la base de datos mediante busqueda global, con soporte para paginación."
                                                           + "\n\n✅ page (int) : Número de página(por defecto: 1)"
                                                           + "\n\n✅ size (int) : Cantidad de registros por página (Valor por defecto: 10)";

            public const string GetClientsByIdSummary = "Obtener clientes por parametro idPerson";
            public const string GetClientsByIdDescription = @"⚡ Descripción: Este endpoint permite obtener un cliente en especifico registrado en la base de datos mediante busqueda por parametros de entrada"
                                                           + "\n\n✅ person ID(int) : ID de cada persona-usuario.";

            public const string CreateClientSummary = "Crear nuevo cliente";
            public const string CreateClientDescription = @"⚡ Descripción: Este endpoint permite crear un nuevo registro de cliente en la base de datos mediante los datos solicitados en el cuerpo de esta petición."
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
                                                               + "\n\n✅ phone (string): Teléfono personal del usuario.";

            public const string BulkInsertClientsSummary = "Creación de clientes masivos";
            public const string BulkInsertClientsDescription = @"⚡ Descripción: Este endpoint permite crear múltiples registros de clientes en una sola transacción dentro de la base de datos, utilizando los datos proporcionados en el cuerpo de la petición."
                                                               + "\n\n🚨 Atención: \n\nTodos los datos requeridos deben ser proporcionados en su totalidad; de lo contrario, se generará un error. Puede cargar los registros en formato JSON para su procesamiento."
                                                               + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia."
                                                               + "\n\n✅ identificationNumber (int): Número de identificación del usuario."
                                                               + "\n\n✅ clientName (string): Nombre del usuario."
                                                               + "\n\n✅ clientLastName (string): Apellido del usuario."
                                                               + "\n\n✅ genderId (int): Donde (1) = Masculino, (2) = Femenino, (3) = No responde."
                                                               + "\n\n✅ birthday (Date): Fecha de nacimiento del usuario. Es importante respetar el mismo formato para evitar errores al cargar la información del "
                                                               + "usuario. (Ejemplo: 2025-02-07T20:03:12.148Z). Se debe tener en cuenta que esto generará la edad real de la persona, por lo cual se encuentra "
                                                               + "parametrizada en un rango de 2 hasta los 95 años de edad, si ingresa una fecha menor o mayor a la estipulada arrojará error."
                                                               + "\n\n✅ email (string): Correo electrónico personal del usuario."
                                                               + "\n\n✅ phone (string): Teléfono personal del usuario.";

            public const string UpdateClientSummary = "Actualizar cliente en base a una persona ya registrado";
            public const string UpdateClientDescription = @"⚡ Descripción: Este endpoint permite actualizar los registros de clientes existente en base de datos:"
                                                               + "\n\n🚨 Atención: \n\nTodos los datos requeridos deben ser proporcionados en su totalidad; de lo contrario, se generará un error.Puede cargar los registros en formato JSON para su procesamiento."
                                                               + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia."
                                                               + "\n\n✅ identificationNumber (int): Número de identificación del usuario."
                                                               + "\n\n✅ clientName (string): Nombre del usuario."
                                                               + "\n\n✅ clientLastName (string): Apellido del usuario."
                                                               + "\n\n✅ genderId (int): Donde (1) = Masculino, (2) = Femenino, (3) = No responde."
                                                               + "\n\n✅ birthday (Date): Fecha de nacimiento del usuario. Es importante respetar el mismo formato para evitar errores al cargar la información del usuario. \n(Ejemplo: 2025-02-07T20:03:12.148Z)."
                                                               + "\n\n✅ email (string): Correo electrónico personal del usuario."
                                                               + "\n\n✅ phone (string): Teléfono personal del usuario.";
                                                               
            public const string DeleteClientSummary = "Eliminar cliente de la base de datos";
            public const string DeleteClientDescription = @"⚡ Descripción: Este endpoint permite eliminar los registros de cliente existente en base de datos, este proceso es irreversible:"
                                                           + "\n\n✅ identificationId (int): Donde (1) = Cédula de ciudadanía, (2) = Tarjeta de identidad, (3) = Cédula de extranjería, (4) = Pasaporte, (5) = Permiso especial de permanencia"
                                                           + " y el número del documento de identidad, importante, ambos deben coincidir de acuerdo al registro de lo contrario no traera el usuario correspondiente";

        }
    }
}
