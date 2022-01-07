using ConsoleTables;
using EducationalCenter.IRepository;
using EducationalCenter.Models;
using EducationalCenter.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EducationalCenter
{
    internal class Adminstration : ModelOfAdminstration, IAdmin
    {
        #region IsMainAdmin
        public static bool IsMainAdmin(Adminstration admin) 
        {
            bool result = false;

            string json = File.ReadAllText(Constants.AdminsJsonPath);
            IList<Adminstration> AdminstList = JsonConvert.DeserializeObject<List<Adminstration>>(json);

            foreach (var iteam in AdminstList)
            {
                if (iteam.FirstName != "")
                {
                    if (iteam.Login == admin.Login)
                    {
                        //checking if password and login true
                        byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(admin.Password);
                        byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                        admin.Password = ByteArrayToString(tmpHash);
                        if (iteam.Password == admin.Password && iteam.RoleOfAdmin == RoleOfAdmin.MainAdmin) result = true ;
                    }
                }
            }
            return result;
        }
        #endregion

        #region ReadPassword
        public static string ReadPassword()
        {
            string password = "";
            while (true)
            {
                place:
                try
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            return null;
                        case ConsoleKey.Enter:
                            return password;
                        case ConsoleKey.Backspace:
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                            break;
                        default:
                            password += key.KeyChar;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("*");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch
                {
                    goto place;
                }
            }
        }
        #endregion

        #region IsAdmin
        public static bool IsAdmin(Adminstration admin)
        {
            bool result  = false;

            string json = File.ReadAllText(Constants.AdminsJsonPath);
            IList<Adminstration> AdminstList = JsonConvert.DeserializeObject<List<Adminstration>>(json);

            foreach (var iteam in AdminstList)
            {
                if (iteam.Login == admin.Login) 
                {   
                    //checking if password and login true
                    byte [] tmpSource = ASCIIEncoding.ASCII.GetBytes(admin.Password);
                    byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                    admin.Password = ByteArrayToString(tmpHash);
                    if(iteam.Password == admin.Password) result = true;
                }
            }
            return result;
        } //Done
        #endregion

        #region AddAdmin
        public static void AddAdmistrator(Adminstration admin, string role)
        {
            string json = File.ReadAllText(Constants.AdminsJsonPath);
            IList<Adminstration> AdminstList = JsonConvert.DeserializeObject<List<Adminstration>>(json);

            int succesChecker = 0;
            //getting file data into string array
            bool result = admin.CheckIfAlreadyExist(Constants.AdminsJsonPath, admin);

            if (result == false)
            {
                //hashing input password
                byte[] tmpByteHashedPassword = HashThePassword(admin);
                admin.Password = ByteArrayToString(tmpByteHashedPassword);

                if (role == "1")
                {
                    AdminstList.Add(new Adminstration() { FirstName = admin.FirstName, LastName = admin.LastName, Age = admin.Age, Login = admin.Login, Password = admin.Password, Contact = admin.Contact, RoleOfAdmin = RoleOfAdmin.MainAdmin });
                    string res = JsonConvert.SerializeObject(AdminstList);
                    File.WriteAllText(Constants.AdminsJsonPath, res);
                    succesChecker++;
                }
                else if (role == "2")
                {
                    AdminstList.Add(new Adminstration() { FirstName = admin.FirstName, LastName = admin.LastName, Age = admin.Age, Login = admin.Login, Password = admin.Password, Contact = admin.Contact, RoleOfAdmin = RoleOfAdmin.AssistantAdmin });
                    string res = JsonConvert.SerializeObject(AdminstList);
                    File.WriteAllText(Constants.AdminsJsonPath, res);
                    succesChecker++;
                }

                //sharing result with user
                if (succesChecker == 0)
                {
                    //changing color of console text
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nXatolik aniqlandi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    //changing color of console text
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nAdminstrator muaffaqiyatli qo'shildi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                //changing color of console text
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nTelfon raqam mavjudligi aniqlandi, iltimos qaytadan urunib ko'ring\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }//Done
        #endregion

        #region DeleteAdmin
        public static void DeleteAdmin()
        {
            Console.Write("\nO'chirmoqchi bo'lgan adminstratorning telefon raqamini kiriting: ");
            string Contact = Console.ReadLine();

            string json = File.ReadAllText(Constants.AdminsJsonPath);
            IList<Adminstration> AdminstList = JsonConvert.DeserializeObject<List<Adminstration>>(json);
            var admins = AdminstList.Where(x => x.Contact == Contact).ToList();

            bool result = false;

            foreach(var admin in admins)
            {
                if (admin.Contact == Contact) result = true;
            }

            if (result == true)
            {
                foreach (var iteam in admins)
                {
                    AdminstList.Remove(iteam);
                }

                string res = JsonConvert.SerializeObject(AdminstList);
                File.WriteAllText(Constants.AdminsJsonPath, res);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAdminstrator muaffaqiyatli o'chirildi\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAdminstrator topilmadi\n");
                Console.ForegroundColor = ConsoleColor.White;
            
            }
        }
        #endregion

        #region ShowAdmins
        public static void ShowAdmins()
        {
            string json = File.ReadAllText(Constants.AdminsJsonPath);
            IList<Adminstration> AdminstList = JsonConvert.DeserializeObject<List<Adminstration>>(json);


            var table = new ConsoleTable("Admin", "Yosh","Telefon raqam", "Login", "Adminstratorlik darajasi");
            foreach (var admin in AdminstList)
            {
                if (admin.FirstName != "")
                {
                    table.AddRow(admin.FirstName + " " + admin.LastName, admin.Age, admin.Contact, admin.Login, admin.RoleOfAdmin);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            table.Write();
            Console.ForegroundColor = ConsoleColor.White;

        }
        #endregion

        #region Hashing
        public static byte [] HashThePassword(Adminstration admin)
        {

            //Create a byte array from source data
            byte [] tmpSource = ASCIIEncoding.ASCII.GetBytes(admin.Password);
            //hashing byte array into another byte array
            byte [] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return tmpHash;
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (int i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        #endregion

        #region Polymorpism
        public virtual bool CheckIfAlreadyExist(string path, Adminstration admin)
        {
            string json = File.ReadAllText(Constants.AdminsJsonPath);
            IList<Adminstration> AdminstList = JsonConvert.DeserializeObject<List<Adminstration>>(json);

            foreach (var Item in AdminstList)
            {
                if (Item.FirstName != "")
                {
                    if (Item.Contact == admin.Contact) return true;
                }
            }
            return false;
        } //Done
        #endregion
    }
}