using Microsoft.AspNetCore.Mvc;
using Product_Review_Analysis.Models;

namespace Product_Review_Analysis.Areas.admin.Controllers
{
    [Area("admin")]
    public class allproductsController : Controller
    {
        public IActionResult Index(int id)
        {
            var db = new ProductReviewAnalysisContext();
            var list_products=db.Products.Where(x=>x.CategoryId == id).ToList();    
            return View(list_products);
        }
    }
}
