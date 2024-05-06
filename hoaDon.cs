    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace prjhoadon
    {
         class HoaDon
        {
            private string _code;
            private string _name;
            private DateTime _exportDate;
            private int _amount;
            private double _total;

            public HoaDon( string code, string name, DateTime exportDate, int amount, double total)
            {
               _code = code;
               _name = name;
               _exportDate = exportDate;
               _amount = amount;
               _total = total;
            }

            public string GetCode() => _code;
            public string SetCode(string code) => _code = code;
            public string GetName() => _name;
            public string SetName(string name) => _name = name;
            public DateTime GetExportDate() => _exportDate;
            public DateTime SetExportDate(DateTime exportDate) => _exportDate = exportDate;
            public int GetAmount() => _amount;
            public int SetAmount (int amount) => _amount = amount;
            public double GetTotal() => _total;
            public double SetTotal(double total) => _total = total;





        }
    }
