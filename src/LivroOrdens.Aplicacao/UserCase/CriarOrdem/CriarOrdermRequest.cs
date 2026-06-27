using LivroOrdens.Dominio.Enum;

namespace LivroOrdens.Aplicacao.UserCase.CriarOrdem
{
    public class CriarOrdermRequest
    {
        public string CIOrdId { get; set; }
        public string Simbolo { get; set; }
        public EAcaoOrdem Lado { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }

    }
}
