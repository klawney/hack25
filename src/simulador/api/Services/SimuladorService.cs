using Core;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Infra.Persistence;
using Infra.Persistence.Entities;
using Mensageria;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class SimuladorService : ISimuladorService
    {
        private readonly IProdutosQueryHandler _produtoQueryHandler;
        private readonly SimulacaoDbContext _dbContext;
        private readonly EventHubSimulacaoProducer _eventHubProducer;
        private readonly IMessageTrackerService _messageTracker;
        private readonly ILogger<SimuladorService> _logger;

        public SimuladorService(
            IProdutosQueryHandler produtoQueryHandler,
            SimulacaoDbContext dbContext,
            EventHubSimulacaoProducer eventHubProducer,
            IMessageTrackerService messageTracker,
            ILogger<SimuladorService> logger)
        {
            _produtoQueryHandler = produtoQueryHandler;
            _dbContext = dbContext;
            _eventHubProducer = eventHubProducer;
            _messageTracker = messageTracker;
            _logger = logger;
        }

        public async Task<SimulacaoResponseDto> RealizarSimulacao(SimulacaoRequestDto solicitacao)
        {
            // --- PASSO 1: LÓGICA DE NEGÓCIO ---
            Produto produto = await buscarProduto(solicitacao.Prazo, solicitacao.ValorDesejado);
            if (produto == null)
            {
                throw ProdutoException.ProdutoInexistente(solicitacao.ValorDesejado, solicitacao.Prazo);
            }
            List<ResultadoSimulacao> simulacoes = MontarSimulacoes(solicitacao, produto);
            
            // --- PASSO 2: PERSISTIR A SIMULAÇÃO ---
            var simulacaoParaSalvar = MapToEntity(produto, simulacoes);
            _dbContext.Simulacoes.Add(simulacaoParaSalvar);
            await _dbContext.SaveChangesAsync();

            long idSimulacaoPersistida = simulacaoParaSalvar.Id;

            // --- PASSO 3: ENVIAR PARA EVENT HUB COM PROTEÇÃO CONTRA DUPLICATAS ---
            var dtoParaRespostaEEventHub = ToDto(produto, simulacoes, idSimulacaoPersistida);

            if (_messageTracker.TryMarkAsProcessing(idSimulacaoPersistida))
            {
                try
                {
                    _logger.LogInformation("Enviando simulação {Id} para o Event Hub.", idSimulacaoPersistida);
                    await _eventHubProducer.EnviarSimulacaoAsync(dtoParaRespostaEEventHub);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Falha ao enviar simulação {Id} para o Event Hub. Ela será retentada se solicitada novamente.", idSimulacaoPersistida);
                    _messageTracker.MarkAsCompleted(idSimulacaoPersistida);
                }
            }
            else
            {
                _logger.LogInformation("Envio duplicado da simulação {Id} foi ignorado.", idSimulacaoPersistida);
            }

            // --- PASSO 4: RETORNAR A RESPOSTA PARA O USUÁRIO ---
            return dtoParaRespostaEEventHub;
        }

        private static SimulacaoResponseDto ToDto(Produto produto, List<ResultadoSimulacao> simulacoes, long idSimulacao)
        {
            return new SimulacaoResponseDto(
                IdSimulacao: idSimulacao,
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

        private SimulacaoData MapToEntity(Produto produto, List<ResultadoSimulacao> simulacoes)
        {
            return new SimulacaoData
            {
                IdSimulacao = 0, // O Id principal (chave primária) será gerado pelo banco. Este é um ID de negócio opcional.
                CodigoProduto = produto.CoProduto,
                DescricaoProduto = produto.NoProduto,
                TaxaJuros = produto.PcTaxaJuros,
                Resultados = simulacoes.Select(r => new ResultadoSimulacaoData
                {
                    Tipo = r.Tipo,
                    Parcelas = r.Parcelas.Select(p => new ParcelaData
                    {
                        Numero = p.Numero,
                        ValorAmortizacao = p.ValorAmortizacao,
                        ValorJuros = p.ValorJuros,
                        ValorPrestacao = p.ValorPrestacao
                    }).ToList()
                }).ToList()
            };
        }

        private List<ResultadoSimulacao> MontarSimulacoes(SimulacaoRequestDto solicitacao, Produto produto)
        {
            SistemaPrice price = new SistemaPrice();
            SistemaSac sac = new SistemaSac();
            List<ResultadoSimulacao> resultadosCalculos = new List<ResultadoSimulacao>();
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