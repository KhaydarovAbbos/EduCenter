using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter.IRepository
{
    internal interface IAdminRepository
    {
        public bool IsMainAdmin(Adminstration admin);

        public string ReadPassword();

        public bool IsAdmin(Adminstration admin);

        public void AddAdmistrator(Adminstration admin, string role);
        public void DeleteAdmin();

        public void ShowAdmins();

        public byte[] HashThePassword(Adminstration admin);

        public bool CheckIfAlreadyExist(string path, Adminstration admin);
    }
}
