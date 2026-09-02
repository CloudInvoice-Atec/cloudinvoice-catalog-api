using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace CloudInvoice.Catalog.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Deixa o pedido avançar na pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message); // Regista o erro no servidor
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ProblemDetails();

            // Mapear tipos de exceção customizados
            if (exception is ApplicationException appEx)
            {
                context.Response.StatusCode = appEx.StatusCode;
                response.Status = appEx.StatusCode;
                response.Title = GetTitleByStatusCode(appEx.StatusCode);
                response.Detail = appEx.Message;

                // Se for ValidationException, adicionar erros específicos
                if (exception is ValidationException valEx && valEx.Errors.Any())
                {
                    response.Extensions["errors"] = valEx.Errors;
                }
            }
            // Tratamento de ArgumentException e ArgumentNullException (validação)
            else if (exception is ArgumentException argEx)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Status = StatusCodes.Status400BadRequest;
                response.Title = "Erro de Validação";
                response.Detail = argEx.Message;
            }
            // Tratamento de InvalidOperationException
            else if (exception is InvalidOperationException invOpEx)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Status = StatusCodes.Status400BadRequest;
                response.Title = "Operação Inválida";
                response.Detail = invOpEx.Message;
            }
            // Tratamento de DbUpdateException (Entity Framework)
            else if (exception.GetType().Name == "DbUpdateException")
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Status = StatusCodes.Status500InternalServerError;
                response.Title = "Erro na Base de Dados";
                response.Detail = "Ocorreu um erro ao processar a operação na base de dados.";
            }
            // Tratamento de DbUpdateConcurrencyException (concorrência)
            else if (exception.GetType().Name == "DbUpdateConcurrencyException")
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                response.Status = StatusCodes.Status409Conflict;
                response.Title = "Conflito de Concorrência";
                response.Detail = "O recurso foi modificado por outro utilizador. Por favor, recarregue e tente novamente.";
            }
            // Tratamento genérico de outras exceções
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Status = StatusCodes.Status500InternalServerError;
                response.Title = "Erro Interno do Servidor";
                response.Detail = exception.Message; // Em produção, considere ocultar detalhes internos
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            return context.Response.WriteAsync(jsonResponse);
        }

        private static string GetTitleByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Erro de Validação",
                401 => "Não Autorizado",
                403 => "Acesso Negado",
                404 => "Não Encontrado",
                409 => "Conflito",
                500 => "Erro Interno do Servidor",
                _ => "Erro"
            };
        }
    }
}
