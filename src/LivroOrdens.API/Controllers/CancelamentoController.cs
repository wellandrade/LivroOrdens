using LivroOrdens.Aplicacao.UserCase.CancelarOrdem;
using LivroOrdens.Aplicacao.UserCase.EnviarCancelamentoOrdem;
using Microsoft.AspNetCore.Mvc;

namespace LivroOrdens.API.Controllers
{
    [Route("api/cancelamento")]
    [ApiController]
    public class CancelamentoController : ControllerBase
    {
        private readonly EnviarCancelamentoUseCase _enviarCancelamentoUseCase;

        public CancelamentoController(EnviarCancelamentoUseCase enviarCancelamentoUseCase)
        {
            _enviarCancelamentoUseCase = enviarCancelamentoUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CancelarOrdem([FromBody] EnviarCancelamentoRequest enviarCancelamentoRequest)
        {
            var response = await _enviarCancelamentoUseCase.Executar(enviarCancelamentoRequest);

            return response.Sucesso ? Ok(response) : BadRequest(response);
        }
    }
}
