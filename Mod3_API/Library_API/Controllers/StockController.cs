using Microsoft.AspNetCore.Mvc;



namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public StockAction _stockAction;

        public StockController(StockAction stockAction)
        {
            _stockAction = stockAction;
        }

        [HttpGet]
        public List<Stock> Index()
        {
            return _stockAction.List();
        }

        [HttpGet]
        [Route("StockBook")]
        public List<Stock> StockBook(int isbn)
        {
            return _stockAction.StockBook(isbn);
        }

        [HttpPost]
        [Route("update")]
        public Stock Edit(Stock stock)
        {
            return _stockAction.Edit(stock);
        }

        [HttpPost]
        [Route("transfer")]
        public void Transfer(string isbn, int transferStock, int nucleoIn, int nucleoOut)
        {
            _stockAction.Transfer(isbn,transferStock, nucleoIn, nucleoOut);
        }
    }
}
