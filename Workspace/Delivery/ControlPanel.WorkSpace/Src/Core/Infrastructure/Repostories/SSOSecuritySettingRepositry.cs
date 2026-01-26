using _4EPlatform.Domain.Models;
using _4EPlatForm.Infrastrucure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4EPlatForm.Infrastrucure.Repostories
{
    public class SSOSecuritySettingRepositry : GRepository<SSOSecuritySetting>, ISSOSecuritySettingRepositry
    {
        public SSOSecuritySettingRepositry(DBContext context) : base(context)
        { }
    }
}
