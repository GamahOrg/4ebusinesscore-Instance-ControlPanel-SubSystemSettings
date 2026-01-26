

using Domain.Models;

namespace _4EPlatform.Domain.Models
{
    public class SSOSecuritySetting :BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        public SSOSecuritySettingType Type { get; set; }
        public ICollection<SubsytemSettings> SubsytemSettings { get; set; }

        public State State { get; set; }



    }
}
