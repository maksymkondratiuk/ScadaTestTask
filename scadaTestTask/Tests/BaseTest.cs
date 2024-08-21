using Microsoft.Extensions.Configuration;

namespace scadaTestTask.Tests
{
    public class BaseTest
    {
        protected readonly string BaseUrl;

        public BaseTest()
        {
            var config = LoadConfiguration();
            BaseUrl = config.GetSection("BaseUrl").Value;
        }

        private IConfigurationRoot LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
