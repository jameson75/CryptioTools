using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace CipherPark.ExchangeTools.Utility.Credentials
{
    public class ExchangeCredentialsManager
    {
        private const string ExchangeCredentialsSectionName = "ExchangeTools:ExchangeCredentials";
        private readonly IConfiguration config = null;

        public ExchangeCredentialsManager(IConfiguration config)
        {
            this.config = config;
        }

        public ExchangeCredentials GetCredentials()
        {
            var credentials = new ExchangeCredentials();
            var section = config.GetSection(ExchangeCredentialsSectionName);
            if (section != null)
                section.Bind(credentials);        
            return credentials;
        }
    }
}
