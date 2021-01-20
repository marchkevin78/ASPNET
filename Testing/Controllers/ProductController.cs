using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testing.Models;
using PagedList;




// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Testing.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }


        // GET: /<controller>/
        public IActionResult Index(string searchString)
        {

            var products = _repo.GetAllProducts();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            return View(products);

            
        }

        public IActionResult ViewProduct(int id)
        {
            var product = _repo.GetProduct(id);

            return View(product);
        }

        public IActionResult UpdateProduct(int id)
        {
            Product prod = _repo.GetProduct(id);

            if (prod == null)
            {
                return View("ProductNotFound");
            }

            return View(prod);
        }

        public IActionResult UpdateProductToDatabase(Product product)
        {
            _repo.UpdateProduct(product);

            return RedirectToAction("ViewProduct", new { id = product.ProductID });
        }

        public IActionResult InsertProduct()
        {
            var prod = _repo.AssignCategory();

            return View(prod);
        }

        public IActionResult InsertProductToDatabase(Product productToInsert)
        {
            _repo.InsertProduct(productToInsert);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(Product product)
        {
            _repo.DeleteProduct(product);

            return RedirectToAction("Index");
        }
    }
}
