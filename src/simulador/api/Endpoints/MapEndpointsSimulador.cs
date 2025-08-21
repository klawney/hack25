
using System.Diagnostics.CodeAnalysis;

namespace api.Endpoints
{
    [ExcludeFromCodeCoverage]
    public static class EndpointsSimulador
    {
        public static WebApplication MapEndpointSim(this WebApplication app)
        {
            var grupo = app.MapGroup("/simulador");

            grupo.MapGet("/realizadas", () => "");

            return app;
        }
    }
}

