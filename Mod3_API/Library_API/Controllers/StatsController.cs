using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        public StatsAction _statsAction;

        public StatsController(StatsAction statsAction)
        {
            _statsAction = statsAction;
        }

        [HttpGet]
        [Route("TopNucleo")]
        public Stats TopNucleo()
        {
            return _statsAction.TopNucleo();
        }

        [HttpGet]
        [Route("WorstNucleo")]
        public Stats WorstNucleo()
        {
            return _statsAction.WorstNucleo();
        }

        [HttpGet]
        [Route("TopBook")]
        public Stats TopBook()
        {
            return _statsAction.TopBook();
        }

        [HttpGet]
        [Route("TopGenre")]
        public Stats TopGenre()
        {
            return _statsAction.TopGenre();
        }

        [HttpGet]
        [Route("WorstGenre")]
        public Stats WorstGenre()
        {
            return _statsAction.WorstGenre();
        }

        [HttpGet]
        [Route("TopAuthor")]
        public Stats TopAuthor()
        {
            return _statsAction.TopAuthor();
        }
    }
}
