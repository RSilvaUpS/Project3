namespace Library_API.Data.Repository
{
    public class ReaderAction
    {
        private ReaderRepository _readerRepository;

        public ReaderAction(ReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }
        public List<Reader> List()
        {
            return _readerRepository.List().ToList();
        }
        public Reader Get(int nif)
        {
            return _readerRepository.Get(nif);
        }

        public string Create(int nif, string fname, string lname, string pwd)
        {
            return _readerRepository.Create(nif, fname,lname,pwd);
        }

        public Reader Edit(Reader reader)
        {
            return _readerRepository.Edit(reader);
        }

        public string Delete(int nif)
        {
            return _readerRepository.Delete(nif);
        }

        public void Activate(int nif)
        {
            _readerRepository.Activate(nif);
        }

        public void Suspend(int nif)
        {
            _readerRepository.Suspend(nif);
        }

        public void MakeAdmin(int nif)
        {
            _readerRepository.MakeAdmin(nif);
        }

        public List<int> Inactive()
        {
            return _readerRepository.Inactive().ToList();
        }
    }
}
