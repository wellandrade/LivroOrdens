namespace LivroOrdens.Aplicacao.Shared
{
    public abstract class BaseResponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}
