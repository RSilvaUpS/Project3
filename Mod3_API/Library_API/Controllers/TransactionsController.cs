using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        public TransactionsAction _transactionsAction;

        public TransactionsController(TransactionsAction transactionsAction)
        {
            _transactionsAction = transactionsAction;
        }

        [HttpGet]
        public List<Transactions> Index()
        {
            return _transactionsAction.List();
        }

        [HttpGet("{transactionId}")]
        public Transactions Get(int transactionId)
        {
            return _transactionsAction.Get(transactionId);
        }

        [HttpGet]
        [Route("Lend")]
        public string Create(int nif, string isbn, int nucleoId)
        {
            return _transactionsAction.Create(nif, isbn, nucleoId);
        }

        [HttpGet]
        [Route("Return")]
        public string ReturnBook(int transactionId, int nif)
        {
            return _transactionsAction.ReturnBook(transactionId, nif);
        }

        [HttpGet]
        [Route("FromNif")]
        public List<Transactions> GetFromNif(int nif)
        {
            return _transactionsAction.GetFromNif(nif);
        }


        [HttpDelete("{transactionId}")]
        public void Delete(int transactionId)
        {
            _transactionsAction.Delete(transactionId);
        }
    }
}
