namespace Library_API.Data.Repository
{
    public class BooksAction
    {
        private BooksRepository _booksRepository;

        public BooksAction(BooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        public List<Books> List()
        {
            return _booksRepository.List().ToList();
        }
        public Books Get(int isbn)
        {
            return _booksRepository.Get(isbn);
        }

        public Books Create(Books book)
        {
            return _booksRepository.Create(book);
        }

        public Books Edit(Books book)
        {
            return _booksRepository.Edit(book);
        }

        public void Delete(int isbn)
        {
            _booksRepository.Delete(isbn);
        }

        public List<Books> Filtered(string search)
        {
            return _booksRepository.Filtered(search).ToList();
        }
    }
}
