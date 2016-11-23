using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jerry.Models;

namespace Jerry.Controllers
{
    public class ReservacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservacion
        public async Task<ActionResult> Index()
        {
            var reservaciones = db.reservaciones.Include(r => r.cliente).Include(r => r.salon);
            return View(await reservaciones.ToListAsync());
        }

        // GET: Reservacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = await db.reservaciones.FindAsync(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservacion/Create
        public ActionResult Create()
        {
            ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre");
            ViewBag.salonID = new SelectList(db.salones, "salonID", "nombre");
            return View();
        }

        // POST: Reservacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "reservacionID,fechaReservacion,fechaEventoInicial,fechaEventoFinal,costo,Detalles,salonID,clienteID")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.reservaciones.Add(reservacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre", reservacion.clienteID);
            ViewBag.salonID = new SelectList(db.salones, "salonID", "nombre", reservacion.salonID);
            return View(reservacion);
        }

        // GET: Reservacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = await db.reservaciones.FindAsync(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre", reservacion.clienteID);
            ViewBag.salonID = new SelectList(db.salones, "salonID", "nombre", reservacion.salonID);
            return View(reservacion);
        }

        // POST: Reservacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "reservacionID,fechaReservacion,fechaEventoInicial,fechaEventoFinal,costo,Detalles,salonID,clienteID")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre", reservacion.clienteID);
            ViewBag.salonID = new SelectList(db.salones, "salonID", "nombre", reservacion.salonID);
            return View(reservacion);
        }

        // GET: Reservacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = await db.reservaciones.FindAsync(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reservacion reservacion = await db.reservaciones.FindAsync(id);
            db.reservaciones.Remove(reservacion);
            await db.SaveChangesAsync();
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
        public ActionResult CreateReservacion(int id)
        {
            ViewBag.clienteID = id;
            ViewBag.salonID = new SelectList(db.salones, "salonID", "nombre");
            return View();
        }

    }
}
