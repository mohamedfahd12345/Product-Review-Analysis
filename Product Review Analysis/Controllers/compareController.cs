using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Product_Review_Analysis.Models;

namespace Product_Review_Analysis.Controllers
{
    [Authorize]
    public class compareController : Controller
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
        public IActionResult Index(int id)
        {
            checkuser();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            var db = new ProductReviewAnalysisContext();
            var tareget_user_case = db.Compares.Where(x => x.UserId == userId).ToList();
            if (tareget_user_case == null ||tareget_user_case.Count==0)
            {
                var new_compare = new Compare();
                new_compare.UserId = userId;
                new_compare.ProductId = id;
                db.Compares.Add(new_compare);
                db.SaveChanges();

                return Redirect("/category/index");
            }
            else if (tareget_user_case.Count == 1)
            {
                var new_compare = new Compare();
                new_compare.UserId = userId;
                new_compare.ProductId = id;
                db.Compares.Add(new_compare);
                db.SaveChanges();
                //////////------------part 2--------------------------/////////////////
               
                var  tareget_user = db.Compares.Where(x => x.UserId == userId).ToList();
                var list_product = new List<Product>();
                foreach(var com in tareget_user)
                {
                    var product = db.Products.Where(x => x.Id == com.ProductId).FirstOrDefault();
                    list_product.Add(product);
                }

                //////////----------part 3  delete 2 ---------------//////////////
                var list_target = db.Compares.Where(x => x.UserId == userId).ToList();
                foreach (var item in list_target)
                {
                    db.Compares.Remove(item);
                    db.SaveChanges();
                }


                return View(list_product);
            }
           

            return View();
        }
    }
}
