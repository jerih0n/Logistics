using System;

namespace Logistics.Common.Models
{
    public class LogisticsCenterDTO
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
