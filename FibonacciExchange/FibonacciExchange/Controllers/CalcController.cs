using System;
using System.Threading.Tasks;
using System.Web.Http;
using FibonacciExchangeCommon.Abstract;
using FibonacciExchangeCommon.Model;


namespace FibonacciExchangeApi.Controllers
{
    [RoutePrefix("api/calc")]
    public class CalcController : ApiController
    {
        private readonly ITaskManager _taskManager;

        public CalcController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
            _taskManager.Start();
        }


        [HttpPut]
        public async Task<IHttpActionResult> Put(Guid id)
        {
            var b = await Request.Content.ReadAsByteArrayAsync();
            if (b == null || b.Length == 0)
                return BadRequest();

            _taskManager.Process(new ProcessingElement(id, b));

            return Ok();
        }

    }
}
