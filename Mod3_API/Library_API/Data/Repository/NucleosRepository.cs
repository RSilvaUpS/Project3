namespace Library_API.Data.Repository
{
    public class NucleosRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<BooksRepository> _logger;
        public NucleosRepository(IConfiguration configuration, ILogger<BooksRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::BooksRepository::");
        }

        public IEnumerable<Nucleos> List()
        {
            SqlConnection cn = null;
            List<Nucleos> cells = new List<Nucleos>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT * FROM Nucleos";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Nucleos cell = new Nucleos();
                    cell.NucleoId = Convert.ToInt32(item["NucleoID"]);
                    cell.Names = item["Names"].ToString();
                    cells.Add(cell);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                cells = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return cells;
        }

        public Nucleos Get(int id)
        {
            SqlConnection cn = null;
            Nucleos cell = new Nucleos();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("SELECT * FROM Nucleos WHERE NucleoID={0}", id);
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                cell.NucleoId = Convert.ToInt32(item["Id"]);
                cell.Names = item["Name"].ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                cell = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return cell;
        }
    }
}
