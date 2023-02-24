using AccommodationAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccommodationAssistant.Persistence.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Navigation(e => e.Equipment).AutoInclude();
            builder.Navigation(e => e.Apartment).AutoInclude();
        }
    }
}
