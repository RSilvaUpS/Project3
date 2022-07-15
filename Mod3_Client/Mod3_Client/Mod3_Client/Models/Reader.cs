namespace Mod3_Client.Models
{
    public class Reader
    {
        [Key]
        public int NIF { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public bool Estado { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
