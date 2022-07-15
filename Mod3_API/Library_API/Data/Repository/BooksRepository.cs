namespace Library_API.Data.Repository
{
    public class BooksRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<BooksRepository> _logger;
        public BooksRepository(IConfiguration configuration, ILogger<BooksRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::BooksRepository::");
        }

        public Books Create(Books book)
        {
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                SqlCommand cmd = new SqlCommand("NewBook", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmd.Parameters.AddWithValue("@titulo", book.Title);
                cmd.Parameters.AddWithValue("@Editora", book.Publisher);
                cmd.Parameters.AddWithValue("@autor", book.Author);
                cmd.Parameters.AddWithValue("@genero", book.Genre);
                cmd.ExecuteNonQuery();
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
            return book;
        }

        public void Delete(int isbn)
        {
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("DELETE FROM Books WHERE ISBN={0}", isbn);
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

        public IEnumerable<Books> List()
        {
            SqlConnection cn = null;
            List<Books> books = new List<Books>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT * FROM Books";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Books book = new Books();
                    book.ISBN = (int)Convert.ToInt64(item["ISBN"]);
                    book.Title = item["Title"].ToString();
                    book.Publisher = item["Publisher"].ToString();
                    book.Author = item["Author"].ToString();
                    book.Genre = item["Genre"].ToString();
                    books.Add(book);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                books = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return books;
        }

        public Books Get(int isbn)
        {
            SqlConnection cn = null;
            Books book = new Books();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("SELECT * FROM Books WHERE ISBN={0}", isbn);
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                book.ISBN = (int)Convert.ToInt64(item["ISBN"]);
                book.Title = item["Title"].ToString();
                book.Publisher = item["Publisher"].ToString();
                book.Author = item["Author"].ToString();
                book.Genre = item["Genre"].ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                book = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return book;
        }

        public Books Edit(Books book)
        {
            //throw new NotImplementedException();
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("UPDATE Books SET Title='{1}', Publisher='{2}', Author='{3}', Genre='{4}' WHERE ISBN={0}",
                                book.ISBN, book.Title, book.Publisher, book.Author, book.Genre);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Edit() :(");
                book = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }

            return book;
        }

        public IEnumerable<Books> Filtered(string search)
        {
            SqlConnection cn = null;
            List<Books> books = new List<Books>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "select * from Books where Author like '%" + search + "%' or Title like '%" + search + "%' or ISBN like '%"+search+"%' or Genre like '%"+search+"%' or Publisher like '%"+search+"%'";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Books book = new Books();
                    book.ISBN = (int)Convert.ToInt64(item["ISBN"]);
                    book.Title = item["Title"].ToString();
                    book.Publisher = item["Publisher"].ToString();
                    book.Author = item["Author"].ToString();
                    book.Genre = item["Genre"].ToString();
                    books.Add(book);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                books = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return books;
        }
    }
}
