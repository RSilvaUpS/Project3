using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingEntregaController : ControllerBase
    {
        public PendingEntregaAction _pendingentregaAction;

        public PendingEntregaController(PendingEntregaAction pendingentregaAction)
        {
            _pendingentregaAction = pendingentregaAction;
        }

        [HttpGet]
        public List<PendingEntrega> Index()
        {
            return _pendingentregaAction.List();
        }

        [HttpGet("{nif}")]
        public List<PendingEntrega> Get(int nif)
        {
            return _pendingentregaAction.Get(nif);
        }
    }
}
