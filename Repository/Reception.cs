using ConsoleTables;
using EducationalCenter.IRepository;
using EducationalCenter.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EducationalCenter
{
    internal class Reception:IReceptionRepository
    {

        #region ShowGroupsToNewStudent
        public string ShowGroupsToNewStudent()
        {
            int succesChecker = 0;
            while (true) {

                string json = File.ReadAllText(Constants.GroupsJsonPath);
                IList<GroupType> GroupstList = JsonConvert.DeserializeObject<List<GroupType>>(json);

                var ress = (GroupstList.Select(gr => new GroupType() { Name = gr.Name, Cost = gr.Cost })).ToList();

                foreach (var item in ress)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item.Name.Capitalize());
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("\nKurslardan birini kiriting: ");
                string choosenGroup = Console.ReadLine();
                choosenGroup.ToLower();

                foreach (var iteam in ress)
                {
                    if (iteam.Name != "")
                    {
                        if (iteam.Name == choosenGroup)
                        {
                            succesChecker++;
                            return choosenGroup;
                        }
                    }
                }
                if (succesChecker == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Kurs topilmadi");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }//Done
        #endregion

        #region ShowGroups
        public void ShowGroups()
        {
            string json = File.ReadAllText(Constants.StudentsJsonPath);
            IList<GroupType> studentsList = JsonConvert.DeserializeObject<List<GroupType>>(json);

            string json1 = File.ReadAllText(Constants.GroupsJsonPath);
            IList<GroupType> groupsList = JsonConvert.DeserializeObject<List<GroupType>>(json1);

            Console.WriteLine($"Bizning o'quv markazimizda {studentsList.Count()} ta o'quvchi va {groupsList.Count()} ta kurs mavjud");
            Console.WriteLine("Quyida har bir kurs bilan yaqindan tanishishingiz mumkun: ");

            var ress = (groupsList.Select(gr => new GroupType() { Name = gr.Name, Cost = gr.Cost })).ToList();


            var table = new ConsoleTable("Kurs nomi", "Kurs narxi");
            foreach (var group in ress)
            {
                if (group.Name != "")
                {
                    table.AddRow(group.Name.Capitalize(), group.Cost);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            table.Write();
            Console.ForegroundColor = ConsoleColor.White;
        }//Done
        #endregion
    }
} 