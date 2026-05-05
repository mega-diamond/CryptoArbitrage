namespace CryptoArbitrage.Middleware
{
    public class ErrorLogging
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<ErrorLogging> _logger;

        public ErrorLogging(RequestDelegate next, ILogger<ErrorLogging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Log before the request is passed to the next middleware
                _logger.LogInformation("Incoming Request: {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                await _next(context); // Pass the request to the next middleware

                // Log after the request is processed
                _logger.LogInformation("Outgoing Response: Status Code {StatusCode}",
                    context.Response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during request processing");
                throw; // Re-throw the exception
            }
        }
    }
}
