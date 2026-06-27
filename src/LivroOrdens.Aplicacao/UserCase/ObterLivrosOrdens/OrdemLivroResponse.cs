using LivroOrdens.Dominio.Enum;

namespace LivroOrdens.Aplicacao.UserCase.ObterLivrosOrdens
{
    public class OrdemLivroResponse
    {
        public string ClOrIdid { get; set; }
        public string Simbolo { get; set; }
        public EAcaoOrdem Lado { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public DateTime CriadaEm { get; set; }
    }
}
