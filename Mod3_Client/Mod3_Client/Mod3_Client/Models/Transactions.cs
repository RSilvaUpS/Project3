namespace Mod3_Client.Models
{
    public class Transactions
    {
        public int TransactionID { get; set; }
        public string ISBN { get; set; }

        public int NIF { get; set; }

        public string? Levantamento { get; set; }
        public string? DataLimite { get; set; }
        public string? Entrega { get; set; }
    }
}
