using System.ComponentModel.DataAnnotations;

namespace Library_API.Models
{
    public class Books
    {
        [Key]
        public int ISBN { get; set; }

        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
	
    }
}
