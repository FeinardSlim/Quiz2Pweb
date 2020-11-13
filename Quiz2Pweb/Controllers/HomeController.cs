using Quiz2Pweb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace Quiz2Pweb.Controllers
{
    public class HomeController : Controller
    {

        private ArticleDBContext db = new ArticleDBContext();
        public ActionResult Index(string Category, string searchIndex, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var CategoryList = new List<string>();

            var CategoryQuery = from d in db.Articles
                                orderby d.Category
                                select d.Category;

            CategoryList.AddRange(CategoryQuery.Distinct());
            ViewBag.Category = new SelectList(CategoryList);

            var artikel = from m in db.Articles select m;

            if (!String.IsNullOrEmpty(searchIndex))
            {
                artikel = artikel.Where(s => s.Title.Contains(searchIndex));
            }
            if (!String.IsNullOrEmpty(Category))
            {
                artikel = artikel.Where(s => s.Category == Category);
            }
                    
            
            
            
            artikel = artikel.OrderByDescending(s => s.ReleaseDate);


            // return View(artikel.ToList());
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(artikel.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "ARCO (Artikel Corona)";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
    }
}