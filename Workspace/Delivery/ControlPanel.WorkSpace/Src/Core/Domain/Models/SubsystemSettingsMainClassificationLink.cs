using _4EPlatform.Domain.Models;
using Domain.Models;

public class SubsystemSettingsMainClassificationLink : BaseEntity<Guid>
{
    [ForeignKey("SubsytemSettings")]
    public Guid SubsystemSettingsId { get; set; }
    public SubsytemSettings SubsystemSettings { get; set; }

    [ForeignKey("SubsytemSettingsMainClassification")]
    public Guid? SubsytemMainId { get; set; }
    public SubsytemSettingsMainClassification SubsytemSettingsMainClassification { get; set; }
}
