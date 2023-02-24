using AccommodationAssistant.Domain.Entities;

namespace AccommodationAssistant.Presentation.Contracts
{
    public class ContractMapper
    {
        public ContractResponse MapToContractResponse(Contract contract)
        {
            return new ContractResponse
            {
                ApartmentName = contract.Apartment.Name,
                EquipmnetName = contract.Equipment.Name,
                AmountOfEquipment = contract.AmountOfEquipment
            };
        }
    }
}
