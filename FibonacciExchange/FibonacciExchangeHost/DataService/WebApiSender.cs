using System.Threading.Tasks;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Model;
using RestSharp;

namespace FibonacciExchangeHost.DataService
{
    public class WebApiSender : IResultSender
    {
        private readonly string _baseApiAddress;

        public WebApiSender(IConfigurationService configurationService)
        {
            _baseApiAddress = configurationService.GetValue("BaseApiAddress");
        }

        public void Send(ProcessingElement element)
        {
            var client = new RestClient(_baseApiAddress);
            var request = new RestRequest("api/calc/{id}", Method.PUT);
            request.AddUrlSegment("id", element.TaskId.ToString());
            request.AddParameter("FibData", element.Data, ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }
        }
    }
}