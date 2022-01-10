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
    public class StudentType : ModelofStudent, IStudentRepository
    {
        #region AddStudent
        public void AddStudent(StudentType student)
        {
            string json = File.ReadAllText(Constants.StudentsJsonPath);
            IList<StudentType> StudentList = JsonConvert.DeserializeObject<List<StudentType>>(json);

            bool result = false;
            var ress = (StudentList.Select(st => new StudentType() { FirstName = st.FirstName, LastName = st.LastName, Age = st.Age, Contact = st.Contact, Group = st.Group })).ToList();

            foreach (var item in ress)
            {
                if (item.Contact == Contact)
                {
                    result = true;
                }
            }

            if (result == false)
            {
                student.Balance = 0;
                
                Reception reception = new Reception();
                string GroupName = reception.ShowGroupsToNewStudent();

                StudentList.Add(new StudentType { FirstName = student.FirstName, LastName = student.LastName, Age = student.Age, Balance = student.Balance, Contact = student.Contact, Group = GroupName});

                string res = JsonConvert.SerializeObject(StudentList);
                File.WriteAllText(Constants.StudentsJsonPath, res);
               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nO'quvchi muaffaqiyatli qo'shildi");
                SystemSounds.Asterisk.Play();
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.White;
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Telfon raqam mavjutligi aniqlandi, iltimos qaytadan urunib ko'ring");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region DeleteStudent
        public bool DeleteStudent(string Contact)
        {
            bool result = false;

            try
            {
                string json = File.ReadAllText(Constants.StudentsJsonPath);
                IList<StudentType> StudentList = JsonConvert.DeserializeObject<List<StudentType>>(json);


                var MethodSyntax = StudentList.Where(x => x.Contact == Contact).ToList();

                if (MethodSyntax.Count > 0)
                {


                    foreach (var item in MethodSyntax)
                    {
                        StudentList.Remove(item);
                    }

                    string res = JsonConvert.SerializeObject(StudentList);
                    File.WriteAllText(Constants.StudentsJsonPath, res);


                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nO'quvchi muaffaqiyatli o'chirildi\n");
                    SystemSounds.Asterisk.Play();
                    Thread.Sleep(1000);
                    result = true;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nO'quvchi topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                return result;
            }
            catch
            {
                //changing console text color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nXatolik yuz berdi, qaytadan urinib koring!\n");
                Console.ForegroundColor = ConsoleColor.White;
            }

            return result;
        }
        #endregion

        #region UpdateStudent
        public void UpdateStudent(string Contact)
        {
            bool result = false;

            StudentType student = new StudentType();

            string json = File.ReadAllText(Constants.StudentsJsonPath);
            IList<StudentType> StudentList = JsonConvert.DeserializeObject<List<StudentType>>(json);

            var students = StudentList.Where(x => x.Contact == Contact).ToList();
            foreach (var st in students)
            {
                student.Balance = st.Balance;
                student.Group = st.Group;
            }

            if (students.Count > 0)
            {
                foreach (var item in students)
                {
                    StudentList.Remove(item);
                }

                string res = JsonConvert.SerializeObject(StudentList);
                File.WriteAllText(Constants.StudentsJsonPath, res);
                result = true;
            }

            if (result == true)
            {
                #region input data
                Console.Write("Yangi ism kiriting: ");
                student.FirstName = Console.ReadLine();
                student.FirstName = student.FirstName.Capitalize();

                Console.Write($"{student.FirstName}ning familyasini kiriting: ");
                student.LastName = Console.ReadLine();
                student.LastName = student.LastName.Capitalize();

                Console.Write($"{student.FirstName} {student.LastName}ning yoshni kiriting: ");
                student.Age = int.Parse(Console.ReadLine());

                Console.Write($"{student.FirstName} {student.LastName}ning telfon raqamini kiriting: ");
                student.Contact = Console.ReadLine();
                #endregion
                
                StudentList.Add(new StudentType { FirstName = student.FirstName, LastName = student.LastName, Age = student.Age, Contact = student.Contact, Balance = student.Balance, Group = student.Group });

                string res = JsonConvert.SerializeObject(StudentList);
                File.WriteAllText(Constants.StudentsJsonPath, res);
            }
        }
        #endregion

        #region SearchStudent
        public void SearchStudent(string Contact)
        {
            int succesChecker = 0;

            string json = File.ReadAllText(Constants.StudentsJsonPath);
            IList<StudentType> StudentList = JsonConvert.DeserializeObject<List<StudentType>>(json);

            var MethodSyntax = StudentList.Where(x => x.Contact == Contact).ToList();

            if (MethodSyntax.Count > 0)
            {
                foreach (var item in MethodSyntax)
                {
                    if (item.Contact == Contact)
                    {
                        //sharing result with user
                        //changing console text color
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nIsm: {item.FirstName}\n" +
                            $"Familya: {item.LastName}\n" +
                            $"Yosh: {item.Age}\n" +
                            $"Kontakt: {item.Contact}\n" +
                            $"Kurs: {item.Group}\n" +
                            $"To'langan summa: {item.Balance} so'm\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        succesChecker++;
                        break;
                    }
                }
            }

            if (succesChecker == 0)
            {
                //changing console text color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("O'quvchi topilmadi");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region PayForStudy
        public void PayForStudy(string Contact)
        {
            string json = File.ReadAllText(Constants.StudentsJsonPath);
            IList<StudentType> StudentList = JsonConvert.DeserializeObject<List<StudentType>>(json);
            
            StudentType student = new StudentType();

            bool result = false;
            var studeninfo = StudentList.Where(x => x.Contact == Contact).ToList();

            foreach (var item in studeninfo)
            {
                if (item.Contact == Contact)
                {
                    result = true;
                }
            }

            if (result == true)
            {
                foreach (var iteam in studeninfo) 
                { 
                    decimal GroupCost = SpecifyerOfGroupCost(iteam.Group);

                    student.FirstName = iteam.FirstName;
                    student.LastName = iteam.LastName;
                    student.Balance = Convert.ToDecimal(iteam.Balance);

                    if (studeninfo.Count > 0)
                    {
                        foreach (var item in studeninfo)
                        {
                            StudentList.Remove(item);
                        }
                        string res = JsonConvert.SerializeObject(StudentList);
                        File.WriteAllText(Constants.StudentsJsonPath, res);
                    }

                    if (GroupCost <= student.Balance)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n{student.FirstName} {student.LastName} uchun to'lov amalga oshirilgan");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (student.Balance < GroupCost)
                    {
                        decimal MustPay = GroupCost - student.Balance;
                        Console.WriteLine($"\n{student.FirstName} {student.LastName} {iteam.Group} kursi uchun {MustPay} so'm to'lashi lozim");
                        Console.Write("\nSummani kiriting: ");
                        try
                        {
                            decimal payment = decimal.Parse(Console.ReadLine());
                            if (payment > MustPay)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\n{MustPay} so'mdan ortiq summa kirita olmaysiz, iltimos qaytadan urunib ko'ring");
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            else if (payment <= MustPay) 
                            { 
                                MustPay = GroupCost - (student.Balance + payment);
                                student.Balance = student.Balance + payment;
                                iteam.Balance = student.Balance;

                                StudentList.Add(new StudentType { FirstName = iteam.FirstName, LastName = iteam.LastName, Age = iteam.Age, Balance = iteam.Balance, Contact = iteam.Contact, Group = iteam.Group });
                                json = JsonConvert.SerializeObject(StudentList);
                                File.WriteAllText(Constants.StudentsJsonPath, json);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"\nTo'lov muaffaqiyatli amlaga oshirldi, o'quvchida {MustPay} so'm miqdorda qarzdorlik qoldi");
                                SystemSounds.Asterisk.Play();
                                Thread.Sleep(1000);
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }
                        }
                        catch
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan urunib ko'ring");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }

                }
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nO'quvchi topilmadi");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region GroupCost
        public decimal SpecifyerOfGroupCost(string NameOfGroup)
        {
            decimal result = 0;
            string json = File.ReadAllText(Constants.GroupsJsonPath);
            IList<GroupType> StudentList = JsonConvert.DeserializeObject<List<GroupType>>(json);
            var groups = StudentList.Where(x => x.Name == NameOfGroup).ToList();
            
            foreach (var group in groups)
            {
                if (group.Name == NameOfGroup) result = group.Cost;
            }
            return result;
        }
        #endregion
    }
}
