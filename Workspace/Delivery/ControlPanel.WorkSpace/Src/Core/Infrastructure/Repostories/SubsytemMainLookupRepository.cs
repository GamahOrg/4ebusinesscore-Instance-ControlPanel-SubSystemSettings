using _4EPlatform.Domain.Models;

namespace Infrastructure.Repostories
{
    public class SubsytemMainLookupRepository : GRepository<SubsytemMainLookup>, ISubsytemMainLookupRepository
    {
        public SubsytemMainLookupRepository(DBContext context) : base(context)
        {

        }
    }
}
