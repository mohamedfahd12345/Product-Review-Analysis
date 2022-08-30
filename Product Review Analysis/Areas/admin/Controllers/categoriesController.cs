using Microsoft.AspNetCore.Mvc;
using Product_Review_Analysis.Models;

namespace Product_Review_Analysis.Areas.admin.Controllers
{
    [Area("admin")]
    public class categoriesController : Controller
    {
        public IActionResult Index()
        {
            var db = new ProductReviewAnalysisContext();
            var list_category = db.Categories.ToList();
            return View(list_category);
            return View();
        }

       
    }
}
