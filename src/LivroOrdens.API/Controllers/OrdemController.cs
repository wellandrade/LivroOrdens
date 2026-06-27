using LivroOrdens.Aplicacao.UserCase.EnviarOrdem;
using Microsoft.AspNetCore.Mvc;

namespace LivroOrdens.API.Controllers
{
    [Route("api/ordens")]
    [ApiController]
    public class OrdemController : ControllerBase
    {
        private readonly EnviarOrdemUseCase _enviarOrdemUseCase;

        public OrdemController(EnviarOrdemUseCase enviarOrdemUseCase)
        {
            _enviarOrdemUseCase = enviarOrdemUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarOrdem([FromBody] EnviarOrdermRequest enviarOrdermRequest)
        {
            var response = await _enviarOrdemUseCase.Executar(enviarOrdermRequest);

            return response.Sucesso ? Ok(response) : BadRequest(response);
        }
    }
}
