using LivroOrdens.Dominio.Entidades;
using LivroOrdens.Dominio.Enum;
using LivroOrdens.Dominio.Expcetions;

namespace LivroOrdens.Tests.Dominio
{
    public class OrdemTest
    {
        [Fact]
        public void Construtor_DeveCriarOrdem_QuandoDadosForemValidos()
        {
            var ordem = new Ordem("VALE3", EAcaoOrdem.Comprar, 100, 35.50m, "ORD001");

            Assert.Equal("VALE3", ordem.Simbolo);
            Assert.Equal(EAcaoOrdem.Comprar, ordem.Lado);
            Assert.Equal(100, ordem.Quantidade);
            Assert.Equal(35.50m, ordem.Preco);
        }

        [Fact]
        public void Construtor_DeveLancarException_QuandoDadosForemInvalidos()
        {
            var exception = Assert.Throws<DominioException>(() =>
            {
                new Ordem("ITSA4", EAcaoOrdem.Comprar, 100, 35.50m, "ORD001");
            });

            Assert.Equal("O símbolo deve ser PETR4 ou VALE3.", exception.Message);
        }

        [Fact]
        public void Construtor_DeveLancarException_QuandoSimboloForVazio()
        {
            var exception = Assert.Throws<DominioException>(() =>
            {
                new Ordem("", EAcaoOrdem.Comprar, 100, 35.50m, "ORD001");
            });

            Assert.Equal("O símbolo não pode ser nulo ou vazio.", exception.Message);
        }

        [Fact]
        public void Construtor_DeveLancarException_QuandoQuantidadeMenorIgualZero()
        {
            var exception = Assert.Throws<DominioException>(() =>
            {
                new Ordem("VALE3", EAcaoOrdem.Comprar, 0, 35.50m, "ORD001");
            });

            Assert.Equal("A quantidade deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public void Construtor_DeveLancarException_QuandoQuantidadeMaiorQueCemMil()
        {
            var exception = Assert.Throws<DominioException>(() =>
            {
                new Ordem("VALE3", EAcaoOrdem.Comprar, 100001, 35.50m, "ORD001");
            });

            Assert.Equal("A quantidade deve ser menor que 100000.", exception.Message);
        }

        [Fact]
        public void Construtor_DeveLancarException_QuandoPrecoMenorIgualZero()
        {
            var exception = Assert.Throws<DominioException>(() =>
            {
                new Ordem("VALE3", EAcaoOrdem.Comprar, 20, 0, "ORD001");
            });

            Assert.Equal("O preço deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public void Construtor_DeveLancarException_QuandoPrecoMaiorQueMil()
        {
            var exception = Assert.Throws<DominioException>(() =>
            {
                new Ordem("VALE3", EAcaoOrdem.Comprar, 80, 2000.50m, "ORD001");
            });

            Assert.Equal("O preço deve ser menor que 1000.", exception.Message);
        }

    }

}
