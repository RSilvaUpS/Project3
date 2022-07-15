namespace Library_API.Data.Repository
{
    public class ImagensAction
    {

        private ImagensRepository _imagensRepository;

        public ImagensAction(ImagensRepository imagensRepository)
        {
            _imagensRepository = imagensRepository;
        }

        public Imagens Get(int isbn)
        {
            return _imagensRepository.Get(isbn);
        }

        public Imagens Edit(Imagens imagem)
        {
            return _imagensRepository.Edit(imagem);
        }
    }
}
