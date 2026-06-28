using LivroOrdens.Dominio.Enum;
using System.ComponentModel.DataAnnotations;

namespace LivroOrdens.Aplicacao.UserCase.EnviarOrdem
{
    public class EnviarOrdermRequest
    {
        [Required]
        public string CIOrdId { get; set; } = "";

        [Required]
        public string Simbolo { get; set; } = "";

        [EnumDataType(typeof(EAcaoOrdem))]
        public EAcaoOrdem Lado { get; set; }

        [Range(1, 100_000)]
        public int Quantidade { get; set; }

        [Range(0.01, 1000)]
        public decimal Preco { get; set; }

    }
}
