using Microsoft.AspNetCore.Mvc;
using Product_Review_Analysis.Models;

namespace Product_Review_Analysis.Areas.admin.Controllers
{
    [Area("admin")]
    public class productController : Controller
    {

        public IActionResult index(int id)
        {
            var db = new ProductReviewAnalysisContext();
            var details_of_product = db.Products.Where(x => x.Id == id).FirstOrDefault();
            return View(details_of_product);
        }

        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(Product product)
        {
            var db = new ProductReviewAnalysisContext();
            var new_product = new Product();
            new_product.CategoryId=product.CategoryId;
            new_product.Name=product.Name;
            new_product.Description=product.Description;
            new_product.Price=product.Price;
            new_product.Photo=product.Photo;
            db.Products.Add(new_product);
            db.SaveChanges();
            Console.WriteLine(product.CategoryId);
            return Redirect("/admin/product/create");
        }
        public IActionResult Edit(int id)
        {
            var db = new ProductReviewAnalysisContext();
            var target_product = db.Products.Where(x => x.Id == id).FirstOrDefault();
            return View(target_product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var db = new ProductReviewAnalysisContext();
            var target_product = db.Products.Where(x => x.Id == product.Id).FirstOrDefault();
            target_product.Name = product.Name;
            target_product.Price = product.Price;
            target_product.CategoryId = product.CategoryId;
            target_product.Photo = product.Photo;
            target_product.Description = product.Description;
            db.Products.Update(target_product);
            db.SaveChanges();
            return Redirect("/admin/categories/index");

        }
        public IActionResult Delete(int id)
        {
            var db = new ProductReviewAnalysisContext();
            var target_product = db.Products.Where(x => x.Id == id).FirstOrDefault();
            db.Products.Remove(target_product);
            db.SaveChanges();
            return Redirect("/admin/categories/index");
        }
    }
}
