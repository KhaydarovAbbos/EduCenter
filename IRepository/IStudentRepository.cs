using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter.IRepository
{
    public interface IStudentRepository
    {
        public bool DeleteStudent(string Contact);

        public void AddStudent(StudentType student);

        public void UpdateStudent(string Contact);

        public void PayForStudy(string Contact);

        public decimal SpecifyerOfGroupCost(string NameOfGroup);

    }
}
