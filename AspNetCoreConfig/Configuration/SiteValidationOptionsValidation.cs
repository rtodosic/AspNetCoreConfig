using AspNetCoreConfig.Models;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfig.Configuration
{
    public class SiteValidationOptionsValidation : IValidateOptions<SiteValidationOptions>
    {
        private readonly MyValuesOptions _myValuesOptions;

        public SiteValidationOptionsValidation(IOptions<MyValuesOptions> myValuesOptions)
        {
            _myValuesOptions = myValuesOptions.Value;
        }

        public ValidateOptionsResult Validate(string name, SiteValidationOptions options)
        {
            if (_myValuesOptions.Value3 && string.IsNullOrEmpty(options.Title))
                return ValidateOptionsResult.Fail("'Title' is required when 'Value3' is true");
            return ValidateOptionsResult.Success;
        }
    }
}
