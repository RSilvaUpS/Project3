namespace Library_API.Data.Repository
{
    public class TransactionsAction
    {
        private TransactionsRepository _transactionsRepository;

        public TransactionsAction(TransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }
        public List<Transactions> List()
        {
            return _transactionsRepository.List().ToList();
        }
        public Transactions Get(int transactionId)
        {
            return _transactionsRepository.Get(transactionId);
        }

        public string Create(int nif, string isbn, int nucleoId)
        {
            return _transactionsRepository.Create(nif, isbn, nucleoId);
        }

        public string ReturnBook(int transactionId, int nif)
        {
            return _transactionsRepository.ReturnBook(transactionId, nif );
        }

        public void Delete(int transactionId)
        {
            _transactionsRepository.Delete(transactionId);
        }

        public List<Transactions> GetFromNif(int nif)
        {
            return _transactionsRepository.GetFromNif(nif).ToList();
        }
    }
}
