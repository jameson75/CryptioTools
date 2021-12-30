using Microsoft.Extensions.Configuration;

namespace CipherPark.CryptioTools.Utility.Credentials
{
    public class ExchangeCredentialsManager
    {
        private const string ExchangeCredentialsSectionRoot = "ExchangeTools:Credentials";
        private readonly IConfiguration config = null;

        public ExchangeCredentialsManager(IConfiguration config)
        {
            this.config = config;
        }

        public ExchangeCredentials GetCredentials(ExchangeCredentialsStore store)
        {
            var credentials = new ExchangeCredentials();
            var sectionName = $"{ExchangeCredentialsSectionRoot}:{store}";
            var section = config.GetSection(sectionName);
            if (section != null)
                section.Bind(credentials);        
            return credentials;
        }
    }
}
