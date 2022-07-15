namespace Library_API.Models
{
    public class Reader
    {
        [Key]
        [Required]
        public int NIF { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public bool Estado { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
