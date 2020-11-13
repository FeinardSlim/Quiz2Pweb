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
    public class ArticlesController : Controller
    {
        private ArticleDBContext db = new ArticleDBContext();

        // GET: Articles
        /*        public ActionResult Index()
                {
                    return View(db.Articles.ToList());
                }*/
        [Authorize]
        public ActionResult Index(string Category, string searchIndex ,string sortOrder)
        {
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

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ReleaseDateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CategorySortParam = sortOrder == "Category" ? "category_desc" : "Category";

            switch (sortOrder)
            {
                case "name_desc":
                    artikel = artikel.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    artikel = artikel.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    artikel = artikel.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "Category":
                    artikel = artikel.OrderBy(s => s.Category);
                    break;
                case "category_desc":
                    artikel = artikel.OrderByDescending(s => s.Category);
                    break;
                default:
                    artikel = artikel.OrderBy(s => s.Title);
                    break;
            }
            return View(artikel.ToList());

            // return View(artikel);
        }

        [HttpPost]
        public string Index(FormCollection fc, string searchIndex)
        {
            return "<h3> From [HttpPost]Index: " + searchIndex + "</h3>";
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

        // GET: Articles/Create
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Category,Body")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Category,Body")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
