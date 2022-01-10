using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace EducationalCenter
{
    internal class MainMenu
    {
        public void Menu()
        {
            Console.Title = "<<< Education Center >>>";
            Console.ForegroundColor = ConsoleColor.White;
            
            while (true)
            {
                Console.Write("\nAdminstratsiya(1) | Kurslar haqida ma'lumot(2) | Dasturdan chiqish(3)\n" +
                    ">>> ");
                string mainChoice = Console.ReadLine();

                if (mainChoice == "1")
                {
                    Console.Clear();
                    Adminstration admin = new Adminstration();
                    #region input data
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Agarda login parolni bilmasangiz, login: admin | parol: admin12345");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("\nLoginingizni kiriting: ");
                    admin.Login = Console.ReadLine();

                    Console.Write("Parolingizni kiriting: ");
                    admin.Password = admin.ReadPassword();

                    #endregion
                    //checking if login and password exsist
                    bool result = admin.IsAdmin(admin);
                    if (result == true)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLogin va parol tasdiqlandi\n");
                        SystemSounds.Asterisk.Play();
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.White;
                        OnlyAdmin();
                    }

                    else
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLogin yoki parol xato kiritildi\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (mainChoice == "2")
                {
                    Reception reception = new Reception();
                    Console.Clear();
                    reception.ShowGroups();
                }
                else if (mainChoice == "3")
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Dastur tugatildi");
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        public void StudentMenu()
        {
            StudentType student = new StudentType();
            Console.Clear();
            while (true)
            {
                Console.Write("\nTo'lovni qabul qilish(1)   |    O'quvchi haqidagi ma'lumotlar(2)   |     Ma'lumotlarini yangilash(3)\n" +
                              "O'quvchi qo'shish(4)       |    O'quvchini o'chirish(5)            |     Ortga qaytish(6)\n" +
                    ">>> ");
                string StudentChoice = Console.ReadLine();
                if (StudentChoice == "1")
                {
                    Console.Clear();
                    Console.Write($"\nO'quvchining telefon raqamini kiritng: ");
                    string Contact = Console.ReadLine();
                    student.PayForStudy(Contact);
                }
                else if (StudentChoice == "2")
                {
                    Console.Clear();
                    Console.Write("O'quvchining telefon raqamini kiriting: ");
                    string Contact = Console.ReadLine();
                    student.SearchStudent(Contact);
                }
                else if (StudentChoice == "3")
                {
                    Console.Clear();
                    Console.Write("O'quvchining telefon raqamini kiriting: ");
                    string Contact = Console.ReadLine();
                    student.UpdateStudent(Contact);
                }
                else if (StudentChoice == "4")
                {
                    Console.Clear();
                    try
                    {
                        #region input data
                        Console.Write("\nO'quvchning ismni kiriting: ");
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
                        
                        student.AddStudent(student);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan kiriting\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (StudentChoice == "5")
                {
                    Console.Clear();
                    Console.Write("\nO'quvchining telefon raqamini kiritng: ");
                    string Contact = Console.ReadLine();
                    student.DeleteStudent(Contact);
                }
                else if (StudentChoice == "6")
                {
                    Console.Clear();
                    OnlyAdmin();
                }
                else
                {
                    Console.Clear();
                    //chaging text color of console
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilamdi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void GroupMenu()
        {
            GroupType group = new GroupType();
            Console.Clear();
            while (true)
            {
                Console.Write("Yangi kurs qo'shish(1) | Kursni o'chirish(2) | Ortga qaytish(3) \n" +
                    ">>> ");
                string GroupChoice = Console.ReadLine();
                if (GroupChoice == "1")
                {
                    Console.Clear();
                    try
                    {
                        #region input data
                        Console.Write("\nKurs nomini kiriting: ");
                        string groupName = Console.ReadLine();
                        groupName = groupName.ToLower();
                        group.Name = groupName;
                        Console.Write("Kurs narxini kiriting: ");
                        group.Cost = int.Parse(Console.ReadLine());
                       
                        #endregion
                        group.AddGroup(group);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan urinib ko'ring\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                else if (GroupChoice == "2")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEslatma: Kursni o'chirish, kurs o'qituvchisi va kurs o'quvchilari ham o'chirilishiga olib keladi!\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("Kurs nomini kiriting: ");
                    string GroupName = Console.ReadLine();
                    group.DeleteGroup(GroupName);
                }
                else if (GroupChoice == "3")
                {
                    Console.Clear();
                    OnlyAdmin();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void AdminMenu()
        {
            Adminstration admin = new Adminstration();
            while (true)
            {
                Console.Write("Admin qo'shish(1) | Adminni o'chirish(2) | Adminlar ro'yhati(3) | Ortga qaytish(4)\n" +
                                   ">>> ");
                string adminChoice = Console.ReadLine();
                if (adminChoice == "1")
                {
                    Console.Clear();
                    try
                    {
                        #region input data
                        Console.Write("Yangi adminstratorning ismni kiriting: ");
                        admin.FirstName = Console.ReadLine();
                        admin.FirstName = admin.FirstName.Capitalize();

                        Console.Write($"{admin.FirstName}ning familyasini kiriting: ");
                        admin.LastName = Console.ReadLine();
                        admin.LastName = admin.LastName.Capitalize();

                        Console.Write($"{admin.FirstName} {admin.LastName}ning yoshni kiriting: ");
                        admin.Age = int.Parse(Console.ReadLine());

                        Console.Write($"{admin.FirstName} {admin.LastName}ning telfon raqamini kiriting: ");
                        admin.Contact = Console.ReadLine();

                        place:
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("\n1.Asosiy admin\n" +
                            "2.Yordamchi admin");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.Write($"{admin.FirstName} {admin.LastName}ning adminstratorlik darajasini kiriting: ");
                        string role = Console.ReadLine();
                        if(role != "1" && role != "2") 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nHech narsa toplimadi\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            goto place;
                        }

                        Console.Write("Login yarating: ");
                        admin.Login = Console.ReadLine();

                        Console.Write("Parol yarating: ");
                        admin.Password = Console.ReadLine();
                        #endregion
                        admin.AddAdmistrator(admin, role);
                    }
                    catch
                    {
                        //chaning text of console color
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan urunib ko'ring\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (adminChoice == "2")
                {
                    Console.Clear();
                    admin.DeleteAdmin();
                }
                else if (adminChoice == "3")
                {
                    Console.Clear();
                    admin.ShowAdmins();
                }
                else if (adminChoice == "4")
                {
                    Console.Clear();
                    OnlyAdmin();
                }
                else
                {
                    Console.Clear();
                    //chaning text of console color
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void OnlyAdmin()
        {   
            Adminstration admin = new Adminstration();
            while (true)
            {
                Console.Write("\nO'quvchilar parametrlari(1) | Kurslar parametrlari(2) | Adminstratorlar parametrlari(3) | Asosiy menuga qaytish(4) \n" +
                    ">>> ");

                string adminChoice = Console.ReadLine();
                if (adminChoice == "1")
                {
                    Console.Clear();
                    StudentMenu();
                }
                else if (adminChoice == "2")
                {
                    Console.Clear();
                    GroupMenu();
                }
                else if (adminChoice == "3")
                {
                    Console.Clear();
                    #region input data
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEslatma: Admistratorlar parametrlariga kirish uchun Asosiy admin bo'lishingiz lozim!\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Agarda login parolni bilmasangiz, login: admin | parol: admin12345");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("\nLoginingizni kiriting: ");
                    admin.Login = Console.ReadLine();

                    Console.Write("Parolingizni kiriting: ");
                    admin.Password = admin.ReadPassword();
                    #endregion 

                    bool result = admin.IsMainAdmin(admin);
                    if (result == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLogin va Parol tasdiqlandi\n");
                        SystemSounds.Asterisk.Play();
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.White;
                        AdminMenu();
                    }
                    else if (result == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nAdmistratorlar parametrlariga faqat Asosiy admin kira oladi!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (adminChoice == "4")
                {
                    Console.Clear();
                    Menu();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}

