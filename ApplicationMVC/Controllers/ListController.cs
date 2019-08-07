using ApplicationMVC.Models.EntityFramework;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ApplicationMVC.Controllers
{
    public class ListController : Controller
    {
        TransactionsDB db = new TransactionsDB();
        // GET: List
        public ActionResult Index()
        {
            //Monthly table sent as a list
            ViewBag.List = db.Monthlies.ToList();
            return View(new Monthly());
        }
         
        [HttpPost]
        public ActionResult Add(Monthly monthly)
        {
            //Checks if modelstate is valid for validation operations
            if (!ModelState.IsValid)
            {
                ViewBag.List = db.Monthlies.ToList();
                return View("Index");
            }
            //If the add button is pressed for insert, id = 0
            if (monthly.id == 0)
            {
                //Database registration
                db.Monthlies.Add(monthly);
            }
            else
            {
                //If id value is nonzero, it is sent to add action by keeping index.cshml, finded in the database and data assigned to the updateMonthly
                var updateMonthly = db.Monthlies.Find(monthly.id);

                //If model takes a null value when id comes to a non-id, redirects to httpnotfound
                if (updateMonthly == null)
                    return HttpNotFound();

                //data in database updated with data in textbox
                updateMonthly.item = monthly.item;
                updateMonthly.money = monthly.money;
            }

            //Changes in database saved
            db.SaveChanges();

            //Redirected to index action after insertion
            return RedirectToAction("Index","List");
        }

        public ActionResult Update(int id)
        {
            //if the edit button is pressed, id from the index page is finded in the database and data of this id is assigned to the model
            var model = db.Monthlies.Find(id);

            //If model takes a null value when id comes to a non-id, redirects to httpnotfound
            if (model == null)
                return HttpNotFound();

            //Monthly table sent as a list
            ViewBag.List = db.Monthlies.ToList();
            return View("Index",model);
        }

        public ActionResult Delete(int id)
        {
            //if the edit button is pressed, id from the index page is finded in the database and data of this id is assigned to the model
            var deleteMonthly = db.Monthlies.Find(id);

            //If deleteMonthly takes a null value when id comes to a non-id, redirects to httpnotfound
            if (deleteMonthly == null)
                return HttpNotFound();

            //Removed deleteMonthly
            db.Monthlies.Remove(deleteMonthly);
            db.SaveChanges();

            //Monthly table sent as a list
            ViewBag.List = db.Monthlies.ToList();
            return View("Index");
        }


        /// <summary>
        /// Localization operations
        /// </summary>
        /// <param name="language">Sended as a language option from wiev to controller</param>
        public ActionResult ChangeLanguage(string language)
        {
            HttpCookie cookie;
            cookie = new HttpCookie("MultiLanguage");
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            cookie.Value = language;
            Response.SetCookie(cookie);
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.LocalPath);
            return RedirectToAction("Index", "List");
        }

    }
}