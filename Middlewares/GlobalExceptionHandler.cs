using Microsoft.AspNetCore.Diagnostics;
using raizes_do_nordeste.Exceptions;

namespace raizes_do_nordeste.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Uma exceção foi capturada: {Message}", exception.Message);

            int statusCode = StatusCodes.Status500InternalServerError;
            string errorName = "ERRO_INTERNO";
            string errorMessage = "Ocorreu um erro inesperado. Tente novamente mais tarde.";

            if (exception is RegraNegocioException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                errorName = "REGRA_DE_NEGOCIO";
                errorMessage = exception.Message;
            }

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var errorResponse = new
            {
                error = errorName,
                message = errorMessage,
                details = Array.Empty<object>(), 
                timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                path = httpContext.Request.Path.Value,
                requestId = httpContext.TraceIdentifier 
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}