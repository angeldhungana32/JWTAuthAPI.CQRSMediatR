using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace JWTAuthAPI.Application.Common.Behaviors
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
