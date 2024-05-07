using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace prjhoadon
{
     class Program
    {

        static void Main(string[] args)
        {
            User user = new User("admin1", "123");
            bool isLoggedIn = false;

            while (!isLoggedIn)
            {
                Console.Write("Nhap username: ");
                string userName = Console.ReadLine().Trim();
                Console.Write("Nhap password: ");
                string password = Console.ReadLine().Trim();

                if (userName == user.UserName && password == user.PassWord)
                {
                    isLoggedIn = true;
                }
                else
                {
                    Console.WriteLine("UserName va PassWord khong dung. Xin nhap lai!!");
                }
            }

            if (isLoggedIn)
            {
                HoaDon hoadon = new HoaDon("1", "milk", new DateTime(2004, 02, 03), 19, 1000);
                HoaDon hoadon1 = new HoaDon("2", "chair", new DateTime(2012, 12, 12), 16, 56000);
                HoaDon hoadon2 = new HoaDon("3", "pen", new DateTime(2006, 12, 26), 10, 45000);
                HoaDon hoadon3 = new HoaDon("4", "meat", new DateTime(2004, 11, 25), 15, 35000);
                HoaDon hoadon4 = new HoaDon("5", "computer", new DateTime(2002, 04, 21), 12, 90000);

                List<HoaDon> listHoaDon = new List<HoaDon> { hoadon, hoadon1, hoadon2, hoadon3, hoadon4 };

                var view = new HoaDonView();
                var controller = new ControllerHoaDon(listHoaDon);
                while (true)
                {
                    view.displayMenu();
                    int action = int.Parse(Console.ReadLine().Trim());
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
            Console.WriteLine("------ Quan ly hoa don ------");
            Console.WriteLine("1. Them hoa don");
            Console.WriteLine("2. Xoa hoa don");
            Console.WriteLine("3. Tim kiem hoa don");
            Console.WriteLine("4. Update hoa don");
            Console.WriteLine("5. Hien thi tat ca hoa don");
            Console.WriteLine("6. Thoat");
            Console.Write("chon action: ");
        }
    }
}
