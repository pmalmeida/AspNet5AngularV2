using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace PA.Services.Configuration
{
    public class AppSettings : IConfigurationRoot
    {
        private IConfigurationRoot _Configuration;

        public AppSettings()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables();
            _Configuration = builder.Build();           
        }

        public string this[string key]
        {
            get
            {
                return _Configuration[key];
            }

            set
            {
                _Configuration[key] = value;
            }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return _Configuration.GetChildren();
        }

        public IChangeToken GetReloadToken()
        {
            return _Configuration.GetReloadToken();
        }

        public IConfigurationSection GetSection(string key)
        {
            return _Configuration.GetSection(key);
        }

        public void Reload()
        {
            _Configuration.Reload();
        }
    }
}
