using LivroOrdens.Dominio.Enum;

namespace LivroOrdens.Aplicacao.UserCase.CancelarOrdem
{
    public class CancelarOrdemRequest
    {
        public string ClOrdId { get; set; } = "";
        public string OrigClOrdId { get; set; } = "";
        public string Simbolo { get; set; } = "";
        public EAcaoOrdem Lado { get; set; }
    }
}
