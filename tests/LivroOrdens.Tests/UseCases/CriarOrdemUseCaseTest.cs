using FluentAssertions;
using LivroOrdens.Aplicacao.UserCase.CriarOrdem;
using LivroOrdens.Dominio.Enum;
using LivroOrdens.Infra.Repository;

namespace LivroOrdens.Tests.UseCases
{
    public class CriarOrdemUseCaseTest
    {
        [Fact]
        public async Task Executar_DeveCriarOrdem_QuandoRequestForValido()
        {
            var repository = new RepositoryOrdemInMemory();
            var useCase = new CriarOrdemUseCase(repository);

            var request = new CriarOrdermRequest
            {
                CIOrdId = "ORD001",
                Simbolo = "PETR4",
                Lado = EAcaoOrdem.Comprar,
                Quantidade = 100,
                Preco = 35.50m
            };

            var response = await useCase.Executar(request);

            response.Should().NotBeNull();
            response.Sucesso.Should().BeTrue();
            response.Mensagem.Should().Be("Ordem criada com sucesso.");
        }

        [Fact]
        public async Task Executar_DeveRetornarErro_QuandoSimboloForInvalido()
        {
            var repository = new RepositoryOrdemInMemory();
            var useCase = new CriarOrdemUseCase(repository);

            var request = new CriarOrdermRequest
            {
                CIOrdId = "ORD001",
                Simbolo = "TEE11",
                Lado = EAcaoOrdem.Comprar,
                Quantidade = 80,
                Preco = 70.50m
            };

            var response = await useCase.Executar(request);

            response.Should().NotBeNull();
            response.Sucesso.Should().BeFalse();
            response.Mensagem.Should().Be("O símbolo deve ser PETR4 ou VALE3.");
        }

        [Fact]
        public async Task Executar_DeveRetornarErro_QuandoQuantidadeForInvalida()
        {
            var repository = new RepositoryOrdemInMemory();
            var useCase = new CriarOrdemUseCase(repository);

            var request = new CriarOrdermRequest
            {
                CIOrdId = "ORD001",
                Simbolo = "VALE3",
                Lado = EAcaoOrdem.Comprar,
                Quantidade = 0,
                Preco = 75.70m
            };

            var response = await useCase.Executar(request);

            response.Should().NotBeNull();
            response.Sucesso.Should().BeFalse();
            response.Mensagem.Should().Be("A quantidade deve ser maior que zero.");
        }

        [Fact]
        public async Task Executar_DeveRetornarErro_QuandoPrecoForInvalida()
        {
            var repository = new RepositoryOrdemInMemory();
            var useCase = new CriarOrdemUseCase(repository);

            var request = new CriarOrdermRequest
            {
                CIOrdId = "ORD001",
                Simbolo = "VALE3",
                Lado = EAcaoOrdem.Comprar,
                Quantidade = 5,
                Preco = 0
            };

            var response = await useCase.Executar(request);

            response.Should().NotBeNull();
            response.Sucesso.Should().BeFalse();
            response.Mensagem.Should().Be("O preço deve ser maior que zero.");
        }

    }
}
