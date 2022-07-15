namespace Library_API.Data.Repository
{
    public class ReaderRepository
    {
        public IConfiguration Configuration { get; set; }
        public string connectionString;
        private readonly ILogger<ReaderRepository> _logger;
        public ReaderRepository(IConfiguration configuration, ILogger<ReaderRepository> logger)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;

            _logger.LogDebug("::ReaderRepository::");
        }

        public string Create(int nif, string fname, string lname, string pwd)
        {
            SqlConnection cn = null;
            string response = "";
            int returnvalue = 0;
            try
            {
                cn = BD.OpenBD(connectionString);
                SqlCommand cmd = new SqlCommand("NewReader", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nif", nif);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@pwd", pwd);
                SqlParameter ret = cmd.Parameters.Add("@rv", SqlDbType.Int);
                ret.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                returnvalue = (int)cmd.Parameters["@rv"].Value;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at CreateUser() :(");
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            switch (returnvalue)
            {
                case 1:
                    response = "User created. Thanks for registering with us";
                    break;
                case 2:
                    response = "NIF already exists";
                    break;
                default:
                    response = "Couldn't create user.";
                    break;
            }
            return response;
        }

        public string Delete(int nif)
        {
            SqlConnection cn = null;
            string response = "";
            int returnvalue = 0;
            try
            {
                cn = BD.OpenBD(connectionString);
                SqlCommand cmd = new SqlCommand("DeleteUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nif", nif);
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
                    response = "User deleted sucessfully";
                    break;
                case 2:
                    response = "User still has books in possesion";
                    break;
                default:
                    response = "System error. Contact support";
                    break;
            }
            return response;
        }

        public IEnumerable<Reader> List()
        {
            SqlConnection cn = null;
            List<Reader> readers = new List<Reader>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "SELECT * FROM Reader";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    Reader reader = new Reader();
                    reader.NIF = Convert.ToInt32(item["NIF"]); ;
                    reader.Nome = item["Nome"].ToString();
                    reader.Apelido = item["Apelido"].ToString();
                    reader.Estado = Convert.ToBoolean(item["Estado"]);
                    reader.Password = item["Password"].ToString();
                    reader.IsAdmin = Convert.ToBoolean(item["IsAdmin"]);
                    readers.Add(reader);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at List() :(");
                readers = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return readers;
        }

        public Reader Get(int nif)
        {
            SqlConnection cn = null;
            Reader reader = new Reader();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("SELECT * FROM Reader WHERE NIF={0}", nif);
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);

                Dictionary<string, object> item = lst[0];
                reader.NIF = Convert.ToInt32(item["NIF"]); ;
                reader.Nome = item["Nome"].ToString();
                reader.Apelido = item["Apelido"].ToString();
                reader.Estado = Convert.ToBoolean(item["Estado"]);
                reader.Password = item["Password"].ToString();
                reader.IsAdmin = Convert.ToBoolean(item["IsAdmin"]);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Get() :(");
                reader = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }
            return reader;
        }

        public Reader Edit(Reader reader)
        {
            //throw new NotImplementedException();
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("UPDATE Reader SET Nome='{1}', Apelido='{2}', Password='{3}', IsAdmin='{4}', Estado='{5}' WHERE NIF={0}",
                                reader.NIF, reader.Nome, reader.Apelido, reader.Password, reader.IsAdmin, reader.Estado);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Edit() :(");
                reader = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }

            return reader;
        }

        public void Activate(int nif)
        {
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("exec ReaderActivate @nif={0}", nif);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Activate() :(");

            }
            finally
            {
                BD.CloseBD(ref cn);
            }
        }

        public void Suspend(int nif)
        {
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("exec ReaderSuspend @nif={0}", nif);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Activate() :(");

            }
            finally
            {
                BD.CloseBD(ref cn);
            }
        }
        public void MakeAdmin(int nif)
        {
            SqlConnection cn = null;
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = String.Format("exec MakeAdmin @nif={0}", nif);
                BD.CmdExecute(cn, SQL);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Activate() :(");

            }
            finally
            {
                BD.CloseBD(ref cn);
            }
        }

        public IEnumerable<int> Inactive()
        {
            SqlConnection cn = null;
            List<int> inactive = new List<int>();
            try
            {
                cn = BD.OpenBD(connectionString);
                string SQL = "select NIF from Transactions intersect select NIF from Transactions where DATEDIFF(day, Levantamento, getdate() ) > 366 except select NIF from Transactions where DATEDIFF(day, Levantamento, getdate() ) < 366";
                List<Dictionary<string, object>> lst = BD.ToListDictionary(cn, SQL);
                foreach (Dictionary<string, object> item in lst)
                {
                    int ir;
                    ir = Convert.ToInt32(item["NIF"]); ;
                    inactive.Add(ir);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                _logger.LogError(ex, "Error at Edit() :(");
                inactive = null;
            }
            finally
            {
                BD.CloseBD(ref cn);
            }

            return inactive;
        }
    }
}
