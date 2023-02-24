using AccommodationAssistant.Domain.Entities;
using AccommodationAssistant.Persistence.Context;
using AccommodationAssistant.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace AccommodationAssistant.Application.Contracts.Interfaces
{
    public class ApartmentRepository : BaseRepository<Apartment>, IApartmentRepository
    {
        public ApartmentRepository(DataContext dbContext, ILogger<ApartmentRepository> logger) : base(dbContext, logger)
        {
        }
    }
}
