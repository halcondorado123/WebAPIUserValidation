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

                    if (sqlEx.Message.Contains("No changes detected"))
                    {
                        return new Exception(sqlEx.Message);
                    }

                    // 🔥 Si es otro error, agregamos el prefijo estándar
                    return new Exception($"An error occurred while accessing the database. {sqlEx.Message}");

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

    public static Exception NullHandleException(string customMessage, Exception ex = null)
    {
        string message = ex == null ? customMessage : $"{customMessage} {ex.Message}";
        return new Exception(message);
    }
    }
}
