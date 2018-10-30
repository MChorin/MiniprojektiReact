using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MiniprojektiReact.Models;

namespace MiniprojektiReact.Controllers
{
    public class KommenttiController : ApiController
    {
        private MPdbModel db = new MPdbModel();

        public KommenttiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Kommentti
        public IQueryable<Kommentti> GetKommentti()
        {
            return db.Kommentti.OrderByDescending(pvm => pvm.Aikaleima);
        }

        // GET: api/Kommentti/5
        [ResponseType(typeof(Kommentti))]
        public IHttpActionResult GetKommentti(int id)
        {
            Kommentti kommentti = db.Kommentti.Find(id);
            if (kommentti == null)
            {
                return NotFound();
            }

            return Ok(kommentti);
        }

        // PUT: api/Kommentti/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKommentti(int id, Kommentti kommentti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kommentti.Kommentti_id)
            {
                return BadRequest();
            }

            db.Entry(kommentti).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KommenttiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kommentti
        [ResponseType(typeof(Kommentti))]
        public IHttpActionResult PostKommentti(Kommentti kommentti)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kommentti.Add(kommentti);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kommentti.Kommentti_id }, kommentti);
        }

        // DELETE: api/Kommentti/5
        [ResponseType(typeof(Kommentti))]
        public IHttpActionResult DeleteKommentti(int id)
        {
            Kommentti kommentti = db.Kommentti.Find(id);
            if (kommentti == null)
            {
                return NotFound();
            }

            db.Kommentti.Remove(kommentti);
            db.SaveChanges();

            return Ok(kommentti);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KommenttiExists(int id)
        {
            return db.Kommentti.Count(e => e.Kommentti_id == id) > 0;
        }
    }
}