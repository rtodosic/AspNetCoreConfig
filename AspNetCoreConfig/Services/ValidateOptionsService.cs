using AspNetCoreConfig.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreConfig.Services
{
    public class ValidateOptionsService: IHostedService
    {
        private readonly ILogger<ValidateOptionsService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IOptions<SiteValidationOptions> _validateOptions;

        public ValidateOptionsService(
            ILogger<ValidateOptionsService> logger,
            IHostApplicationLifetime appLifetime,
            IOptions<SiteValidationOptions> validateOptions)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _validateOptions = validateOptions;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _ = _validateOptions.Value; // Trigger the validation
            }
            catch (OptionsValidationException ex)
            {
                _logger.LogError("One or more options validation checks failed");
                foreach (var failure in ex.Failures)
                    _logger.LogError(failure);

                _appLifetime.StopApplication(); // stop the app now !!!
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
