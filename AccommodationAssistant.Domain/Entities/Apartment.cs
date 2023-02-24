using AccommodationAssistant.Domain.Common;

namespace AccommodationAssistant.Domain.Entities
{
    public class Apartment : IBaseEntity
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
    }
}
