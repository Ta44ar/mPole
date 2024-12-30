namespace mPole.Utils.Helpers
{
    public class LocalizationHelper
    {
        private readonly IConfiguration _configuration;

        public LocalizationHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RequestLocalizationOptions GetLocalizationOptions()
        {
            var cultures = _configuration.GetSection("Cultures")
                .GetChildren().ToDictionary(x => x.Key, x => x.Value);

            var supportedCultures = cultures.Keys.ToArray();

            var localizationOptions = new RequestLocalizationOptions()
                                        .SetDefaultCulture(supportedCultures[0])
                                        .AddSupportedCultures(supportedCultures)
                                        .AddSupportedUICultures(supportedCultures);

            return localizationOptions;
        }
    }
}
