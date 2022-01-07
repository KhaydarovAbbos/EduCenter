using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter.IRepository
{
    internal interface IGroup
    {
        public static void AddGroup(GroupType group, string FirstName, string LastName, int Age)
        {
            throw new NotImplementedException();
        }

        public static void DeleteGroup(string GroupName)
        {
            throw new NotImplementedException();
        }

        public static bool CheckIfAlreadyExist(string path, GroupType group)
        {
            throw new NotImplementedException();
        }


    }
}
