using LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens;
using Microsoft.AspNetCore.Mvc;

namespace LivroOrdens.API.Controllers
{
    [Route("api/livro-ordens")]
    [ApiController]
    public class LivroOrdensController : ControllerBase
    {
        private readonly ObterLivrosOrdensUseCase _obterLivrosOrdensUseCase;

        public LivroOrdensController(ObterLivrosOrdensUseCase obterLivrosOrdensUseCase)
        {
            _obterLivrosOrdensUseCase = obterLivrosOrdensUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Obter()
        {
            var response = await _obterLivrosOrdensUseCase.Executar();

            return response.Sucesso ? Ok(response) : BadRequest(response);
        }
    }
}
