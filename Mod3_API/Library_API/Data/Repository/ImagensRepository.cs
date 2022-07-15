namespace Library_API.Data.Repository
{
    public class ImagensRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<ImagensRepository> _logger;
        public ImagensRepository(IConfiguration configuration, ILogger<ImagensRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::TransactionsRepository::");
        }

        public Imagens Get(int isbn)
        {
            SqlConnection cn = null;
            Imagens image = new Imagens();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("SELECT * FROM Imagens WHERE ISBN={0}", isbn);
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                
                    image.ISBN = (int)Convert.ToInt32(item["ISBN"]);
                if (item["CoverImage"] is DBNull) 
                {
                    image.CoverImage = null;
                }
                else
                {
                    image.CoverImage = (byte[])item["CoverImage"];
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                image = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return image;
        }

        public Imagens Edit(Imagens image)
        {
            //throw new NotImplementedException();
            SqlConnection cn = null;
            string commandText = "UPDATE Imagens SET CoverImage=@CoverImage WHERE ISBN=@ISBN"
                ;
            try
            {
                cn = BD.OpenBD(connectionString);
                SqlCommand command = new SqlCommand(commandText, cn);
                command.Parameters.Add("@ISBN", SqlDbType.Int);
                command.Parameters["@ISBN"].Value = image.ISBN;
                command.Parameters.Add("@CoverImage", SqlDbType.VarBinary);
                command.Parameters["@CoverImage"].Value = image.CoverImage;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Edit() :(");
                image = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }

            return image;
        }
    }
}
