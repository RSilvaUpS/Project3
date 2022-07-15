namespace Library_API.Data.Repository
{
    public class StatsRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<StatsRepository> _logger;
        public StatsRepository(IConfiguration configuration, ILogger<StatsRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::StatsRepository::");
        }

        public Stats TopNucleo()
        {
            SqlConnection cn = null;
            Stats stats = new Stats();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("Select top(1) Transactions.NucleoID, count (Transactions.NucleoID) as Requisicoes  from Transactions INNER JOIN Books ON Transactions.ISBN = Books.ISBN group by Transactions.NucleoID order by Requisicoes desc");
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                stats.key = item["NucleoID"].ToString(); ;
                stats.value = Convert.ToInt32(item["Requisicoes"]);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                stats = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stats;
        }

        public Stats WorstNucleo()
        {
            SqlConnection cn = null;
            Stats stats = new Stats();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("Select top(1) Transactions.NucleoID, count (Transactions.NucleoID) as Requisicoes  from Transactions INNER JOIN Books ON Transactions.ISBN = Books.ISBN group by Transactions.NucleoID order by Requisicoes asc");
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                stats.key = item["NucleoID"].ToString(); 
                stats.value = Convert.ToInt32(item["Requisicoes"]);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                stats = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stats;
        }

        public Stats TopBook()
        {
            SqlConnection cn = null;
            Stats stats = new Stats();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("Select top (1) Books.ISBN, count (Books.ISBN) as Requisicoes  from Transactions INNER JOIN Books ON Transactions.ISBN = Books.ISBN group by Books.ISBN order by Requisicoes desc");
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                stats.key = item["ISBN"].ToString(); 
                stats.value = Convert.ToInt32(item["Requisicoes"]);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                stats = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stats;
        }

        public Stats TopGenre()
        {
            SqlConnection cn = null;
            Stats stats = new Stats();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("Select top (1) Books.Genre, count (Books.ISBN) as Requisicoes  from Transactions INNER JOIN Books ON Transactions.ISBN = Books.ISBN group by Books.Genre order by Requisicoes desc");
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                stats.key = item["Genre"].ToString(); 
                stats.value = Convert.ToInt32(item["Requisicoes"]);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                stats = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stats;
        }

        public Stats WorstGenre()
        {
            SqlConnection cn = null;
            Stats stats = new Stats();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("Select top (1) Books.Genre, count (Books.ISBN) as Requisicoes  from Transactions INNER JOIN Books ON Transactions.ISBN = Books.ISBN group by Books.Genre order by Requisicoes asc");
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                stats.key = item["Genre"].ToString(); 
                stats.value = Convert.ToInt32(item["Requisicoes"]);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                stats = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stats;
        }

        public Stats TopAuthor()
        {
            SqlConnection cn = null;
            Stats stats = new Stats();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("Select top (1) Books.Author, count (Books.ISBN) as Requisicoes  from Transactions INNER JOIN Books ON Transactions.ISBN = Books.ISBN group by Books.Author order by Requisicoes desc");
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                stats.key = item["Author"].ToString(); 
                stats.value = Convert.ToInt32(item["Requisicoes"]);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                stats = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stats;
        }
    }
}
