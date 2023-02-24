using AccommodationAssistant.Application.Contracts.Interfaces;
using AccommodationAssistant.Application.Contracts.Models;
using AccommodationAssistant.Domain.Entities;
using AccommodationAssistant.Domain.Exceptions;

namespace AccommodationAssistant.Application.Contracts
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public ContractService(IContractRepository contractRepository,
                               IApartmentRepository apartmentRepository,
                               IEquipmentRepository equipmentRepository)
        {
            _contractRepository = contractRepository;
            _apartmentRepository = apartmentRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<Contract> CreateContract(ContractModel contractModel)
        {
            var equimpent = await _equipmentRepository.GetOneAsync(e => e.Code == contractModel.EquipmnetCode);
            if (equimpent is null)
            {
                throw new ContractException($"Failed to find equipment with code {contractModel.EquipmnetCode}");
            }

            var apartment = await _apartmentRepository.GetOneAsync(a => a.Code == contractModel.ApartmentCode);
            if (apartment is null)
            {
                throw new ContractException($"Failed to find apartment with code {contractModel.ApartmentCode}");
            }

            if (apartment.Area < equimpent.Area * contractModel.AmountOfEquipment)
            {
                throw new ContractException("There's too much equipment for an apartment");
            }

            var contract = new Contract
            {
                ApartmentId = apartment.Id,
                EquipmentId = equimpent.Id,
                AmountOfEquipment = contractModel.AmountOfEquipment,
            };

            contract = await _contractRepository.AddAsync(contract);
            contract.Equipment = equimpent;
            contract.Apartment = apartment;

            return contract;
        }

        public async Task<IList<Contract>> GetAll()
        {
            var contracts = await _contractRepository.GetAsync();

            return contracts;
        }
    }
}
