namespace Library_API.Models
{
    public class PendingEntrega
    {
        public int TransactionID { get; set; }

        public string ISBN { get; set; }

        public int NIF { get; set; }

        public int NucleoId { get; set; }

        public string Names { get; set; }
        public string Levantamento { get; set; }
        public string DataLimite { get; set; }
        public string Entrega { get; set; }
        public int Estado { get; set; }
        public string Mensagem { get; set; }
    }
}
