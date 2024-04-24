using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prjhoadon
{
    internal class ControllerHoaDon
    {
        List<hoaDon> listHoaDon = new List<hoaDon>();

        public void Add() {
            Console.Write("Nhap code: ");
            string code = Console.ReadLine().Trim();
            if (CheckDupilicate(code)) {
                Console.WriteLine("code is duplicate !!");
                return;
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

            hoaDon hoadon = new hoaDon(code, name, exportDate, amount, total); 
            listHoaDon.Add(hoadon);
            Console.WriteLine("Add thanh cong !!");
        }

        private bool CheckDupilicate(string code)
        {
            foreach (var hoaDon in listHoaDon)
            {
                if (hoaDon.GetCode().Equals(code))
                {
                    return true;
                }
            }
            return false;

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
            /*int index = -1;
            for (int i = 0; i < listHoaDon.Count; i++)
            {
                hoaDon hoadon = listHoaDon[i];

                if (hoadon.GetCode().Equals(deleteCode, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;
                    break;
                }
            }*/
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
