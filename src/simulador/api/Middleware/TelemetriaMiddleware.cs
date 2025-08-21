using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace api.Middleware
{
    public class TelemetriaMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, int> _contagemRequisicoes = new();
        private static readonly ConcurrentDictionary<string, long> _tempoTotal = new();

        public TelemetriaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/simular"))
            {
                var stopwatch = Stopwatch.StartNew();
                await _next(context);
                stopwatch.Stop();

                _contagemRequisicoes.AddOrUpdate("simular", 1, (key, oldValue) => oldValue + 1);
                _tempoTotal.AddOrUpdate("simular", stopwatch.ElapsedMilliseconds, (key, oldValue) => oldValue + stopwatch.ElapsedMilliseconds);
            }
            else
            {
                await _next(context);
            }
        }

        public static int GetQtdeRequisicoes() => _contagemRequisicoes.TryGetValue("simular", out var val) ? val : 0;
        public static long GetTempoTotalMs() => _tempoTotal.TryGetValue("simular", out var val) ? val : 0;
    }
}
