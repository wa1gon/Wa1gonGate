using System.Configuration;

namespace HamDevLib.Configuration
{
    public class GetConfiguationItem
    {
        public string GetPreferredLanguage(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
