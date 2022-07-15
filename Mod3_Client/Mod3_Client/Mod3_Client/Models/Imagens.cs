namespace Mod3_Client.Models
{
    public class Imagens
    {
        public string ISBN { get; set; }

        public byte[]? CoverImage { get; set; }

        public FileUpload? fileUpload { get; set; }
    }
}
