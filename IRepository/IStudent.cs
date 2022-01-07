using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter.IRepository
{
    public interface IStudent
    {
        public bool DeleteStudent()
        {
            throw new NotImplementedException();
        }

        public void AddStudent()
        {
            throw new NotImplementedException();    
        }

        public void UpdateStudent()
        {
            throw new NotImplementedException();
        }

        public void PayForStudy()
        {
            throw new NotImplementedException();    
        }

        public static bool CheckIfAlreadyExist(string path, string Contact) 
        {
            throw new NotImplementedException();
        }

        public static decimal SpecifyerOfGroupCost(string NameOfGroup)
        {
            throw new NotImplementedException();
        }

    }
}
