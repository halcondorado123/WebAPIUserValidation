namespace APIUserValidation.Helpers.SwaggerComments.ClientControlles
{
    public class SwaggerClientsCommentsENG
    {
        public static class Clients
        {
            public const string GetClientsSummary = "Get Clients";
            public const string GetClientsDescription = @"⚡ Description: This endpoint allows retrieving all registered clients from the database through a global search, with pagination support."
                                                           + "\n\n✅ page (int) : Page number (default: 1)"
                                                           + "\n\n✅ size (int) : Number of records per page (default: 10)";

            public const string GetClientsByIdSummary = "Get Clients by idPerson parameter";
            public const string GetClientsByIdDescription = @"⚡ Description: This endpoint allows retrieving a specific client registered in the database through input parameter search."
                                                           + "\n\n✅ person ID (int) : ID of each person-user.";

            public const string CreateClientSummary = "Create a new client";
            public const string CreateClientDescription = @"⚡ Description: This endpoint allows creating a new client record in the database using the data requested in the body of this request."
                                                              + "\n\n🚨 Attention: \n\nAll required data must be fully provided; otherwise, an error will occur. You can upload records in JSON format for processing."
                                                              + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity card, (3) = Foreigner ID, (4) = Passport, (5) = Special residence permit."
                                                              + "\n\n✅ identificationNumber (int): User identification number."
                                                              + "\n\n✅ clientName (string): User's first name."
                                                              + "\n\n✅ clientLastName (string): User's last name."
                                                              + "\n\n✅ genderId (int): Where (1) = Male, (2) = Female, (3) = No response."
                                                              + "\n\n✅ birthday (Date): User's birth date. It is important to follow the same format to avoid errors when loading the user's information."
                                                              + " (Example: 2025-02-07T20:03:12.148Z). This will determine the person's real age, which is set within a range of 2 to 95 years old. If a date outside this range is entered, an error will be generated."
                                                              + "\n\n✅ email (string): User's personal email address."
                                                              + "\n\n✅ phone (string): User's personal phone number.";

            public const string BulkInsertClientsSummary = "Bulk Client Creation";
            public const string BulkInsertClientsDescription = @"⚡ Description: This endpoint allows creating multiple client records in a single transaction within the database, using the data provided in the request body."
                                                              + "\n\n🚨 Attention: \n\nAll required data must be fully provided; otherwise, an error will occur. You can upload records in JSON format for processing."
                                                              + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity card, (3) = Foreigner ID, (4) = Passport, (5) = Special residence permit."
                                                              + "\n\n✅ identificationNumber (int): User identification number."
                                                              + "\n\n✅ clientName (string): User's first name."
                                                              + "\n\n✅ clientLastName (string): User's last name."
                                                              + "\n\n✅ genderId (int): Where (1) = Male, (2) = Female, (3) = No response."
                                                              + "\n\n✅ birthday (Date): User's birth date. It is important to follow the same format to avoid errors when loading the user's information."
                                                              + " (Example: 2025-02-07T20:03:12.148Z). This will determine the person's real age, which is set within a range of 2 to 95 years old. If a date outside this range is entered, an error will be generated."
                                                              + "\n\n✅ email (string): User's personal email address."
                                                              + "\n\n✅ phone (string): User's personal phone number.";

            public const string UpdateClientSummary = "Update client based on an already registered person";
            public const string UpdateClientDescription = @"⚡ Description: This endpoint allows updating existing client records in the database."
                                                          + "\n\n🚨 Attention: \n\nAll required data must be fully provided; otherwise, an error will occur. You can upload records in JSON format for processing."
                                                          + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity card, (3) = Foreigner ID, (4) = Passport, (5) = Special residence permit."
                                                          + "\n\n✅ identificationNumber (int): User identification number."
                                                          + "\n\n✅ clientName (string): User's first name."
                                                          + "\n\n✅ clientLastName (string): User's last name."
                                                          + "\n\n✅ genderId (int): Where (1) = Male, (2) = Female, (3) = No response."
                                                          + "\n\n✅ birthday (Date): User's birth date. It is important to follow the same format to avoid errors when loading the user's information."
                                                          + " (Example: 2025-02-07T20:03:12.148Z)."
                                                          + "\n\n✅ email (string): User's personal email address."
                                                          + "\n\n✅ phone (string): User's personal phone number.";

            public const string DeleteClientSummary = "Delete client from the database";
            public const string DeleteClientDescription = @"⚡ Description: This endpoint allows deleting existing client records from the database. This process is irreversible."
                                                         + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity card, (3) = Foreigner ID, (4) = Passport, (5) = Special residence permit"
                                                         + " and the identity document number. Both must match the existing record; otherwise, the corresponding user will not be found.";
        }
    }
}
