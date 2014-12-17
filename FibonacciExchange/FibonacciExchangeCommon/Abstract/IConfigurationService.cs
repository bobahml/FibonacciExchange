namespace FibonacciExchangeCommon.Abstract
{
    public interface IConfigurationService
    {
        string GetValue(string key);
        string[] GetArray(string key);
        string GetConnectionString(string key);
        string ApplicationDataFolder { get; }
    }
}
