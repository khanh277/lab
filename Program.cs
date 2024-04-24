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
            var view = new HoaDonView();
            var controller = new ControllerHoaDon();
            while (true)
            {
                view.displayMenu();
                int action = int.Parse(Console.ReadLine());
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
                        controller.DisplayAll();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break; ;

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
            Console.WriteLine("4. Hien thi tat ca hoa don");
            Console.WriteLine("5. Thoat");
            Console.Write("chon action: ");
        }
    }
}
