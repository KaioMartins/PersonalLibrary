﻿using System;
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
    public class LivroController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Livro
        [AuthFilter]
        public ActionResult Index()
        {
            using(LibraryContext ctx = new LibraryContext())
            {
                Usuario user = (Usuario)Session["usuario"];
                int id = user.UsuarioId;

                //ctx.Livro.Include(x => x.Autor)
                //Para carregar o objeto virtual
                List<Livro> lista = ctx.Livro.Include(x => x.Autor).Where(c => c.UsuarioId == id).ToList();
                return View(lista);
            }

            //var livro = db.Livro.Include(l => l.Autor).Include(l => l.Usuario);
            //return View(livro.ToList());
        }

        // GET: Livro/Details/5
        [AuthFilter]
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
            db.Entry(livro).Reference(e => e.Autor).Load();
            return View(livro);
        }

        // GET: Livro/Create
        [AuthFilter]
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
        [AuthFilter]
        public ActionResult Create([Bind(Include = "LivroId,Titulo,ISBN,DataCompra,StatusLido,AutorId,UsuarioId")] Livro livro)
        {
            using (LibraryContext ctx = new LibraryContext())
            {
                if (ModelState.IsValid)
                {
                    Usuario u = (Usuario)Session["usuario"];
                    livro.UsuarioId = u.UsuarioId;

                    try
                    {
                        db.Livro.Add(livro);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ViewBag.Erro = "É necessário cadastrar o Primeiro Autor antes de cadastrar um Livro!";
                        return View();
                    }
                    
                    //if(livro.AutorId.Equals(0))
                    //{
                    //    ViewBag.Erro = "É necessário cadastrar o Primeiro Autor antes de cadastrar um Livro!";
                    //    return View();
                    //}else
                    //{
                    //    db.Livro.Add(livro);
                    //    db.SaveChanges();
                    //}                   

                    ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
                    return RedirectToAction("Index");
                }
                
                
                //ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
                return View(livro);
            }
        }

        // GET: Livro/Edit/5
        [AuthFilter]
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

            Usuario u = (Usuario)Session["usuario"];

            ViewBag.AutorId = new SelectList(db.Autor.Where(c => c.UsuarioId == u.UsuarioId), "AutorId", "Nome");
            //ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
            //ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome", livro.UsuarioId);
            return View(livro);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public ActionResult Edit([Bind(Include = "LivroId,Titulo,ISBN,DataCompra,StatusLido,AutorId,UsuarioId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                Usuario u = (Usuario)Session["usuario"];
                livro.UsuarioId = u.UsuarioId;

                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.AutorId = new SelectList(db.Autor, "AutorId", "Nome", livro.AutorId);
            //ViewBag.UsuarioId = new SelectList(db.Usuario, "UsuarioId", "Nome", livro.UsuarioId);
            return View(livro);
        }

        // GET: Livro/Delete/5
        [AuthFilter]
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
            db.Entry(livro).Reference(e => e.Autor).Load();
           
            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livro.Find(id);

            try
            {
                db.Livro.Remove(livro);
                db.SaveChanges();
            }
            catch(Exception)
            {
                ViewBag.Erro = "Este livro está emprestado. Exclua o empréstimo antes de excluir este livro.";
                return View();
            }           
            
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
