using Core.Dtos;
using Core.Entities;
using Core.Events;
using Core.Exceptions;
using Core.Interfaces;

// using MassTransit; // Removed dependency on MassTransit

namespace Core.Services
{
    public class SimuladorService : ISimuladorService
    {
        private readonly IProdutosQueryHandler _produtoQueryHandler;
        public SimuladorService(IProdutosQueryHandler produtoQueryHandler)
        {
            _produtoQueryHandler = produtoQueryHandler;
        }
        public async Task<SimulacaoResponseDto> RealizarSimulacao(SimulacaoRequestDto solicitacao)
        {
            Produto produto = await buscarProduto(solicitacao.Prazo, solicitacao.ValorDesejado);
            if (produto == null)
            {
                throw ProdutoException.ProdutoInexistente(solicitacao.ValorDesejado, solicitacao.Prazo);
            }
            List<ResultadoSimulacao> simulacoes = MontarSimulacoes(solicitacao, produto);
            return ToDto(produto, simulacoes);
        }
        private static SimulacaoResponseDto ToDto(Produto produto, List<ResultadoSimulacao> simulacoes)
        {
            return new SimulacaoResponseDto(
                IdSimulacao: 1, //gerar um id
                CodigoProduto: produto.CoProduto,
                DescricaoProduto: produto.NoProduto,
                TaxaJuros: produto.PcTaxaJuros,
                ResultadoSimulacao: simulacoes
                    .Select(r => new ResultadoSimulacaoDto(
                    Tipo: r.Tipo,
                    Parcelas: r.Parcelas.Select(p => new ParcelaDto(
                        Numero: p.Numero,
                        ValorAmortizacao: p.ValorAmortizacao,
                        ValorJuros: p.ValorJuros,
                        ValorPrestacao: p.ValorPrestacao
                    )).ToList()
                )).ToList()
            );
        }

        private List<ResultadoSimulacao> MontarSimulacoes(SimulacaoRequestDto solicitacao, Produto produto)
        {
            SistemaPrice price = new SistemaPrice();
            SistemaSac sac = new SistemaSac();
            List<ResultadoSimulacao> resultadosCalculos = [];
            resultadosCalculos.Add(price.SimularPorPrazoDesejado(solicitacao.Prazo, produto.PcTaxaJuros, solicitacao.ValorDesejado));
            resultadosCalculos.Add(sac.SimularPorPrazoDesejado(solicitacao.Prazo, produto.PcTaxaJuros, solicitacao.ValorDesejado));
            return resultadosCalculos;
        }

        private async Task<Produto> buscarProduto(int Prazo, decimal ValorDesejado)
        {
            var produtoCompativel = await _produtoQueryHandler.BuscaProdutoCompativel(Prazo, ValorDesejado);

            return produtoCompativel;
        }
        private async Task<List<Produto>> listarProdutos(int Prazo, decimal ValorDesejado)
        {
            var produtosCompativeis = await _produtoQueryHandler.ListarProdutosCompativeis(Prazo, ValorDesejado);
            if (produtosCompativeis == null || !produtosCompativeis.Any())
            {
                throw new Exception("Nenhum produto encontrado para os critérios fornecidos.");
            }

            return produtosCompativeis;
        }
    }
}