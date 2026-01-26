using Domain.Models;

namespace _4EPlatform.Domain.Models
{
    public class SubsytemSettingsMainClassification : BaseEntity<Guid>
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        public State State { get; set; }
        public int? Squence { get; set; }
        public bool? IsHide { get; set; }
        public string? Description { get; set; }
        public string? DescriptionEn { get; set; }
        public string? Icon { get; set; }
        public Color? Color { get; set; }
        [ForeignKey("SubsytemMainLookup")]
        public Guid? SubsytemMainLookupId { get; set; }
        public SubsytemMainLookup SubsytemMainLookup { get; set; }

        public ICollection<SubsystemSettingsMainClassificationLink> SubsystemSettingsMainClassificationLinks { get; set; }
    }
}
