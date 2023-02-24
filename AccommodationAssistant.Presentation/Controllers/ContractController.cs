using AccommodationAssistant.Application.Contracts.Interfaces;
using AccommodationAssistant.Application.Contracts.Models;
using AccommodationAssistant.Presentation.Contracts;
using AccommodationAssistant.Presentation.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace AccommodationAssistant.Presentation.Controllers
{
    public class ContractController : BaseApiController
    {
        private readonly IContractService _contractService;
        private readonly ContractMapper _contractMapper;

        public ContractController(IContractService contractService, ContractMapper contractMapper)
        {
            _contractService = contractService;
            _contractMapper = contractMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts()
        {
            var result = await _contractService.GetAll();
            var contracts = result.Select(_contractMapper.MapToContractResponse);

            return Ok(contracts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] ContractModel contract)
        {
            var result = await _contractService.CreateContract(contract);

            return Ok(_contractMapper.MapToContractResponse(result));
        }
    }
}
