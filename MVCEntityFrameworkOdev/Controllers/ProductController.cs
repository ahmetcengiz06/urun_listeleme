using MVCEntityFrameworkOdev.Models;
using MVCEntityFrameworkOdev.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEntityFrameworkOdev.Controllers
{
    public class ProductController : Controller
    {
        NORTHWNDEntities context;
        public ProductController()
        {
            context = new NORTHWNDEntities();
        }
        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = context.Products.ToList();
            return View(products);
        }
        [HttpGet]
        public ActionResult Orders(int? id)
        {
            List<SelectListItem> calisanlar = new List<SelectListItem>();
            foreach (var item in context.Employees)
            {
                calisanlar.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.EmployeeID.ToString() });
            }
            ViewBag.Calisanlar = calisanlar;
            List<SelectListItem> musteriler = new List<SelectListItem>();
            foreach (var item in context.Customers)
            {
                musteriler.Add(new SelectListItem() { Text = item.CompanyName, Value = item.CustomerID });
            }
            ViewBag.Musteriler = musteriler;
            List<Urunler> urunler = new List<Urunler>();
            var orders = (from o in context.Orders
                          join od in context.Order_Details on o.OrderID equals od.OrderID
                          join p in context.Products on od.ProductID equals p.ProductID
                          join e in context.Employees on o.EmployeeID equals e.EmployeeID
                          join c in context.Customers on o.CustomerID equals c.CustomerID
                          where p.ProductID == id
                          select new
                          {
                              Musteri = c.CompanyName,
                              Urun = p.ProductName,
                              SiparisTarih = o.OrderDate,
                              SiparisGirenCalisan = e.FirstName + " " + e.LastName,
                              SiparisId = p.ProductID

                          }
                        ).ToList();

            foreach (var item in orders)
            {
                Urunler u = new Urunler();
                u.OrderID = item.SiparisId;
                u.Customer = item.Musteri;
                u.ProductName = item.Urun;
                u.OrderDate = item.SiparisTarih;
                u.FullName = item.SiparisGirenCalisan;
                urunler.Add(u);
            }

            return View(urunler);
        }

        [HttpPost]
        public ActionResult Orders(FormCollection form, int? id)
        {
            List<SelectListItem> calisanlar = new List<SelectListItem>();
            foreach (var item in context.Employees)
            {
                calisanlar.Add(new SelectListItem() { Text = item.FirstName + " " + item.LastName, Value = item.EmployeeID.ToString() });
            }
            ViewBag.Calisanlar = calisanlar;
            List<SelectListItem> musteriler = new List<SelectListItem>();
            foreach (var item in context.Customers)
            {
                musteriler.Add(new SelectListItem() { Text = item.CompanyName, Value = item.CustomerID });
            }
            ViewBag.Musteriler = musteriler;
            List<Urunler> urunler = new List<Urunler>();

            var employee = form.Get("calisanlar");
            var customer = form.Get("musteriler");


            int employeeID;
            if (employee != "")
            {
                employeeID = Convert.ToInt32(employee);
                var orders = (from o in context.Orders
                              join od in context.Order_Details on o.OrderID equals od.OrderID
                              join p in context.Products on od.ProductID equals p.ProductID
                              join e in context.Employees on o.EmployeeID equals e.EmployeeID
                              join c in context.Customers on o.CustomerID equals c.CustomerID
                              where p.ProductID == id && e.EmployeeID == employeeID
                              select new
                              {
                                  Musteri = c.CompanyName,
                                  Urun = p.ProductName,
                                  SiparisTarih = o.OrderDate,
                                  SiparisGirenCalisan = e.FirstName + " " + e.LastName,
                                  SiparisId = o.OrderID

                              }
                      ).ToList();

                foreach (var item in orders)
                {
                    Urunler u = new Urunler();
                    u.OrderID = item.SiparisId;
                    u.Customer = item.Musteri;
                    u.ProductName = item.Urun;
                    u.OrderDate = item.SiparisTarih;
                    u.FullName = item.SiparisGirenCalisan;
                    urunler.Add(u);
                }
            }
            else if (employee != "" && customer.Length > 0)
            {
                string customerID = customer.ToString();
                employeeID = Convert.ToInt32(employee);
                var orders = (from o in context.Orders
                              join od in context.Order_Details on o.OrderID equals od.OrderID
                              join p in context.Products on od.ProductID equals p.ProductID
                              join e in context.Employees on o.EmployeeID equals e.EmployeeID
                              join c in context.Customers on o.CustomerID equals c.CustomerID
                              where p.ProductID == id && e.EmployeeID == employeeID && c.CustomerID == customerID
                              select new
                              {
                                  Musteri = c.CompanyName,
                                  Urun = p.ProductName,
                                  SiparisTarih = o.OrderDate,
                                  SiparisGirenCalisan = e.FirstName + " " + e.LastName,
                                  SiparisId = o.OrderID

                              }
                      ).ToList();

                foreach (var item in orders)
                {
                    Urunler u = new Urunler();
                    u.OrderID = item.SiparisId;
                    u.Customer = item.Musteri;
                    u.ProductName = item.Urun;
                    u.OrderDate = item.SiparisTarih;
                    u.FullName = item.SiparisGirenCalisan;
                    urunler.Add(u);
                }
            }
            else
            {
                string customerID = customer.ToString();

                var orders = (from o in context.Orders
                              join od in context.Order_Details on o.OrderID equals od.OrderID
                              join p in context.Products on od.ProductID equals p.ProductID
                              join e in context.Employees on o.EmployeeID equals e.EmployeeID
                              join c in context.Customers on o.CustomerID equals c.CustomerID
                              where p.ProductID == id && c.CustomerID == customerID
                              select new
                              {
                                  Musteri = c.CompanyName,
                                  Urun = p.ProductName,
                                  SiparisTarih = o.OrderDate,
                                  SiparisGirenCalisan = e.FirstName + " " + e.LastName,
                                  SiparisId = o.OrderID

                              }
                      ).ToList();

                foreach (var item in orders)
                {
                    Urunler u = new Urunler();
                    u.OrderID = item.SiparisId;
                    u.Customer = item.Musteri;
                    u.ProductName = item.Urun;
                    u.OrderDate = item.SiparisTarih;
                    u.FullName = item.SiparisGirenCalisan;
                    urunler.Add(u);
                }
            }


            return View(urunler);
        }

        [HttpGet]
        public ActionResult Suppliers(int? id)
        {
            var suppliers = (from p in context.Products
                             join s in context.Suppliers
                             on p.SupplierID equals s.SupplierID

                             where p.ProductID == id
                             select new
                             {

                                 ProductID = p.ProductID,
                                 ProductName = p.ProductName,
                                 SupplierID = s.SupplierID,
                                 CategoryID = p.CategoryID,
                                 SupplierName = s.CompanyName,
                                 SiparisBilgisi = p.UnitsOnOrder
                             }).ToList();

            Urunler u = new Urunler();
            foreach (var item in suppliers)
            {

                u.ProductID = item.ProductID;
                u.ProductName = item.ProductName;
                u.SupplierID = item.SupplierID;
                u.Supplier = item.SupplierName;
                u.CategoryID = item.CategoryID;
                u.UnitsOrder = item.SiparisBilgisi;

            }
            return View(u);
        }

        [HttpPost]
        public ActionResult Suppliers(Urunler urun,int? id)
        {
            
                Product product = context.Products.Find(urun.ProductID);
                product.UnitsOnOrder = urun.UnitsOrder;

                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
               int check = context.SaveChanges();
            if (check>0)
            {
                ViewBag.Check = true;
            }
            else
            {
                ViewBag.Check = true;
            }
          
            var suppliers = (from p in context.Products
                             join s in context.Suppliers
                             on p.SupplierID equals s.SupplierID

                             where p.ProductID == id
                             select new
                             {

                                 ProductID = p.ProductID,
                                 ProductName = p.ProductName,
                                 SupplierID = s.SupplierID,
                                 CategoryID = p.CategoryID,
                                 SupplierName = s.CompanyName,
                                 SiparisBilgisi = p.UnitsOnOrder
                             }).ToList();

            Urunler u = new Urunler();
            foreach (var item in suppliers)
            {

                u.ProductID = item.ProductID;
                u.ProductName = item.ProductName;
                u.SupplierID = item.SupplierID;
                u.Supplier = item.SupplierName;
                u.CategoryID = item.CategoryID;
                u.UnitsOrder = item.SiparisBilgisi;

            }
            return View(u);
            
        }
    }
}