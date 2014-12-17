using System;
using System.Configuration;
using System.IO;
using System.Linq;
using FibonacciExchangeCommon.Abstract;

namespace FibonacciExchangeCommon.Concrete
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILog _log;
        public ConfigurationService(ILog log)
        {
            _log = log;
        }

        public string GetValue(string key)
        {
            var val = ConfigurationManager.AppSettings[key];
            if (val == null)
                _log.WriteMessage(MessageSeverity.Warn, "Was not specified configuration parameter: " + key);
            return val;
        }

        public string[] GetArray(string key)
        {
            var val = ConfigurationManager.AppSettings[key];
            if (val == null)
            {
                _log.WriteMessage(MessageSeverity.Warn, "Was not specified configuration parameter: " + key);
                return null;
            }
            var separators = new[] { ',', ';' };

            var arr = val.Split(separators).Select(s => s.Trim()).ToArray();

            if (!arr.Any())
            {
                _log.WriteMessage(MessageSeverity.Warn,
                    string.Format("You must specify a parameter {0} as list of values ​​with a separators: {1}", key,
                        string.Join(" or ", separators)));
            }
            return arr;

        }

        public string GetConnectionString(string key)
        {
            var val = ConfigurationManager.ConnectionStrings[key];
            if (val != null)
                return val.ConnectionString;

            _log.WriteMessage(MessageSeverity.Warn, "Was not specified ConnectionString: " + key);
            return null;
        }


        #region prop
        

        public string AppName
        {
            get { return "FibonacciExchange.Api"; }
        }



        #region ApplicationDataFolder
        private string _applicationDataFolder;
        public string ApplicationDataFolder
        {
            get
            {
                return _applicationDataFolder ??
                       (_applicationDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                               AppName));
            }
        }
        #endregion ApplicationDataFolder

        #endregion prop
    }
}