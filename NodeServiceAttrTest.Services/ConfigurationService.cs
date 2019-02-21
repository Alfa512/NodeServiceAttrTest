using System;
using Microsoft.Extensions.Configuration;
using NodeServiceAttrTest.Contracts;

namespace NodeServiceAttrTest.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString
        {
            get { return _configuration.GetValue<string>("ConnectionString"); }
        }
    }
}
