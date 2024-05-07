using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Console_BillManager
{
    public class BaseEntity
    {
        private string code;
        public string Code
        {
            get => code;
            set
            {
                while (string.IsNullOrWhiteSpace(value))
                {
                    Console.Write("Code cannot be left blank!! Please re-enter code: ");
                    value = Console.ReadLine().Trim();
                }
                code = value;
            }
        }

        protected BaseEntity(string code)
        {
            Code = code;
        }
    }


    public class Product : BaseEntity
    {
        private string name;
        public string Name
        {
            get => name;
            set
            {
                while (string.IsNullOrWhiteSpace(value))
                {
                    Console.Write("Product name cannot be left blank!! Please re-enter product name: ");
                    value = Console.ReadLine().Trim();
                }
                name = value;
            }
        }

        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                while (value <= 0)
                {
                    Console.Write("Amount must be greater than 0. Please re-enter amount: ");
                    value = int.TryParse(Console.ReadLine(), out int result) ? result : 0;
                }
                amount = value;
            }
        }

        private double price;
        public double Price
        {
            get => price;
            private set
            {
                while (value <= 0)
                {
                    Console.Write("Price must be greater than 0. Please re-enter price: ");
                    value = double.TryParse(Console.ReadLine(), out double result) ? result : 0;
                }
                price = value;
            }
        }

        public Product(string code, string name, double price, int amount) : base(code)
        {
            Name = name;
            Price = price;
            Amount = amount;
        }

        public void UpdatePrice(double newPrice)
        {
            if (newPrice <= 0)
                Console.Write("Price must be greater than 0.");
            else
                price = newPrice;
        }

    }


    public class Bill : BaseEntity
    {
        public DateTime ExportDate { get; set; }
        public List<Product> Products { get; private set; }
        public double Total { get { return GetTotalPrice(); } }

        public Bill(string code, DateTime exportDate) : base(code)
        {
            ExportDate = exportDate;
            Products = new List<Product>();
        }

        public double GetTotalPrice()
        {
            return Products.Sum(p => p.Price * p.Amount);
        }
    }
}
