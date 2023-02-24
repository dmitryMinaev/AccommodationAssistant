using AccommodationAssistant.Domain.Entities;
using AccommodationAssistant.Persistence.Context;
using AccommodationAssistant.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace AccommodationAssistant.Application.Contracts.Interfaces
{
    public class EquipmentRepository : BaseRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DataContext dbContext, ILogger<EquipmentRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
