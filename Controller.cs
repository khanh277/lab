using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Console_BillManager
{
    public class Controller
    {
        private List<Bill> listBill;

        public Controller(List<Bill> bills)
        {
            listBill = bills ?? new List<Bill>();
        }

        public void Add()
        {
            string billCode = PromptForNonEmptyString("Bill Code");
            while (CheckDuplicate(billCode))
            {
                Console.WriteLine("A bill with this code already exists!");
                billCode = PromptForNonEmptyString("Bill Code");
            }

            Console.Write("Enter Export Date (yyyy-MM-dd): ");
            DateTime exportDate = FormatDate();

            Bill newBill = new Bill(billCode, exportDate);

            string productCode = PromptForNonEmptyString("Product Code");
            string productName = PromptForNonEmptyString("Product Name");
            double price = ReadPositiveDouble("Product Price");
            int amount = ReadPositiveInteger("Product Amount");

            Product product = new Product(productCode, productName, price, amount);
            newBill.Products.Add(product);

            listBill.Add(newBill);
            Console.WriteLine("Bill added successfully!");
        }

        private string PromptForNonEmptyString(string fieldName)
        {
            Console.Write($"Enter {fieldName}: ");
            string input = Console.ReadLine().Trim();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write($"{fieldName} cannot be left blank! Re-enter {fieldName}: ");
                input = Console.ReadLine().Trim();
            }
            return input;
        }

        private double ReadPositiveDouble(string fieldName)
        {
            Console.Write($"Enter {fieldName}: ");
            double value;
            while (!double.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.Write($"{fieldName} must be a positive number. Re-enter {fieldName}: ");
            }
            return value;
        }

        private int ReadPositiveInteger(string fieldName)
        {
            Console.Write($"Enter {fieldName}: ");
            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.Write($"{fieldName} must be a positive integer. Re-enter {fieldName}: ");
            }
            return value;
        }

        private bool CheckDuplicate(string billCode)
        {
            return listBill.Any(b => b.Code == billCode);
        }

        private DateTime FormatDate()
        {
            DateTime date;
            string input = Console.ReadLine().Trim();
            while (!DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Console.Write("Invalid date format, please re-enter (yyyy-MM-dd): ");
                input = Console.ReadLine().Trim();
            }
            return date;
        }

        public void Remove()
        {
            Console.Write("Enter the code of the bill to be deleted: ");
            string code = Console.ReadLine().Trim();
            Bill bill = listBill.FirstOrDefault(b => b.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (bill != null)
            {
                listBill.Remove(bill);
                Console.WriteLine("Bill deleted successfully!");
            }
            else
            {
                Console.WriteLine("Bill not found!");
            }
        }

        public void Search()
        {
            Console.Write("Enter the name of the bill to search for: ");
            string keyword = Console.ReadLine().Trim();
            var results = listBill.Where(b => b.Products.Any(p => p.Name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
            if (results.Count > 0)
            {
                Console.WriteLine($"------ Search Results for '{keyword}' ------");
                Console.WriteLine($"| {"Code",-15} | {"Product Code",-15} | {"Product Name",-20} | {"Export Date",-12} | {"Price",-10} | {"Amount",-10} | {"Total",-10}");

                foreach (var bill in listBill)
                {
                    foreach (var product in bill.Products)
                    {
                        Console.WriteLine($"| {bill.Code,-15} | {product.Code,-15} | {product.Name,-20} | {bill.ExportDate.ToString("yyyy-MM-dd"),-12} | {product.Price,-10} | {product.Amount,-10} | {bill.Total,-10} |");
                    }
                }
            }
            else
            {
                Console.WriteLine("No results found!");
            }
        }

        public void Update()
        {
            Console.Write("Enter the code of the bill to update: ");
            string code = Console.ReadLine().Trim();
            Bill bill = listBill.FirstOrDefault(b => b.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (bill == null)
            {
                Console.WriteLine("Bill not found!");
                return;
            }

            Console.WriteLine("Select the information to update:");
            Console.WriteLine("1. Export Date");
            Console.WriteLine("2. Product Details");

            Console.Write("Enter your choice (1-2): ");
            int choice ;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                Console.Write("Enter choice in range 1 and 2 : ");
            }

            switch (choice)
            {
                case 1:
                    UpdateExportDate(bill);
                    break;
                case 2:
                    UpdateProductDetails(bill);
                    break;
                default:
                    Console.WriteLine("Invalid choice. No changes made.");
                    break;
            }

            Console.WriteLine("Update completed successfully.");
        }

        private void UpdateExportDate(Bill bill)
        {
            Console.Write("Enter new Export Date (yyyy-MM-dd): ");
            DateTime newDate;
            string input = Console.ReadLine().Trim();
            if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDate))
            {
                bill.ExportDate = newDate;
                Console.WriteLine("Export date updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid date format. No changes made.");
            }
        }

        private void UpdateProductDetails(Bill bill)
        {
            if (bill.Products.Count == 0)
            {
                Console.WriteLine("No products in this bill to update.");
                return;
            }

            Product product = bill.Products[0];
                
            Console.WriteLine("Select the detail to update for the first product:");
            Console.WriteLine("1. Product Code");
            Console.WriteLine("2. Product Name");
            Console.WriteLine("3. Quantity");
            Console.WriteLine("4. Price");

            Console.Write("Enter your choice (1-4): ");
            int detailChoice;
            while (!int.TryParse(Console.ReadLine(), out detailChoice) || detailChoice < 1 || detailChoice > 4)
            {
                Console.Write("Enter choice in range 1 and 4 : ");
            }
            switch (detailChoice)
            {
                case 1:
                    Console.Write("Enter new product code: ");
                    product.Code = Console.ReadLine().Trim();
                    break;
                case 2:
                    Console.Write("Enter new product name: ");
                    product.Name = Console.ReadLine().Trim();
                    break;
                case 3:
                    Console.Write("Enter new quantity: ");
                    product.Amount = int.Parse(Console.ReadLine());
                    break;
                case 4:
                    Console.Write("Enter new price: ");
                    double newPrice = double.Parse(Console.ReadLine());
                    product.UpdatePrice(newPrice);
                    break;
                default:
                    Console.WriteLine("Invalid choice. No changes made.");
                    break;
            }

            Console.WriteLine("Product updated successfully.");
        }

        public void DisplayAll()
        {
            Console.WriteLine("------ List of invoices ------");
            if (!listBill.Any())
            {
                Console.WriteLine("Empty List!!");
                return;
            }

            Console.WriteLine($"| {"Code",-15} | {"Product Code",-15} | {"Product Name",-20} | {"Export Date",-12} | {"Price",-10} | {"Amount",-10} | {"Total",-10}");

            foreach (var bill in listBill)
            {
                foreach (var product in bill.Products)
                {
                    Console.WriteLine($"| {bill.Code,-15} | {product.Code,-15} | {product.Name,-20} | {bill.ExportDate.ToString("yyyy-MM-dd"),-12} | {product.Price,-10} | {product.Amount,-10} | {bill.Total,-10} |");
                }

            }
        }

    }
}
