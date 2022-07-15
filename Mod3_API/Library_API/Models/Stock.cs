namespace Library_API.Models
{
    public class Stock
    {
        [Key]
        public int NucleoID { get; set; }
        [Key]
        public string ISBN { get; set; }

        public int Stocks { get; set; }

        public string Names { get; set; }
    }
}
