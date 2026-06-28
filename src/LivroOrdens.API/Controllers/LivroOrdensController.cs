using LivroOrdens.Aplicacao.Interfaces.Fix.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LivroOrdens.API.Controllers
{
    [Route("api/livro-ordens")]
    [ApiController]
    public class LivroOrdensController : ControllerBase
    {
        private readonly IFixClienteService _fixClienteService;

        public LivroOrdensController(IFixClienteService fixClienteService)
        {
            _fixClienteService = fixClienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Obter()
        {
            var response = await _fixClienteService.ObterLivroOrdens();

            return response.Sucesso ? Ok(response) : BadRequest(response);
        }
    }
}
