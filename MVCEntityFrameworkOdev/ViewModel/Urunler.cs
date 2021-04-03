using MVCEntityFrameworkOdev.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVCEntityFrameworkOdev.ViewModel
{
    public class Urunler:IEnumerable
    {
       
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string  FullName { get; set; }
        public string Customer { get; set; }
        public string ProductName { get; set; }
        public string  Supplier { get; set; }
        public short? UnitsOrder { get; set; }
        public int? SupplierID { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? CategoryID { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}