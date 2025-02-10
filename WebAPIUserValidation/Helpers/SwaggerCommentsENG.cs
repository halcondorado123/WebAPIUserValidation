using ApiUserValidation.Models.Entities.UserAttributesME;
using ApiUserValidation.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Numerics;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace APIUserValidation.Helpers
{
    public static class SwaggerCommentsENG
    {
        public static class Clients
        {


            public const string GetUsersSummary = "Get Users";
            public const string GetUsersDescription = @"⚡ Description: This endpoint allows retrieving all registered users in the database through a global search, with pagination support."
                                                           + "\n\n\nInput Parameters:"
                                                           + "\n\n✅ page(int) : Page number (default: 1)"
                                                           + "\n\n✅ size(int) : Number of records per page (Default: 10)";



            public const string GetUsersByIdSummary = "Get Users by idPerson parameter";
            public const string GetUsersByIdDescription = @"⚡ Description: This endpoint allows retrieving a specific user registered in the database by searching with input parameters."
                                                                + "\n\n\nInput Parameters:"
                                                                + "\n\n✅ personID(int) : ID of each person-user.";


            public const string GetUsersByParametersSummary = "Get users by parameters (Identity Document / Email)";
            public const string GetUsersByParametersDescription = @"⚡ Description: This endpoint allows retrieving a specific user registered in the database by searching with input parameters, which are optional depending on the case."
                                                                   + "\n\n\nInput parameters:"
                                                                   + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity Card, (3) = Foreigner ID, (4) = Passport, (5) = Special Stay Permit"
                                                                   + " and the identity document number. Important: Both must match according to the record; otherwise, the corresponding user will not be retrieved."
                                                                   + "\n\n✅ email (string): The user's personal email address.";

            public const string CreateUserSummary = "Create new users";
            public const string CreateUserDescription = @"⚡ Description: This endpoint allows creating a new user record in the database using the required data in the request body."
                                                               + "\n\n🚨 Attention: \n\nAll required data must be fully provided; otherwise, an error will be generated. You can upload the records in JSON format for processing."
                                                               + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity Card, (3) = Foreigner ID, (4) = Passport, (5) = Special Stay Permit."
                                                               + "\n\n✅ identificationNumber (int): User's identification number."
                                                               + "\n\n✅ clientName (string): User's first name."
                                                               + "\n\n✅ clientLastName (string): User's last name."
                                                               + "\n\n✅ genderId (int): Where (1) = Male, (2) = Female, (3) = Prefer not to say."
                                                               + "\n\n✅ birthday (Date): User's date of birth. It is important to respect the same format to avoid errors when uploading user information. \n" 
                                                               + "(Example: 2025-02-07T20:03:12.148Z)."
                                                               + "\n\n✅ email (string): User's personal email."
                                                               + "\n\n✅ phone (string): User's personal phone number."
                                                               + "\n\n✅ rolId (int): Where (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                               + "\n\n✅ statusId (int): Where (1) = Active, (2) = Inactive, (3) = Suspended, (4) = Blocked, (5) = Supervisor, (6) = Banned."
                                                               + "\n\n✅ userName (string): Enter the username."
                                                               + "\n\n✅ password (string): User-defined password."
                                                               + "\n\n✅ CreatedAt (Date): Record creation date (automatically generated when the record is created)."
                                                               + "\n\n✅ UpdatedAt (Date): Record update date (automatically generated when the record is updated)."
                                                               + "\n\n✅ LastLogin (Date): User's last login date (automatically updated).";




            public const string BulkInsertUsersSummary = "Bulk User Creation";
            public const string BulkInsertUsersDescription = @"⚡ Description: This endpoint allows the creation of multiple user records in a single transaction within the database, using the data provided in the request body."
                                                                     + "\n\n🚨 Attention: \n\nAll required data must be fully provided; otherwise, an error will be generated. You can upload the records in JSON format for processing."
                                                                     + "\n\n✅ personID(int) : ID de cada persona a la que se le va a modificar el registro"
                                                                     + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity Card, (3) = Foreigner ID, (4) = Passport, (5) = Special Stay Permit."
                                                                     + "\n\n✅ identificationNumber (int): User's identification number."
                                                                     + "\n\n✅ clientName (string): User's first name."
                                                                     + "\n\n✅ clientLastName (string): User's last name."
                                                                     + "\n\n✅ genderId (int): Where (1) = Male, (2) = Female, (3) = Prefer not to say."
                                                                     + "\n\n✅ birthday (Date): User's date of birth. It is important to follow the correct format to avoid errors when uploading user information. \n(Example: 2025-02-07T20:03:12.148Z)."
                                                                     + "\n\n✅ email (string): User's personal email."
                                                                     + "\n\n✅ phone (string): User's personal phone number."
                                                                     + "\n\n✅ rolId (int): Where (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                                     + "\n\n✅ statusId (int): Where (1) = Active, (2) = Inactive, (3) = Suspended, (4) = Blocked, (5) = Supervisor, (6) = Banned."
                                                                     + "\n\n✅ userName (string): Username."
                                                                     + "\n\n✅ password (string): User-defined password."
                                                                     + "\n\n✅ CreatedAt (Date): Record creation date (automatically generated when the record is created)."
                                                                     + "\n\n✅ UpdatedAt (Date): Record update date (automatically generated when the record is updated)."
                                                                     + "\n\n✅ LastLogin (Date): Last login date of the user (automatically updated).";




            public const string InsertUserToExistingPersonSummary = "Create User for an Existing Person";
            public const string InsertUserToExistingPersonDescription = @"⚡ Description: This endpoint allows inserting user information for an already registered person, in order to complete the corresponding registration:"
                                                                               + "\n\n🚨 Attention: \n\nAll required data must be fully provided; otherwise, an error will be generated. You can upload the records in JSON format for processing."
                                                                               + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity Card, (3) = Foreigner ID, (4) = Passport, (5) = Special Stay Permit."
                                                                               + "\n\n✅ identificationNumber (int): User's identification number."
                                                                               + "\n\n✅ userName (string): Username."
                                                                               + "\n\n✅ password (string): User-defined password."
                                                                               + "\n\n✅ rolId (int): Where (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                                               + "\n\n✅ statusId (int): Where (1) = Active, (2) = Inactive, (3) = Suspended, (4) = Blocked, (5) = Supervisor, (6) = Banned."
                                                                               + "\n\n✅ CreatedAt (Date): Record creation date (automatically generated when the record is created)."
                                                                               + "\n\n✅ UpdatedAt (Date): Record update date (automatically generated when the record is updated)."
                                                                               + "\n\n✅ LastLogin (Date): Last login date of the user (automatically updated).";


            public const string UpdateUserSummary = "Update an existing user based on a registered person";
            public const string UpdateUserDescription = @"⚡ Description: This endpoint allows updating the records of an existing user in the database:"
                                                       + "\n\n🚨 Attention: \n\nAll required data must be fully provided; otherwise, an error will be generated. You can upload records in JSON format for processing."
                                                       + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity Card, (3) = Foreigner ID, (4) = Passport, (5) = Special Stay Permit."
                                                       + "\n\n✅ identificationNumber (int): User's identification number."
                                                       + "\n\n✅ clientName (string): User's first name."
                                                       + "\n\n✅ clientLastName (string): User's last name."
                                                       + "\n\n✅ genderId (int): Where (1) = Male, (2) = Female, (3) = Prefer not to say."
                                                       + "\n\n✅ birthday (Date): User's date of birth. It is important to maintain the same format to avoid errors when loading user information. \n(Example: 2025-02-07T20:03:12.148Z)."
                                                       + "\n\n✅ email (string): User's personal email address."
                                                       + "\n\n✅ phone (string): User's personal phone number."
                                                       + "\n\n✅ rolId (int): Where (1) = Admin, (2) = User, (3) = Guest, (4) = Editor, (5) = Supervisor, (6) = Manager, (7) = Customer."
                                                       + "\n\n✅ statusId (int): Where (1) = Active, (2) = Inactive, (3) = Suspended, (4) = Blocked, (5) = Supervisor, (6) = Banned."
                                                       + "\n\n✅ userName (string): Username."
                                                       + "\n\n✅ password (string): User-defined password."
                                                       + "\n\n✅ UpdatedAt (Date): Record update date (automatically generated upon update)."
                                                       + "\n\n✅ LastLogin (Date): Date of the user's last login (automatically updated).";




            public const string DeleteUserSummary = "Delete user from the database";
            public const string DeleteUserDescription = @"⚡ Description: This endpoint allows deleting existing user records from the database:"
                                                       + "\n\n\nInput Parameters:"
                                                       + "\n\n✅ identificationId (int): Where (1) = Citizenship ID, (2) = Identity Card, (3) = Foreigner ID, (4) = Passport, (5) = Special Stay Permit."
                                                       + "\n\n✅ identificationNumber (string): The identification document number. Both parameters must match the registered data; otherwise, the corresponding user will not be found.";

        }
    }
}
