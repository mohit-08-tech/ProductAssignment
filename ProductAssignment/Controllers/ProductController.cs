using ProductAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductAssignment.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            ProductQuery query = new ProductQuery();
            List<Product> allitems = query.LoadAllProduct();
            return View(allitems);
        }
        [HttpPost]
        public ActionResult Index(string ProductName, string ProductSize, string ProductPrice, string MFGDate, string ProductCategory, string SearchConj) 
        {

            Product product = new Product();
            var allitems = new List<Product>();
            int count = 0;
            if (!String.IsNullOrEmpty(ProductName))
            {
                product.ProductName = ProductName;
                count++;
            }
            if (!String.IsNullOrEmpty(ProductSize))
            {
                product.Size = ProductSize;
                count++;
            }
            if (!String.IsNullOrEmpty(ProductPrice))
            {
                product.Price = Double.Parse(ProductPrice);
                count++;
            }
            if (!String.IsNullOrEmpty(MFGDate))
            {
                product.MFGDate = DateTime.Parse(MFGDate);
                count++;
            }
            if (!String.IsNullOrEmpty(ProductCategory))
            {
                product.Category = ProductCategory;
                count++;
            }

            if (!String.IsNullOrEmpty(SearchConj))
            {
                product.SearchWith = SearchConj;
            }

            if ((count > 1 || count == 0) && String.IsNullOrEmpty(SearchConj))
            {
                ViewBag.Error = "No Data Found!!";
            }
            else
            {
                ProductQuery query = new ProductQuery();
                allitems = query.SearchProduct(product);
            }   
            return View(allitems);
        }
    }
}