namespace Library_API.Data.Repository
{
    public class NucleosAction
    {
        private NucleosRepository _nucleosRepository;

        public NucleosAction(NucleosRepository nucleosRepository)
        {
            _nucleosRepository = nucleosRepository;
        }
        public List<Nucleos> List()
        {
            return _nucleosRepository.List().ToList();
        }
        public Models.Nucleos Get(int isbn)
        {
            return _nucleosRepository.Get(isbn);
        }
    }
}
