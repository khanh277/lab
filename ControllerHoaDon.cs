using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prjhoadon
{
    class ControllerHoaDon
    {

        private List<HoaDon> listHoaDon;

        public ControllerHoaDon(List<HoaDon> hoaDonList)
        {
            listHoaDon = hoaDonList;
        }


        public void Add() {
            Console.Write("Nhap code: ");
            string code = Console.ReadLine().Trim();
            while (CheckDuplicate(code))
            {
                Console.WriteLine("Code hoa don da ton tai xin vui long nhap lai!!");
                Console.Write("Nhap lai code: ");
                code = Console.ReadLine().Trim();
            }

            Console.Write("Nhap name: ");
            string name = Console.ReadLine().Trim();

            Console.Write("Nhap Export Date(yyyy-MM-dd): ");
            DateTime exportDate = FormatDate();

            Console.Write("Nhap amount: ");
            int amount;
            while (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0) {
                Console.WriteLine("Error !!!");
                Console.Write("Nhap lai amount: ");
            }

            Console.Write("Nhap total: ");
            double total;
            while (!double.TryParse(Console.ReadLine(), out total) || total <= 0) {
                Console.WriteLine("Error !!!");
                Console.Write("Nhap lai total: ");
            }   

            HoaDon hoadon = new HoaDon(code, name, exportDate, amount, total); 
            listHoaDon.Add(hoadon);
            Console.WriteLine("Add thanh cong !!");
        }

        private bool CheckDuplicate(string code)
        {
            return listHoaDon.Any(hd => hd.GetCode().Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        private DateTime FormatDate() { 
            while (true) {
                string ngayNhap = Console.ReadLine();

                string pattern = @"^\d{4}-\d{2}-\d{2}$";

                if (Regex.IsMatch(ngayNhap, pattern))
                {
                    DateTime exportDate;
                    if (DateTime.TryParse(ngayNhap, out exportDate))
                    {
                        return exportDate;
                    }
                }
                Console.WriteLine("Hay vui long nhap lai cho dung format !!");
                Console.Write("Nhap lai Export Date(yyyy-MM-dd): ");
            }
            
        }

        public void Remove()
        {
            Console.Write("Nhap code hoa don can xoa: ");
            String deleteCode = Console.ReadLine();
            int index = listHoaDon.FindIndex(hd => hd.GetCode().Equals(deleteCode, StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                listHoaDon.RemoveAt(index);
                Console.WriteLine($"Xoa thanh cong hoa don co ma {deleteCode}.");
            }
            else
            {
                
                Console.WriteLine($"Khong tim thay hoa don co ma {deleteCode}.");
            }
        
        }

        public void Search() {
            Console.Write("Nhap ten  hoa don can tim kiem: ");
            string keyword = Console.ReadLine().Trim();

            var result = listHoaDon.Where(hd => hd.GetName().IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            if (result.Count > 0)
            {
                Console.WriteLine($"------ Ket qua tim kiem cho '{keyword}' ------");
                Console.WriteLine($"| {"Code",-15} | {"Name",-15} | {"Export Date",-12} | {"Amount",-10} | {"Total",-10} |");

                foreach (var hoaDon in result)
                {
                    Console.WriteLine($"| {hoaDon.GetCode(),-15} | {hoaDon.GetName(),-15} | {hoaDon.GetExportDate().ToString("yyyy-MM-dd"),-12} | {hoaDon.GetAmount(),-10} | {hoaDon.GetTotal(),-10} |");
                }
            }
            else
            {
                Console.WriteLine($"Khong tim thay ket qua cho '{keyword}'.");
            }

        }

        public void Update() {

            Console.WriteLine("Nhap code cua hoa don can cap nhap: ");
            string code = Console.ReadLine().Trim();

            HoaDon hoadonToUpdate = listHoaDon.FirstOrDefault(hd => hd.GetCode().Equals(code, StringComparison.OrdinalIgnoreCase));

            if (hoadonToUpdate == null)
            {
                Console.WriteLine($"Khong tim thay hoa don voi ma {code}.");
                return;
            }

            Console.WriteLine("Chon thong tin can sua:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Export Date");
            Console.WriteLine("3. Amount");
            Console.WriteLine("4. Total");

            Console.Write("Nhap lua chon cua ban: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Lua chon khong hop le.");
                Console.Write("Nhap lai lua chon cua ban: ");
            }
            switch (choice)
            {
                case 1:
                    Console.Write("Nhap ten moi: ");
                    string newName = Console.ReadLine().Trim();
                    hoadonToUpdate.SetName(newName);
                    break;
                case 2:
                    Console.Write("Nhap Export Date moi (yyyy-MM-dd): ");
                    DateTime newExportDate = FormatDate();
                    hoadonToUpdate.SetExportDate(newExportDate);
                    break;
                case 3:
                    Console.Write("Nhap so luong moi: ");
                    int newAmount;
                    while (!int.TryParse(Console.ReadLine(), out newAmount) || newAmount <= 0)
                    {
                        Console.WriteLine("So luong khong hop le.");
                        Console.Write("Nhap lai so luong: ");
                    }
                    hoadonToUpdate.SetAmount(newAmount);
                    break;
                case 4:
                    Console.Write("Nhap tong gia tri moi: ");
                    double newTotal;
                    while (!double.TryParse(Console.ReadLine(), out newTotal) || newTotal <= 0)
                    {
                        Console.WriteLine("Tong gia tri khong hop le.");
                        Console.Write("Nhap lai tong gia tri: ");
                    }
                    hoadonToUpdate.SetTotal(newTotal);
                    break;
            }

            Console.WriteLine("Cap nhat thanh cong!!");
        }
       


        public void DisplayAll() {
            Console.WriteLine("------ Danh sach cac hoa don ------");
            if (listHoaDon.Count == 0)
            {
                Console.WriteLine("Danh sach trong.");
                return;
            }

            Console.WriteLine($"| {"Code",-15} | {"Name",-15} | {"Export Date",-12} | {"Amount",-10} | {"Total",-10} |");

            foreach (var hoaDon in listHoaDon)
            {
                Console.WriteLine($"| {hoaDon.GetCode(),-15} | {hoaDon.GetName(),-15} | {hoaDon.GetExportDate().ToString("yyyy-MM-dd"),-12} | {hoaDon.GetAmount(),-10} | {hoaDon.GetTotal(),-10} |");
            }
        }
    }
}
