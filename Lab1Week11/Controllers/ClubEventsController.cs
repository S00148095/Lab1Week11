using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab1Week11.Models.ClubModel;

namespace Lab1Week11.Controllers
{
    public class ClubEventsController : Controller
    {
        private ClubContext db = new ClubContext();

        // GET: ClubEvents
        public async Task<ActionResult> Index()
        {
            var clubEvents = db.ClubEvents.Include(c => c.associatedClub);
            return View(await clubEvents.ToListAsync());
        }

        // GET: ClubEvents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClubEvent clubEvent = await db.ClubEvents.FindAsync(id);
            if (clubEvent == null)
            {
                return HttpNotFound();
            }
            return View(clubEvent);
        }

        // GET: ClubEvents/Create
        public ActionResult Create()
        {
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName");
            return View();
        }

        // POST: ClubEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Venue,Location,ClubId,StartDateTime,EndDateTime")] ClubEvent clubEvent)
        {
            if (ModelState.IsValid)
            {
                db.ClubEvents.Add(clubEvent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", clubEvent.ClubId);
            return View(clubEvent);
        }

        // GET: ClubEvents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClubEvent clubEvent = await db.ClubEvents.FindAsync(id);
            if (clubEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", clubEvent.ClubId);
            return View(clubEvent);
        }

        // POST: ClubEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Venue,Location,ClubId,StartDateTime,EndDateTime")] ClubEvent clubEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clubEvent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClubId = new SelectList(db.Clubs, "ClubId", "ClubName", clubEvent.ClubId);
            return View(clubEvent);
        }

        // GET: ClubEvents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClubEvent clubEvent = await db.ClubEvents.FindAsync(id);
            if (clubEvent == null)
            {
                return HttpNotFound();
            }
            return View(clubEvent);
        }

        // POST: ClubEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClubEvent clubEvent = await db.ClubEvents.FindAsync(id);
            db.ClubEvents.Remove(clubEvent);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public PartialViewResult _ClubEvents(int id)
        {
            var qry = db.ClubEvents.Where(ce => ce.ClubId == id).ToList();
            return PartialView(qry);
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
