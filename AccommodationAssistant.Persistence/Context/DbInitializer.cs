using AccommodationAssistant.Domain.Entities;

namespace AccommodationAssistant.Persistence.Context
{
    public static class DbInitializer
    {
        public static async Task Initialize(DataContext context)
        {
            if (context.Apartments.Any()
                && context.Equipments.Any())
            {
                return;
            }

            var equipments = new List<Equipment>
            {
                new Equipment
                {
                    Code = 13,
                    Name = "Table",
                    Area = 30
                },
                new Equipment
                {
                    Code = 134,
                    Name = "X-3000",
                    Area = 130
                },
                new Equipment
                {
                    Code = 1,
                    Name = "Sofa",
                    Area = 55
                }
            };

            var apartments = new List<Apartment>
            {
                new Apartment
                {
                    Code = 15,
                    Name = "Volins",
                    Area = 155
                },
                new Apartment
                {
                    Code = 345,
                    Name = "Krahov",
                    Area = 75
                },
                new Apartment
                {
                    Code = 12,
                    Name = "Msq",
                    Area = 230
                }
            };

            context.Equipments.AddRange(equipments);
            context.Apartments.AddRange(apartments);

            await context.SaveChangesAsync();
        }
    }
}
