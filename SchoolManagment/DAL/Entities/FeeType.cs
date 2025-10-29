

namespace DAL.Entities
{
    public class FeeType
    {
        public int FeeTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? DefaultAmount { get; set; }
        public bool IsActive { get; set; }
    }

}
