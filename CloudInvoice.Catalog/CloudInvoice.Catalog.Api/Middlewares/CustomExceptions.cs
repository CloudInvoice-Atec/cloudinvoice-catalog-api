namespace CloudInvoice.Catalog.Api.Middlewares
{
    /// <summary>
    /// Exceção base customizada para a aplicação
    /// </summary>
    public class ApplicationException : Exception
    {
        public int StatusCode { get; set; }

        public ApplicationException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Exceção lançada quando um recurso não é encontrado
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message, 404) { }
    }

    /// <summary>
    /// Exceção lançada quando há erro de validação
    /// </summary>
    public class ValidationException : ApplicationException
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public ValidationException(string message, Dictionary<string, string[]> errors = null) : base(message, 400)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }
    }

    /// <summary>
    /// Exceção lançada quando há conflito nos dados (ex.: recurso duplicado)
    /// </summary>
    public class ConflictException : ApplicationException
    {
        public ConflictException(string message) : base(message, 409) { }
    }

    /// <summary>
    /// Exceção lançada quando o utilizador não tem permissão
    /// </summary>
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message = "Acesso negado. Você não tem permissão para realizar esta ação.") 
            : base(message, 403) { }
    }

    /// <summary>
    /// Exceção lançada quando há erro de autenticação
    /// </summary>
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException(string message = "Autenticação necessária.") 
            : base(message, 401) { }
    }

    /// <summary>
    /// Exceção lançada quando há erro de operação na base de dados
    /// </summary>
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
