using AccommodationAssistant.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccommodationAssistant.Domain.Entities
{
    public class Contract : IBaseEntity
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public int AmountOfEquipment { get; set; }
    }
}
