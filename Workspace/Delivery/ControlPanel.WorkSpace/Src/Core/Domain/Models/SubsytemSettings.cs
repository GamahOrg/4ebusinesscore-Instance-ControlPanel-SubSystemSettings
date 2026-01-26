

using _4EPlatform.Domain.Models;

namespace Domain.Models
{
    public class SubsytemSettings : BaseEntity<Guid>
    {
        [Required]
        public string NoteNameAr { get; set; }
        [Required]
        public string NoteNameEn { get; set; }

        public long? WorkSpaceKey { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public string? DescriptionEn { get; set; }
        public string Icon { get; set; }
        public Color Color { get; set; }
        public State State { get; set; }
        public bool IsActive { get; set; }
        public DateTime VisitDate { get; set; }
        [ForeignKey("LKNoteTypes")]
        public Guid? NoteTypeId { get; set; }
        [ForeignKey("Parent")]
        public Guid? ParentId { get; set; }
        public bool? IsVisiable { get; set; }
        public bool? IsExternal { get; set; }
        public bool? IsExternalComponent { get; set; }

        public bool? IsSinglSignOn { get; set; }
        public string? IdentityProvider { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public bool? SubsystemIsVisiable { get; set; }
        public int? Squence { get; set; }

        public int? SubsystemmainSequnce { get; set; }
        public bool? IsUpdated { get; set; }

        public Guid? OrganizationHierarchyId { get; set; }

        public SubsytemSettings Parent { get; set; }

        [InverseProperty("Parent")]
        public ICollection<SubsytemSettings> Children { get; set; }
        public LK_NoteTypes LKNoteTypes { get; set; }

          public ICollection<SubsystemSettingsMainClassificationLink> SubsystemSettingsMainClassificationLinks { get; set; }

        [ForeignKey("SubsytemMainLookup")]
        public Guid? SubsytemMainLookupId { get; set; }
        public SubsytemMainLookup SubsytemMainLookup { get; set; }


        [ForeignKey("SSOSecuritySetting")]
        public Guid? SSOSecuritySettingId { get; set; }
        public SSOSecuritySetting SSOSecuritySetting { get; set; }

        public bool? IsIntegerated { get; set; }
        public string? EndPoint { get; set; }
    }
}
