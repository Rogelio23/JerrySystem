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
using System.Net.Mail;
using System.IO;

namespace Jerry.Controllers
{
    public class CorreoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Correo
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Correos.ToListAsync());
        }

        // GET: Correo/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correos.FindAsync(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // GET: Correo/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Correo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "correoID,To,Subject,Body")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Correos.Add(correo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(correo);
        }

        // GET: Correo/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correos.FindAsync(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // POST: Correo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "correoID,To,Subject,Body")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(correo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(correo);
        }

        // GET: Correo/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Correo correo = await db.Correos.FindAsync(id);
            if (correo == null)
            {
                return HttpNotFound();
            }
            return View(correo);
        }

        // POST: Correo/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Correo correo = await db.Correos.FindAsync(id);
            db.Correos.Remove(correo);
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



        /// <summary>
        /// Send Mail with hotmail
        /// </summary>
        /// <param name="objModelMail">MailModel Object, keeps all properties</param>
        /// <param name="fileUploader">Selected file data, example-filename,content,content type(file type- .txt,.png etc.),length etc.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult EnviarCorreo(Jerry.Models.Correo objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                string from = "rogelio_135@hotmail.com"; //example:- sourabh9303@gmail.com
                using (MailMessage mail = new MailMessage(from, objModelMail.To))
                {
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    if (fileUploader != null)
                    {
                        string fileName = Path.GetFileName(fileUploader.FileName);
                        mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "baltasar153");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    //return View("EnviarCorreo", objModelMail);
                    return RedirectToAction("Index", "Reservacion");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult EnviarCorreo()
        {
            return View();
        }



    }
}
