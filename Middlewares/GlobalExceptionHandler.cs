using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            if (exception is RegraNegocioException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails.Title = "Erro de Validação";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = exception.Message;
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Erro Interno no Servidor";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde.";
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}