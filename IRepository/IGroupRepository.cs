using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter.IRepository
{
    internal interface IGroupRepository
    {
        public void AddGroup(GroupType group);

        public void DeleteGroup(string GroupName);

    }
}
