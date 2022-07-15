namespace Library_API.Data.Repository
{
    public class StatsAction
    {
        private StatsRepository _statsRepository;

        public StatsAction(StatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public Stats TopNucleo()
        {
            return _statsRepository.TopNucleo();
        }

        public Stats WorstNucleo()
        {
            return _statsRepository.TopNucleo();
        }

        public Stats TopBook()
        {
            return _statsRepository.TopBook();
        }
        public Stats TopGenre()
        {
            return _statsRepository.TopGenre();
        }

        public Stats WorstGenre()
        {
            return _statsRepository.WorstGenre();
        }

        public Stats TopAuthor()
        {
            return _statsRepository.TopAuthor();
        }
    }
}
