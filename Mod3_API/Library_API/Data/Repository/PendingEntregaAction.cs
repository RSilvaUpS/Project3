namespace Library_API.Data.Repository
{
    public class PendingEntregaAction
    {
        private PendingEntregaRepository _pendingentregaRepository;

        public PendingEntregaAction(PendingEntregaRepository pendingentregaRepository)
        {
            _pendingentregaRepository = pendingentregaRepository;
        }
        public List<PendingEntrega> List()
        {
            return _pendingentregaRepository.List().ToList();
        }

        public List<PendingEntrega> Get(int nif)
        {
            return _pendingentregaRepository.Get(nif).ToList();
        }
    }
}
