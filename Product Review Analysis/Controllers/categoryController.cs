using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Product_Review_Analysis.Models;

namespace Product_Review_Analysis.Controllers
{
    [Authorize]
    public class categoryController : Controller
    {
        public void checkuser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            var db = new ProductReviewAnalysisContext();
            var item = db.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (item == null)
            {
                var user = new User();
                user.Id = userId;
                user.Email = userName;
                db.Users.Add(user);
                db.SaveChanges();
            }
            ////////////////////////////////////////////////////////////////////////
        }

        public IActionResult Index()
        {
            checkuser();
            var db = new ProductReviewAnalysisContext();
            var list_category = db.Categories.ToList();
            return View(list_category);
            
        }
    }
}
