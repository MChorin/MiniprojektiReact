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
using GoogleMaps.LocationServices;
using MiniprojektiReact.Models;

namespace MiniprojektiReact.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
            //IEnumerable<Paikka> paikat = db.Paikka;
            //foreach(var jotain in paikat)
            //{
            //    float ka = ((float)jotain.ArvostelujenSumma / jotain.KommenttienMaara);
            //}

            return db.Paikka.OrderByDescending(a=> ((float)a.ArvostelujenSumma / a.KommenttienMaara)).Take(100);
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

        //GET Kaupungilla: api/paikka/kaupunki/{kaupungin nimi} (eli hakuehto= kaupungin nimi)
        [ResponseType(typeof(IEnumerable<Paikka>))]
        public IHttpActionResult GetPaikkaKaupunki(string hakuehto)
        {
           IEnumerable<Paikka> paikat = db.Paikka.Where(t => t.Kaupunki == hakuehto).OrderByDescending(a => (a.ArvostelujenSumma / a.KommenttienMaara)).Take(100);

            if (paikat is null)
            {
                return NotFound();
            }



            return Ok(paikat);
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
            //var location = new GoogleLocationService();
            //var point = location.GetLatLongFromAddress(paikka.Kaupunki + ", " + paikka.Maa);

            paikka.Kayttaja_id = 1; //kunnes identifikointi toimii
            paikka.KommenttienMaara = 0;
            paikka.ArvostelujenSumma = 0;
            //paikka.Longitude = point.Latitude; //ehkä toimii... :D
            //paikka.Latitude = point.Longitude; 

            paikka.Longitude = null;
            paikka.Latitude = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Paikka.Add(paikka);
         
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaikkaExists(paikka.Paikka_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

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