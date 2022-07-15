using System.Data;

namespace Library_API.Data.Repository
{
    public class TransactionsRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<TransactionsRepository> _logger;
        public TransactionsRepository(IConfiguration configuration, ILogger<TransactionsRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::TransactionsRepository::");
        }

        public string Create(int nif, string isbn, int nucleoId)
        {
            SqlConnection cn = null;
            string response = "";
            int returnvalue = 0;
            try
            {
                cn = BD.OpenBD(connectionString);
                SqlCommand cmd = new SqlCommand("LendBook", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nif", nif);
                cmd.Parameters.AddWithValue("@isbn", isbn);
                cmd.Parameters.AddWithValue("@nucleoID", nucleoId);
                SqlParameter ret = cmd.Parameters.Add("@rv", SqlDbType.Int);
                ret.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                returnvalue = (int)cmd.Parameters["@rv"].Value;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Create() :(");
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            switch (returnvalue)
            {
                case 1:
                    response = "Book loan sucessfull";
                    break;
                case 2:
                    response = "Sorry but this cell doesn't have enough copies to lend at the moment. Physical consultation only";
                    break;
                case 3:
                    response = "You've reached the maximum number of books in your possesion";
                    break;
                case 4:
                    response = "I'm sorry but you are suspended from renting books. Contact the closest library";
                    break;
                default:
                    response = "Book rental could not be performed. Contact support for further clarification";
                    break;
            }
            return response;
        }

        public void Delete(int transactionId)
        {
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("DELETE FROM Transactions WHERE TransactionID={0}", transactionId);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Delete() :(");

            }
            finally
            {
                BD.CloseBD(ref cn);
            }
        }

        public IEnumerable<Transactions> List()
        {
            SqlConnection cn = null;
            List<Transactions> transactions = new List<Transactions>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT * FROM Transactions";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Transactions transaction = new Transactions();
                    transaction.TransactionID = (int)Convert.ToInt64(item["TransactionID"]);
                    transaction.ISBN = item["ISBN"].ToString();
                    transaction.NIF = Convert.ToInt32(item["NIF"]);
                    DateTime dateTime = Convert.ToDateTime(item["Levantamento"]);
                    transaction.Levantamento = dateTime.ToShortDateString();
                    DateTime dateTimes = Convert.ToDateTime(item["DataLimite"]);
                    transaction.DataLimite = dateTimes.ToShortDateString();
                    if (item["Entrega"] is DBNull)
                    {
                        transaction.Entrega = "Not Returned";
                    }
                    else
                    {
                        DateTime dateTim = Convert.ToDateTime(item["Entrega"]);
                        transaction.Entrega = dateTim.ToShortDateString();
                    }
                    transactions.Add(transaction);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                transactions = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return transactions;
        }

        public string ReturnBook(int transactionId, int nif)
        {
            SqlConnection cn = null;
            string response = "";
            int returnvalue = 0;
            try
            {
                cn = BD.OpenBD(connectionString);
                SqlCommand cmd = new SqlCommand("BookReturn", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tID", transactionId);
                cmd.Parameters.AddWithValue("@nifc", nif);
                SqlParameter ret = cmd.Parameters.Add("@rv", SqlDbType.Int);
                ret.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                returnvalue = (int)cmd.Parameters["@rv"].Value;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at ReturnBook() :(");
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            switch (returnvalue)
            {
                case 1:
                    response = "Book returned sucessfully";
                    break;
                case 2:
                    response = "Book returned sucessfully But after limit date";
                    break;
                case 3:
                    response = "Book returned sucesfully but too many late strikes. You are now suspended";
                    break;
                case 4:
                    response = "NIF doesn't match with rental user";
                    break;
                case 5:
                    response = "Book already returned";
                    break;
                default:
                    response = "Couldn't return book. Contact support for further clarification";
                    break;
            }
            return response;
        }

        public Transactions Get(int transactionId)
        {
            SqlConnection cn = null;
            Transactions transaction = new Transactions();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("SELECT * FROM Transactions WHERE TransactionID={0}", transactionId);
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                transaction.TransactionID = (int)Convert.ToInt64(item["TransactionID"]);
                transaction.ISBN = item["ISBN"].ToString();
                transaction.NIF = Convert.ToInt32(item["NIF"]);
                DateTime dateTime = Convert.ToDateTime(item["Levantamento"]);
                transaction.Levantamento = dateTime.ToShortDateString();
                DateTime dateTimes = Convert.ToDateTime(item["DataLimite"]);
                transaction.DataLimite = dateTimes.ToShortDateString();
                if (item["Entrega"] is DBNull)
                {
                    transaction.Entrega = "Not Returned";
                }
                else
                {
                    DateTime dateTim = Convert.ToDateTime(item["Entrega"]);
                    transaction.Entrega = dateTim.ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                transaction = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return transaction;
        }

        public IEnumerable<Transactions> GetFromNif(int nif)
        {
            SqlConnection cn = null;
            List<Transactions> transactions = new List<Transactions>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT * FROM Transactions WHERE NIF=" + nif;
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Transactions transaction = new Transactions();
                    transaction.TransactionID = (int)Convert.ToInt64(item["TransactionID"]);
                    transaction.ISBN = item["ISBN"].ToString();
                    transaction.NIF = Convert.ToInt32(item["NIF"]);
                    DateTime dateTime = Convert.ToDateTime(item["Levantamento"]);
                    transaction.Levantamento = dateTime.ToShortDateString();
                    DateTime dateTimes = Convert.ToDateTime(item["DataLimite"]);
                    transaction.DataLimite = dateTimes.ToShortDateString();
                    if (item["Entrega"] is DBNull)
                    {
                        transaction.Entrega = "Not Returned";
                    }
                    else
                    {
                        DateTime dateTim = Convert.ToDateTime(item["Entrega"]);
                        transaction.Entrega = dateTim.ToShortDateString();
                    }
                    transactions.Add(transaction);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                transactions = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return transactions;
        }
    }
}
