using _4EPlatform.Domain.Models;

namespace Infrastructure.Repostories
{
    public class SubsytemSettingsMainClassificationRepository : GRepository<SubsytemSettingsMainClassification>, ISubsytemSettingsMainClassificationRepository
    {
        public SubsytemSettingsMainClassificationRepository(DBContext context) : base(context)
        {

        }
    }
}
