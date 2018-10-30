using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using MiniprojektiReact.Models;

namespace MiniprojektiReact.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class KommenttiController : ApiController
    {
        private MPdbModel db = new MPdbModel();

        public KommenttiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Kommentti
        public IQueryable<Kommentti> GetKommentti(int paikka_id)
        {
            return db.Kommentti.OrderByDescending(pvm => pvm.Aikaleima).Where(a => a.Paikka_id == paikka_id);
        }

        // GET: api/Kommentti/5
        //[ResponseType(typeof(Kommentti))]
        //public IHttpActionResult GetKommentti(int id)
        //{
        //    Kommentti kommentti = db.Kommentti.Find(id);
        //    if (kommentti == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(kommentti);
        //}

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

            kommentti.Aikaleima = DateTime.Now;
            kommentti.Kayttaja_id = 1; //kunnes identifiointi toimii
            kommentti.OnkoKuva = false;
            //update paikka-tauluun kommenttien määrä ja summa

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kommentti.Add(kommentti);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KommenttiExists(kommentti.Kommentti_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

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