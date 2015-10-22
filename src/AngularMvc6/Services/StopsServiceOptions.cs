using System.IO;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;

namespace AngularMvc6.Services
{
    public class StopsServiceOptions {
        private const char DelimiterDefault = ',';
        private const bool HasHeaderDefault = true;
        private const bool HasQuotesDefault = true;

        public string FilePath { get; set; }
        public char Delimiter { get; set; }
        public bool HasHeader { get; set; }
        public bool HasQuotes { get; set; }

        public StopsServiceOptions(IConfiguration config, IApplicationEnvironment appEnv)
        {
            FilePath = Path.Combine(appEnv.ApplicationBasePath,
                config.GetSection("DataSource:FilePath").Value);

            // optional parameters
            var delimiter = config.GetSection("DataSource:Delimiter").Value;
            var hasHeader = config.GetSection("DataSource:HasHeader").Value;
            var hasQuotes = config.GetSection("DataSource:HasQuotes").Value;

            Delimiter = delimiter?[0] ?? DelimiterDefault;
            HasHeader = hasHeader == null ? HasHeaderDefault : bool.Parse(hasHeader);
            HasQuotes = hasQuotes == null ? HasQuotesDefault : bool.Parse(hasQuotes);
        }
    }
}