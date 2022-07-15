using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagensController : ControllerBase
    {
        public ImagensAction _imagensAction;

        public ImagensController(ImagensAction imagensAction)
        {
            _imagensAction = imagensAction;
        }

        [HttpGet("{isbn}")]
        public Imagens Get(int isbn)
        {
            return _imagensAction.Get(isbn);
        }

        [HttpPut]
        public Imagens Edit(Imagens imagem)
        {
            return _imagensAction.Edit(imagem);
        }
    }
}
