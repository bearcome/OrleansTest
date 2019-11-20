using KWKY.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NLog;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KWKY.WebClient.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private ILogger _logger;
        private IClusterClient _clusterClient;

        public ValuesController (IClusterClient clusterClient)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _clusterClient = clusterClient;
        }
        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        //[ResponseCache]
        public ActionResult<IEnumerable<string>> Get ()
        {
            return new JsonResult(ResponseModel.BadRequest);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get (int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put (int id, [FromBody][BindRequired] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<int> Delete (int id)
        {
            await Task.CompletedTask;
            return id*100;
        }
    }
}
