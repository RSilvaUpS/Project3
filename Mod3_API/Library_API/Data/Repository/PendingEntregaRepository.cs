namespace Library_API.Data.Repository
{
    public class PendingEntregaRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<BooksRepository> _logger;
        public PendingEntregaRepository(IConfiguration configuration, ILogger<BooksRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::BooksRepository::");
        }
        public IEnumerable<PendingEntrega> List()
        {
            SqlConnection cn = null;
            List<PendingEntrega> pentregas = new List<PendingEntrega>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT * FROM PendingEntrega";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    PendingEntrega pentrega = new PendingEntrega();
                    pentrega.TransactionID = Convert.ToInt32(item["TransactionID"]);
                    pentrega.ISBN = item["ISBN"].ToString();
                    pentrega.NIF = Convert.ToInt32(item["NIF"]);
                    pentrega.NucleoId= Convert.ToInt32(item["NucleoID"]);
                    pentrega.Names = item["Names"].ToString();
                    DateTime dateTime = Convert.ToDateTime(item["Levantamento"]);
                    pentrega.Levantamento = dateTime.ToShortDateString();
                    DateTime dateTimes = Convert.ToDateTime(item["DataLimite"]);
                    pentrega.DataLimite = dateTimes.ToShortDateString();
                    pentrega.Estado = Convert.ToInt32(item["Estado"]);
                    pentrega.Mensagem = item["Mensagem"].ToString();
                    if (item["Entrega"] is DBNull)
                    {
                        pentrega.Entrega = "Not Returned";
                    }
                    else
                    {
                        DateTime dateTim = Convert.ToDateTime(item["Entrega"]);
                        pentrega.Entrega = dateTim.ToShortDateString();
                    }
                    pentregas.Add(pentrega);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                pentregas = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return pentregas;
        }

        public IEnumerable<PendingEntrega> Get(int nif)
        {
            SqlConnection cn = null;
            List<PendingEntrega> pentregas = new List<PendingEntrega>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT * FROM PendingEntrega WHERE NIF=" + nif;
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    PendingEntrega pentrega = new PendingEntrega();
                    pentrega.TransactionID = Convert.ToInt32(item["TransactionID"]);
                    pentrega.ISBN = item["ISBN"].ToString();
                    pentrega.NIF = Convert.ToInt32(item["NIF"]);
                    pentrega.NucleoId = Convert.ToInt32(item["NucleoID"]);
                    pentrega.Names = item["Names"].ToString();
                    DateTime dateTime = Convert.ToDateTime(item["Levantamento"]);
                    pentrega.Levantamento = dateTime.ToShortDateString();
                    DateTime dateTimes = Convert.ToDateTime(item["DataLimite"]);
                    pentrega.DataLimite = dateTimes.ToShortDateString();
                    pentrega.Estado = Convert.ToInt32(item["Estado"]);
                    pentrega.Mensagem = item["Mensagem"].ToString();
                    if (item["Entrega"] is DBNull)
                    {
                        pentrega.Entrega = "Not Returned";
                    }
                    else
                    {
                        DateTime dateTim = Convert.ToDateTime(item["Entrega"]);
                        pentrega.Entrega = dateTim.ToShortDateString();
                    }
                    pentregas.Add(pentrega);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                pentregas = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return pentregas;
        }
    }
}
