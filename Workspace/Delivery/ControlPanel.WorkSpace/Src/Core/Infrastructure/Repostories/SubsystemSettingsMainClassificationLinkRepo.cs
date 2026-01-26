using _4EPlatForm.Infrastrucure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4EPlatForm.Infrastrucure.Repostories
{
    public class SubsystemSettingsMainClassificationLinkRepo:GRepository<SubsystemSettingsMainClassificationLink>, ISubsystemSettingsMainClassificationLinkRepo
    {
        public SubsystemSettingsMainClassificationLinkRepo(DBContext context) : base(context)
        {

        }
    }
}
