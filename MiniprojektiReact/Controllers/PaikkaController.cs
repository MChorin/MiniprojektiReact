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
    public class PaikkaController : ApiController
    {
        private MPdbModel db = new MPdbModel();

        public PaikkaController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Paikka
        public IQueryable<Paikka> GetPaikka()

        {
            return db.Paikka;
        }

        // GET: api/Paikka/5
        [ResponseType(typeof(Paikka))]
        public IHttpActionResult GetPaikka(int id)
        {
            Paikka paikka = db.Paikka.Find(id);
            if (paikka == null)
            {
                return NotFound();
            }

            return Ok(paikka);
        }

        // PUT: api/Paikka/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaikka(int id, Paikka paikka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paikka.Paikka_id)
            {
                return BadRequest();
            }

            db.Entry(paikka).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaikkaExists(id))
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

        // POST: api/Paikka
        [ResponseType(typeof(Paikka))]
        public IHttpActionResult PostPaikka(Paikka paikka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paikka.Add(paikka);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = paikka.Paikka_id }, paikka);
        }

        // DELETE: api/Paikka/5
        [ResponseType(typeof(Paikka))]
        public IHttpActionResult DeletePaikka(int id)
        {
            Paikka paikka = db.Paikka.Find(id);
            if (paikka == null)
            {
                return NotFound();
            }

            db.Paikka.Remove(paikka);
            db.SaveChanges();

            return Ok(paikka);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaikkaExists(int id)
        {
            return db.Paikka.Count(e => e.Paikka_id == id) > 0;
        }
    }
}