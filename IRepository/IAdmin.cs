using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter.IRepository
{
    internal interface IAdmin
    {
        public static bool IsMainAdmin(Adminstration admin)
        {
            throw new NotImplementedException();
        }

        public static string ReadPassword()
        {
            throw new NotSupportedException();
        }

        public static bool IsAdmin(Adminstration admin)
        {
            throw new NotImplementedException();
        }

        public static void AddAdmistrator(Adminstration admin, string role)
        {
            throw new NotImplementedException();
        }

        public static void DeleteAdmin()
        {
            throw new NotImplementedException();
        }

        public static void ShowAdmins()
        {
            throw new NotImplementedException();
        }

        public static byte[] HashThePassword(Adminstration admin)
        {
            throw new NotImplementedException();
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            throw new NotImplementedException();
        }

        public virtual bool CheckIfAlreadyExist(string path, Adminstration admin)
        {
            throw new NotImplementedException();
        }

    }
}
