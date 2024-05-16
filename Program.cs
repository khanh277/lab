using prjhoadon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_BillManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("admin1", "123");
            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                Console.Write("Enter Username: ");
                string userName = Console.ReadLine().Trim();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Username or password cannot be left blank!!");
                }
                else if (userName == account.UserName && password == account.PassWord)
                {
                    isLoggedIn = true;
                }
                else
                {
                    Console.WriteLine("UserName or PassWord incorrect!!");
                }
            }

            if (isLoggedIn)
            {
                List<Bill> listBill = new List<Bill>
                {

                };

                var view = new HoaDonView();
                Controller controller = new Controller(listBill);
                while (true)
                {
                    view.displayMenu();
                    int action;
                    while (!int.TryParse(Console.ReadLine(), out action) || action < 1 || action > 6)
                    {
                        Console.Write("Enter choice in range 1 and 6 : ");
                    }
                    switch (action)
                    {
                        case 1:
                            controller.Add();
                            break;
                        case 2:
                            controller.Remove();
                            break;
                        case 3:
                            controller.Search();
                            break;
                        case 4:
                            controller.Update();
                            break;
                        case 5:
                            controller.DisplayAll();
                            break;
                        case 6:
                            Environment.Exit(0);
                            break; ;

                    }
                }
            }
        }
    }

    class HoaDonView
    {
        public void displayMenu()
        {
            Console.WriteLine("------ Invoice Management ------");
            Console.WriteLine("1. Add Invoice");
            Console.WriteLine("2. Delete Invoice");
            Console.WriteLine("3. Search Invoice");
            Console.WriteLine("4. Update Invoice");
            Console.WriteLine("5. Display All Invoice");
            Console.WriteLine("6. Exit");
            Console.Write("Enter Choice: ");
        }
    }


}
