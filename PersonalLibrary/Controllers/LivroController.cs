using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonalLibrary.Models;

namespace PersonalLibrary.Controllers
{
    public class LivroController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Livro
        public ActionResult Index()
        {
            using(LibraryContext ctx = new LibraryContext())
            {
                Usuario user = (Usuario)Session["usuario"];
                int id = user.UsuarioId;

                List<Livro> lista = ctx.Livro.Where(c => c.UsuarioId == id).ToList();
                return View(lista);
            }

            //var livro = db.Livro.Include(l => l.Autor).Include(l => l.Usuario);
            //return View(livro.ToList());
        }

        // GET: Livro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            using (LibraryContext ctx = new LibraryContext()) {
                Usuario u = (Usuario)Session["usuario"];

                ViewBag.AutorId = new SelectList(db.Autor.Where(c => c.UsuarioId == u.UsuarioId), "AutorId", "Nome");
                return View();
            }
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LivroId,Titulo,ISBN,DataCompra,StatusLido,AutorId,UsuarioId")] Livro livro)
        {
            using (LibraryContext ctx = new LibraryContext())
            {
                if (ModelState.IsValid)
                {
                    Usuario u = (Usuario)Session["usuario"];
                    livro.UsuarioId = u.UsuarioId;

                    if(livro.AutorId.Equals(0))
                    {
                        ViewBag.Erro = "É necessário cadastrar o Primeiro Autor antes de cadastrar um Livro!";
                        return View();
                    }else
                    {
                        db.Livro.Add(livro);
                        db.SaveChanges();
                    }                   

                    ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
                    return RedirectToAction("Index");
                }
                
                
                //ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
                return View(livro);
            }
        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
            ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome", livro.UsuarioId);
            return View(livro);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LivroId,Titulo,ISBN,DataCompra,StatusLido,AutorId,UsuarioId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
            ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome", livro.UsuarioId);
            return View(livro);
        }

        // GET: Livro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livro.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livro.Find(id);
            db.Livro.Remove(livro);
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
