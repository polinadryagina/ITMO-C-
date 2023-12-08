using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private static List<double> history = new List<double>();

        // GET: api/History
        [HttpGet]
        public IEnumerable<double> Get()
        {
            return history;
        }

        // GET: api/History/5
        [HttpGet("{id}", Name = "Get")]
        public double Get(int id)
        {
            return history.ElementAt(id);
        }

        // POST: api/History
        [HttpPost]
        public void Post([FromBody] double value)
        {
            history.Add(value);
        }

        // DELETE: api/History
        [HttpDelete]
        public void Delete()
        {
            history.Clear();
        }
    }
}