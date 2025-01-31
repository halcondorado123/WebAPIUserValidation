using Microsoft.Data.SqlClient;

namespace ApiUserValidation.Data.Exceptions
{
    public static class ExceptionHandler
    {
        public static Exception HandleException(Exception ex)
        {
            switch (ex)
            {
                case SqlException sqlEx:
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    return new Exception("An error occurred while accessing the database. Please try again later.");

                case TimeoutException timeoutEx:
                    Console.WriteLine($"Timeout Error: {timeoutEx.Message}");
                    return new Exception("The request timed out. Please try again later.");

                case InvalidOperationException invalidOpEx:
                    Console.WriteLine($"Operation Error: {invalidOpEx.Message}");
                    return new Exception("An unexpected error occurred while processing the request.");

                default:
                    Console.WriteLine($"Error: {ex.Message}");
                    return new Exception(ex.Message);
            }
        }
    }
}
