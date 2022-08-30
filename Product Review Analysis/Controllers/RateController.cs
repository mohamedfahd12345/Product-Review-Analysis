using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Product_Review_Analysis.Models;

namespace Product_Review_Analysis.Controllers
{
    [Authorize]
	public class RateController : Controller
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

        public IActionResult Index(Product product)
		{
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var db = new ProductReviewAnalysisContext();
            var new_rate = new Rateing();
            new_rate.ProductId = product.Id;
            new_rate.UserId = userId;
            new_rate.Rate = (float)product.Price;
            db.Rateings.Add(new_rate);
            db.SaveChanges();

            return Redirect("/category/index");

		}
	}
}
