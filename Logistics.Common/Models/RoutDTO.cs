using System;

namespace Logistics.Common.Models
{
    public class RoutDTO
    {
        public int Id { get; set; }
        public int StartCityId { get; set; }
        public string StartCityName { get; set; }
        public int EndCityId { get; set; }
        public string EndCityName { get; set; }
        public DateTime DateUpdated { get; set; }
        public decimal Distance { get; set; }
    }
}
