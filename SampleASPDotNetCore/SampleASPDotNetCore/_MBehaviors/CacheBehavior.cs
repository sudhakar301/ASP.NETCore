using MediatR;
using Microsoft.Extensions.Caching.Memory;
using SampleASPDotNetCore._MCaching;

namespace SampleASPDotNetCore._MBehaviors
{
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheable
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheBehavior<TRequest, TResponse>> _logger;

        public CacheBehavior(ILogger<CacheBehavior<TRequest, TResponse>> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Request} is configured for caching", request.GetType());
            TResponse cacheResponse;
            if (_cache.TryGetValue(request.CacheKey, out cacheResponse!))
            {
                _logger.LogInformation("Returning cached value for {Request}", request.GetType());
                return cacheResponse;
            }
            _logger.LogInformation("{Request} Cache key: {key} is not inside the cache, executing request", request.GetType(),request.CacheKey);
            cacheResponse = await next();
            _cache.Set(request.CacheKey, cacheResponse);

            return cacheResponse;
        }
    }
}
