using AccommodationAssistant.Application.Contracts.Interfaces;
using AccommodationAssistant.Domain.Entities;
using AccommodationAssistant.Persistence.Context;
using Microsoft.Extensions.Logging;

namespace AccommodationAssistant.Persistence.Repositories
{
    public class ContractRepository : BaseRepository<Contract>, IContractRepository
    {
        public ContractRepository(DataContext dbContext, ILogger<ContractRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
