using AccommodationAssistant.Application.Contracts.Models;
using AccommodationAssistant.Domain.Entities;

namespace AccommodationAssistant.Application.Contracts.Interfaces
{
    public interface IContractService
    {
        public Task<Contract> CreateContract(ContractModel contractModel);
        public Task<IList<Contract>> GetAll();
    }
}
