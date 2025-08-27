using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISimuladorService
    {
        Task<SimulacaoResponseDto> RealizarSimulacao(SimulacaoRequestDto solicitacao);
        //Simulacao RealizarSimulacao();
    }
}