using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        public ReaderAction _readerAction;

        public ReaderController(ReaderAction readerAction)
        {
            _readerAction = readerAction;
        }

        [HttpGet]
        public List<Reader> Index()
        {
            return _readerAction.List();
        }

        [HttpGet("{nif}")]
        public Reader Get(int nif)
        {
            return _readerAction.Get(nif);
        }

        [HttpGet]
        [Route("newUser")]
        public string Create(int nif, string fname, string lname, string pwd)
        {
            return _readerAction.Create(nif, fname, lname, pwd);
        }

        [HttpPut]
        public Reader Edit(Reader reader)
        {
            return _readerAction.Edit(reader);
        }

        [HttpDelete("{nif}")]
        public string Delete(int nif)
        {
            return _readerAction.Delete(nif);
        }

        [HttpPost]
        [Route("activate")]
        public void Activate(int nif)
        {
            _readerAction.Activate(nif);
        }

        [HttpPost]
        [Route("suspend")]
        public void Suspend(int nif)
        {
            _readerAction.Suspend(nif);
        }

        [HttpPost]
        [Route("makeAdmin")]
        public void MakeAdmin(int nif)
        {
            _readerAction.MakeAdmin(nif);
        }

        [HttpGet]
        [Route("Inactive")]
        public List<int> Inactive()
        {
            return _readerAction.Inactive();
        }
    }
}
