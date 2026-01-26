using Domain.Models;

namespace _4EPlatform.Domain.Models
{
    public class SubsytemMainLookup : BaseEntity<Guid>
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        public State State { get; set; }

        public ICollection<SubsytemSettings> SubsytemSettings { get; set; }
        public ICollection<SubsytemSettingsMainClassification> SubsytemSettingsMainClassifications { get; set; }
    }
}
