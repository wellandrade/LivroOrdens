using LivroOrdens.Dominio.Enum;
using System.ComponentModel.DataAnnotations;

namespace LivroOrdens.Aplicacao.UserCase.EnviarCancelamentoOrdem
{
    public class EnviarCancelamentoRequest
    {
        [Required]
        public string CLOrId { get; set; } = "";

        public string OrigClOrdId { get; set; } = "";

        [Required]
        public string Simbolo { get; set; } = "";

        [EnumDataType(typeof(EAcaoOrdem))]
        public EAcaoOrdem Lado { get; set; }

    }
}
