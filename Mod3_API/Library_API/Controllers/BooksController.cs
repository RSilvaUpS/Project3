using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksAction _booksAction;

        public BooksController(BooksAction booksAction)
        {
            _booksAction = booksAction;
        }

        [HttpGet]
        public List<Books> Index()
        {
            return _booksAction.List();
        }

        [HttpGet("{isbn}")]
        public Books Get(int isbn)
        {
            return _booksAction.Get(isbn);
        }

        [HttpPost]
        [Route("NewBook")]
        public Books Create(Books Books)
        {
            return _booksAction.Create(Books);
        }

        [HttpPut]
        public Books Edit(Books Books)
        {
            return _booksAction.Edit(Books);
        }

        [HttpDelete("{isbn}")]
        public void Delete(int id)
        {
            _booksAction.Delete(id);
        }

        [HttpGet]
        [Route("Filtered")]
        public List<Books> Filtered(string search)
        {
            return _booksAction.Filtered(search);
        }
    }
}
