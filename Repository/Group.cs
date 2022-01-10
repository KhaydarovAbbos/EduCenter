using EducationalCenter.IRepository;
using EducationalCenter.Models;
using EducationalCenter.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;

namespace EducationalCenter
{
    internal class GroupType : ModelOfGroup, IGroupRepository
    {
        #region AddGroup
        public void AddGroup(GroupType group)
        {
            bool result = false;
            string json = File.ReadAllText(Constants.GroupsJsonPath);
            IList<GroupType> GroupstList = JsonConvert.DeserializeObject<List<GroupType>>(json);

            var ress = (GroupstList.Select(gr => new GroupType() { Name = gr.Name, Cost = gr.Cost})).ToList();
            
            foreach (var item in ress)
            {
                if (item.Name == group.Name)
                {
                    result = true;
                }
            }

            if (result == false)
            {
                GroupstList.Add(new GroupType { Name = group.Name, Cost = group.Cost});

                string res = JsonConvert.SerializeObject(GroupstList);
                File.WriteAllText(Constants.GroupsJsonPath, res);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nKurs muaffaqiyatli qo'shildi\n");
                SystemSounds.Asterisk.Play();
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                //chaning text color of color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nKurs mavjudligi aniqlandi, iltimos qaytadan urunib ko'ring\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region DeleteGroup
        public void DeleteGroup(string GroupName)
        {
            GroupName = GroupName.ToLower();
            bool result = false;

            try
            {
                string json = File.ReadAllText(Constants.GroupsJsonPath);
                IList<GroupType> GroupstList = JsonConvert.DeserializeObject<List<GroupType>>(json);

                var MethodSyntax = GroupstList.Where(x => x.Name == GroupName).ToList();

                foreach (var item in MethodSyntax)
                {
                    if (item.Name == GroupName) result = true;
                }

                if (result == true)
                {
                    foreach (var item in MethodSyntax)
                    {
                        GroupstList.Remove(item);
                    }

                    string res = JsonConvert.SerializeObject(GroupstList);
                    File.WriteAllText(Constants.GroupsJsonPath, res);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nKurs muaffaqiylatli o'chirildi");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nKurs topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nXatolik aniqlandi, qaytadan urinib koring\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

    }
}
