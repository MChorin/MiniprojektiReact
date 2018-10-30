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
    public class KuvatController : ApiController
    {
        private MPdbModel db = new MPdbModel();

        public KuvatController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Kuvat
        public IQueryable<Kuvat> GetKuvat()
        {
            return db.Kuvat;
        }

        // GET: api/Kuvat/5
        [ResponseType(typeof(Kuvat))]
        public IHttpActionResult GetKuvat(int id)
        {
            Kuvat kuvat = db.Kuvat.Find(id);
            if (kuvat == null)
            {
                return NotFound();
            }

            return Ok(kuvat);
        }

        // PUT: api/Kuvat/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKuvat(int id, Kuvat kuvat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kuvat.Kuva_id)
            {
                return BadRequest();
            }

            db.Entry(kuvat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KuvatExists(id))
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

        // POST: api/Kuvat
        [ResponseType(typeof(Kuvat))]
        public IHttpActionResult PostKuvat(Kuvat kuvat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kuvat.Add(kuvat);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kuvat.Kuva_id }, kuvat);
        }

        // DELETE: api/Kuvat/5
        [ResponseType(typeof(Kuvat))]
        public IHttpActionResult DeleteKuvat(int id)
        {
            Kuvat kuvat = db.Kuvat.Find(id);
            if (kuvat == null)
            {
                return NotFound();
            }

            db.Kuvat.Remove(kuvat);
            db.SaveChanges();

            return Ok(kuvat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KuvatExists(int id)
        {
            return db.Kuvat.Count(e => e.Kuva_id == id) > 0;
        }
    }
}