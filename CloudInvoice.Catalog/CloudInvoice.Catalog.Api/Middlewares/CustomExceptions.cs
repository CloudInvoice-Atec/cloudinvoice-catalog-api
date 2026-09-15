namespace CloudInvoice.Catalog.Api.Middlewares
{
    public class ApplicationException : Exception
    {
        public int StatusCode { get; set; }

        public ApplicationException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message, 404) { }
    }

    public class ValidationException : ApplicationException
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public ValidationException(string message, Dictionary<string, string[]> errors = null) : base(message, 400)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }

    public class ConflictException : ApplicationException
    {
        public ConflictException(string message) : base(message, 409) { }
    }

    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message = "Acesso negado. Você não tem permissão para realizar esta ação.") 
            : base(message, 403) { }
    }

    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException(string message = "Autenticação necessária.") 
            : base(message, 401) { }
    }

    public class DatabaseException : ApplicationException
    {
        public DatabaseException(string message, Exception innerException = null) 
            : base(message, 500)
        {
            if (innerException != null)
            {
                Data["InnerException"] = innerException.Message;
            }
        }
    }
}
