using System.Text.Json;
internal class Program
{
    private static void Main(string[] args)
    {
        TelBook t = new TelBook();
        Menu(t);
    }

    public static void Menu(TelBook t)
    {
        while (true){
            Console.Clear();
            Console.WriteLine("1 - Add a contact");
            Console.WriteLine("2 - Find a contact");
            Console.WriteLine("3 - Delete a contact");
            Console.WriteLine("4 - Change a contact");
            Console.WriteLine("5 - See all my contacts");
            Console.WriteLine("0 - Exit");
            int ans = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (ans == 1)
            {
                t.AddContact();
            }
            else if (ans == 2)
            {
                t.FindContact();
            }
            else if (ans == 3)
            {
                t.RemoveContact();
            }
            else if (ans == 4)
            {
                t.ChangeContact();
            }
            else if (ans == 5)
            {
                t.ShowAll();
            }
            else if (ans == 0)
            {
                t.SerializeMethod();
                //Console.WriteLine(t.SerializeMethod());
                break;
            }
            else continue;
            Console.WriteLine("Press any button to continue!");
            Console.ReadKey();
        }
    }

    public class TelBook
    {
        List<Contact> contacts;
        public TelBook()
        {
            contacts = new List<Contact>(0);
        }
        public void AddContact()
        {
            Contact c = new Contact();
            c.Init();
            contacts.Add(c);
        }
        public void AddContact(Contact c)
        {
            contacts.Add(c);
        }
        public int FindContact()
        {
            Console.WriteLine("Enter the name of contact you want to find");
            string n = Console.ReadLine();
            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].GetName() == n)
                {
                    contacts[i].Show();
                    return i;
                }
            }
            Console.WriteLine("Contact not found");
            return -1;
        }
        public void RemoveContact()
        {
            int i = FindContact();
            if (i != -1)
            {
                contacts.Remove(contacts[i]);
                Console.WriteLine("Deleted!");
            }
        }
        public void ChangeContact()
        {
            int i = FindContact();
            if (i != -1)
            {
                Console.WriteLine("1 - Change name");
                Console.WriteLine("2 - Change telefone number");
                Console.WriteLine("3 - Change work number");
                Console.WriteLine("4 - Change information");
                Console.WriteLine("0 - Continue ");
                int ans = Convert.ToInt32(Console.ReadLine());

                if (ans == 1)
                {
                    contacts[i].ChangeName();
                }
                else if (ans == 2)
                {
                    contacts[i].ChangeTelNum();
                }
                else if (ans == 3)
                {
                    contacts[i].ChangeWorkNum();
                }
                else if (ans == 4)
                {
                    contacts[i].ChangeInfo();
                }
                else if (ans == 0)
                {

                }
                else Console.WriteLine("Unknown command");
            } 
        }
        public void ShowAll()
        {
            contacts.Sort(Compare.CompareContacts);
            for (int i = 0; i < contacts.Count; i++)
            {
                contacts[i].Show();
            }
        }
        public string SerializeMethod()
        {
            var result = JsonSerializer.SerializeToUtf8Bytes(this.contacts, _options);
            File.WriteAllBytes("./Contacts.json", result);
            return System.Text.Encoding.UTF8.GetString(result);
            //return JsonSerializer.Serialize(contacts);
        }
    }

    public static JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };

    public delegate int Comparison<in T>(T x, T y);

    public class Compare
    {
        public static int CompareContacts(Contact x, Contact y)
        {
            return x.GetName()[0].CompareTo(y.GetName()[0]);
        }
    }

    public class Contact
    {
        string name;
        string telnum;
        string worknum;
        string info;

        public Contact()
        {
            name = " ";
            telnum = "0000000000000";
            worknum = "0000000000000";
            info = " ";
        }
        public Contact(string na, string tn, string wn, string inf)
        {
            name = na;
            if (tn.Length != 12 || CheckNum(tn) == false) Console.WriteLine("Incorect num: " + tn + "\nNumber is not added, change it");
            else telnum = tn;
            if (wn.Length != 12 || CheckNum(wn) == false) Console.WriteLine("Incorect num: " + wn + "\nNumber is not added, change it");
            else telnum = wn;
            info = inf;
        }

        public void Show()
        {
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Telefone number: +" + telnum);
            Console.WriteLine("Work number: +" + worknum);
            Console.WriteLine("Information: " + info);
        }
        public void ChangeName()
        {
            Console.WriteLine("Enter new name");
            string s;
            s = Console.ReadLine();
            name = s;
        }
        public void ChangeTelNum()
        {
            string s;
            Console.WriteLine("Enter new num");
            s = Console.ReadLine();
            if (s.Length != 12 || CheckNum(s) == false) Console.WriteLine("Incorect num: " + s);
            else telnum = s;
        }
        public void ChangeWorkNum()
        {
            string s;
            Console.WriteLine("Enter new num");
            s = Console.ReadLine();
            if (s.Length != 12 || CheckNum(s) == false) Console.WriteLine("Incorect num: " + s);
            else worknum = s;
        }
        public void ChangeInfo()
        {
            Console.WriteLine("Enter new information about the contact");
            string s;
            s = Console.ReadLine();
            info = s;
        }
        public void Init()
        {
            Console.WriteLine("Enter the name");
            name = Console.ReadLine();
            Console.WriteLine("Enter the telefone number");
            string tn = Console.ReadLine();
            if (tn.Length != 12 || CheckNum(tn) == false) Console.WriteLine("Incorect num: " + tn);
            else telnum = tn;
            Console.WriteLine("Enter the work number");
            string wn = Console.ReadLine();
            if (wn.Length != 12 || CheckNum(wn) == false) Console.WriteLine("Incorect num: " + wn);
            else worknum = wn;
            Console.WriteLine("Enter some information about the contact");
            info = Console.ReadLine();
        }
        private bool CheckNum(string s)
        {
            int r;
            for (int i = 0; i < s.Length; i++)
            {
                if (int.TryParse(s[i].ToString(), out r) == false)
                {
                    return false;
                }
            }
            return true;
        }
        public string GetName()
        {
            return name;
        }
        public string GetTelNum()
        {
            return telnum;
        }
        public string GetWorkNum()
        {
            return worknum;
        }
        public string GetInfo()
        {
            return info;
        }
    }
}


