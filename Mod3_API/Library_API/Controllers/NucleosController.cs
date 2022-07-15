using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NucleosController : ControllerBase
    {
        public NucleosAction _nucleosAction;

        public NucleosController(NucleosAction cellsAction)
        {
            _nucleosAction = cellsAction;
        }

        [HttpGet]
        public List<Nucleos> Index()
        {
            return _nucleosAction.List();
        }

        [HttpGet("{id}")]
        public Nucleos Get(int id)
        {
            return _nucleosAction.Get(id);
        }
    }
}
