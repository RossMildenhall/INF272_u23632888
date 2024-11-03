using h3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace h3.Controllers
{
    public class HomeController : Controller
    {

        private LibraryEntities db = new LibraryEntities();

        public async Task<ActionResult>Index()
        {
            var viewModel = new CombinedViewModel
            {
                students = await db.students.ToListAsync(),
                books = await db.books
                    .Include(b => b.authors)  // Include author data
                    .Include(b => b.types)    // Include type data
                    .ToListAsync(),
                authors = await db.authors.ToListAsync(),
                types = await db.types.ToListAsync(),
                borrows = await db.borrows.ToListAsync()
            };

            return View(viewModel);
        }

        // GET: students
        public async Task<ActionResult> studentsIndex()
        {
            return View(await db.students.ToListAsync());
        }

        // GET: students/Details/5
        public async Task<ActionResult> studentsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            students student = await db.students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: students/Create
        public ActionResult studentsCreate()
        {
            return PartialView();
        }

        // POST: students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> studentsCreate([Bind(Include = "studentId,name,surname,birthdate,gender,class,point")] students student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return PartialView(student);
        }

        // GET: students/Edit/5
        public async Task<ActionResult> studentsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            students student = await db.students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> studentsEdit([Bind(Include = "studentId,name,surname,birthdate,gender,class,point")] students student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("studentsIndex");
            }
            return View(student);
        }

        // GET: students/Delete/5
        public async Task<ActionResult> studentsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            students student = await db.students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("studentsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> studentsDeleteConfirmed(int id)
        {
            students student = await db.students.FindAsync(id);
            db.students.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction("studentsIndex");
        }

        // GET: books
        public async Task<ActionResult> booksIndex()
        {
            return View(await db.books.ToListAsync());
        }

        // GET: books/Details/5
        public async Task<ActionResult> booksDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            books book = await db.books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: books/Create
        public ActionResult booksCreate()
        {
            return View();
        }

        // POST: books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> booksCreate([Bind(Include = "bookId,name,pagecount,point,authorId,typeId")] books book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("booksIndex");
            }

            return View(book);
        }

        // GET: books/Edit/5
        public async Task<ActionResult> booksEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            books book = await db.books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> booksEdit([Bind(Include = "bookId,name,pagecount,point,authorId,typeId")] books book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("booksIndex");
            }
            return View(book);
        }

        // GET: books/Delete/5
        public async Task<ActionResult> booksDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            books book = await db.books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("booksDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> booksDeleteConfirmed(int id)
        {
            books book = await db.books.FindAsync(id);
            db.books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("booksIndex");
        }

        public async Task<ActionResult> About()
        {
            var viewModel = new MaintainedViewModel
            {
                authors = await db.authors.ToListAsync(),
                types = await db.types.ToListAsync(),
                borrows = await db.borrows
                    .Include(b => b.students)  // Include student data
                    .Include(b => b.books)     // Include book data
                    .ToListAsync(),
                students = await db.students.ToListAsync(),
                books = await db.books.ToListAsync()
            };

            return View(viewModel);
        }

        // Add this after your About action method
        public async Task<ActionResult> Contact()
        {
            var viewModel = new ReportViewModel
            {
                students = await db.students.ToListAsync(),
                books = await db.books
                    .Include(b => b.authors)
                    .Include(b => b.types)
                    .ToListAsync(),
                borrows = await db.borrows
                    .Include(b => b.students)
                    .Include(b => b.books)
                    .ToListAsync()
            };

            return View(viewModel);
        }

    }
}