using System.Configuration;

namespace HamDotNetToolkit.Configuration
{
    public class GetConfiguationItem
    {
        public string GetPreferredLanguage(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
