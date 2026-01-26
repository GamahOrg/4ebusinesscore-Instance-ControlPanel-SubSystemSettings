
namespace Domain.Models
{
    public class LK_NoteTypes : BaseEntity<Guid>
    {
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        public State State { get; set; }
        public ICollection<SubsytemSettings> SubsytemSettings { get; set; }
    }
}
