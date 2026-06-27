using LivroOrdens.Dominio.Constantes;
using LivroOrdens.Dominio.Enum;
using LivroOrdens.Dominio.Expcetions;

namespace LivroOrdens.Dominio.Entidades
{
    public class Ordem
    {
        public Ordem(string simbolo, EAcaoOrdem lado, int quantidade, decimal preco, string cIOrdId)
        {
            Simbolo = simbolo;
            Lado = lado;
            Quantidade = quantidade;
            Preco = preco;
            CIOrdId = cIOrdId;
            Status = EStatusOrdem.Nova;
            CriadEm = DateTime.UtcNow;

            ValidarRegras();
        }

        public string Simbolo { get; }
        public EAcaoOrdem Lado { get; }
        public int Quantidade { get; }
        public decimal Preco { get; }
        public string CIOrdId { get; }
        public EStatusOrdem Status { get; private set; }
        public DateTime CriadEm { get; private set; }

        private void ValidarRegras()
        {
            if (string.IsNullOrWhiteSpace(Simbolo))
            {
                throw new DominioException("O símbolo não pode ser nulo ou vazio.");
            }

            if (!Simbolos.SimbolosPermitidos.Contains(Simbolo.ToUpperInvariant()))
            {
                throw new DominioException("O símbolo deve ser PETR4 ou VALE3.");
            }

            if (Quantidade <= 0)
            {
                throw new DominioException("A quantidade deve ser maior que zero.");
            }

            if (Quantidade >= 100000)
            {
                throw new DominioException("A quantidade deve ser menor que 100000.");
            }

            if (Preco <= 0)
            {
                throw new DominioException("O preço deve ser maior que zero.");
            }

            if (Preco >= 1000)
            {
                throw new DominioException("O preço deve ser menor que 1000.");
            }

            if (string.IsNullOrWhiteSpace(CIOrdId))
            {
                throw new DominioException("O CIOrdId não pode ser nulo ou vazio.");
            }
        }

        public void Cancelar()
        {
            if (Status == EStatusOrdem.Cancelada)
            {
                throw new DominioException("A ordem já está cancelada.");
            }

            SetStatus(EStatusOrdem.Cancelada);
        }

        private void SetStatus(EStatusOrdem statusOrdem)
        {
            Status = statusOrdem;
        }

    }
}
