using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonalLibrary.Models;
using PersonalLibrary.Filters;

namespace PersonalLibrary.Controllers
{
    public class EmprestimoController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Emprestimo
        [AuthFilter]
        public ActionResult Index()
        {
            using(LibraryContext ctx = new LibraryContext())
            {
                Usuario user = (Usuario)Session["usuario"];
                int id = user.UsuarioId;

                //ctx.Emprestimo.Include(x => x.Livro) 
                //Para carregar o objeto virtual
                List<Emprestimo> lista = ctx.Emprestimo.Include(x => x.Livro).Where(c => c.UsuarioId == id).ToList();
                return View(lista);
            }

            //var emprestimo = db.Emprestimo.Include(e => e.Titulo).Include(e => e.Usuario);
            //return View(emprestimo.ToList());
        }

        // GET: Emprestimo/Details/5
        [AuthFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // GET: Emprestimo/Create
        [AuthFilter]
        public ActionResult Create()
        {

            using (LibraryContext ctx = new LibraryContext())
            {
                Usuario u = (Usuario)Session["usuario"];
                ViewBag.LivroId = new SelectList(db.Livro.Where(c => c.UsuarioId == u.UsuarioId), "LivroId", "Titulo");
                return View();
            }
        }

        // POST: Emprestimo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public ActionResult Create([Bind(Include = "EmprestimoId,Emprestado,PessoaEmprestimo,DataEmprestimo,DataDevolucao,LivroId,UsuarioId")] Emprestimo emprestimo)
        {
            using (LibraryContext ctx = new LibraryContext())
            {
                if (ModelState.IsValid)
                {
                    Usuario u = Session["usuario"] as Usuario;
                    emprestimo.UsuarioId = u.UsuarioId;

                    db.Emprestimo.Add(emprestimo);
                    db.SaveChanges();

                    ViewBag.LivroId = new SelectList(ctx.Usuario.Where(c => c.UsuarioId == u.UsuarioId));

                    return RedirectToAction("Index");
                }

                //ViewBag.LivroId = new SelectList(db.Livro, "LivroId", "Titulo", emprestimo.LivroId);
                
                return View(emprestimo);
            }
        }

        // GET: Emprestimo/Edit/5
        [AuthFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }

            using (LibraryContext ctx = new LibraryContext())
            {
                Usuario u = (Usuario)Session["usuario"];
                ViewBag.LivroId = new SelectList(db.Livro.Where(c => c.UsuarioId == u.UsuarioId), "LivroId", "Titulo");
                return View(emprestimo);
            }
            //ViewBag.LivroId = new SelectList(db.Livro, "LivroId", "Titulo", emprestimo.LivroId);
           //ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome", emprestimo.UsuarioId);
            //return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public ActionResult Edit([Bind(Include = "EmprestimoId,Emprestado,PessoaEmprestimo,DataEmprestimo,DataDevolucao,LivroId,UsuarioId")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                Usuario u = Session["usuario"] as Usuario;
                emprestimo.UsuarioId = u.UsuarioId;

                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LivroId = new SelectList(db.Livro, "LivroId", "Titulo", emprestimo.LivroId);
            ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome", emprestimo.UsuarioId);
            return View(emprestimo);
        }

        // GET: Emprestimo/Delete/5
        [AuthFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            db.Emprestimo.Remove(emprestimo);
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
