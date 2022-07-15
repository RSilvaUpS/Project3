namespace Library_API.Data.Repository
{
    public class StockAction
    {
        private StockRepository _stockRepository;

        public StockAction(StockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public List<Stock> List()
        {
            return _stockRepository.List().ToList();
        }

        public List<Stock> StockBook(int isbn)
        {
            return _stockRepository.StockBook(isbn).ToList();
        }

        public Stock Edit(Stock stock)
        {
            return _stockRepository.Edit(stock);
        }

        public void Transfer(string isbn, int transferStock, int nucleoIn, int nucleoOut)
        {
            _stockRepository.Transfer(isbn, transferStock, nucleoIn, nucleoOut);
        }

    }
}
