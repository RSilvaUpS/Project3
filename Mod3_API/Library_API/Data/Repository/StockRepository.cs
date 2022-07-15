namespace Library_API.Data.Repository
{
    public class StockRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<StockRepository> _logger;
        public StockRepository(IConfiguration configuration, ILogger<StockRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::StockRepository::");
        }

        public IEnumerable<Stock> List()
        {
            SqlConnection cn = null;
            List<Stock> stocks = new List<Stock>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT    Stock.NucleoID, Stock.ISBN, Stock.Stocks, Nucleos.Names FROM Stock INNER JOIN Nucleos ON Stock.NucleoID = Nucleos.NucleoID";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Stock stock = new Stock();
                    stock.NucleoID = Convert.ToInt32(item["NucleoID"]);
                    stock.ISBN = item["ISBN"].ToString();
                    stock.Stocks = Convert.ToInt32(item["Stocks"]);
                    stock.Names = item["Names"].ToString();
                    stocks.Add(stock);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                stocks = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stocks;
        }

        public List<Stock> StockBook(int isbn)
        {
            SqlConnection cn = null;
            List<Stock> stocks = new List<Stock>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT    Stock.NucleoID, Stock.ISBN, Stock.Stocks, Nucleos.Names FROM Stock INNER JOIN Nucleos ON Stock.NucleoID = Nucleos.NucleoID WHERE ISBN=" + isbn;
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Stock stock = new Stock();
                    stock.NucleoID = Convert.ToInt32(item["NucleoID"]);
                    stock.ISBN = item["ISBN"].ToString();
                    stock.Stocks = Convert.ToInt32(item["Stocks"]);
                    stock.Names = item["Names"].ToString();
                    stocks.Add(stock);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                stocks = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return stocks;
        }


        public Stock Edit(Stock stock)
        {
            //throw new NotImplementedException();
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("update Stock Set Stocks = {2} where ISBN = {0} AND NucleoID = {1}",
                                stock.ISBN, stock.NucleoID, stock.Stocks);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Edit() :(");
                stock = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }

            return stock;
        }
        public void Transfer(string isbn, int transferStock, int nucleoIn, int nucleoOut)
        {
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("exec StockTransfer @ISBN='{0}',@NucleoIn='{1}',@NucleoOut='{2}',@stock={3}",
                                isbn, nucleoIn, nucleoOut, transferStock);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Transfer() :(");
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
        }

    }
}
