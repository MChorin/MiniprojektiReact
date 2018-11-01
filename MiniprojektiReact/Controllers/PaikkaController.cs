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
using Microsoft.AspNet.Identity;
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
        public List<Paikka> GetPaikka()
        {
            List<Paikka> lähetettävät = new List<Paikka>();

            IQueryable<Paikka> paikat = db.Paikka;
            foreach (var paik in paikat)
            {
                if (paik.KommenttienMaara == 0)
                {
                    paik.KommenttienMaara = 1;
                    lähetettävät.Add(paik);
                }

                else
                {
                    lähetettävät.Add(paik);
                }

            }
            List<Paikka> pois = new List<Paikka>(lähetettävät.OrderByDescending(a => ((float)a.ArvostelujenSumma / a.KommenttienMaara)).Take(100));

            return pois;
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
            List<Paikka> lähetettävät = new List<Paikka>();

            IQueryable<Paikka> paikat = db.Paikka.Where(a => (a.Kaupunki == hakuehto));
            foreach (var paik in paikat)
            {
                if (paik.KommenttienMaara == 0)
                {
                    paik.KommenttienMaara = 1;
                    lähetettävät.Add(paik);
                }

                else
                {
                    lähetettävät.Add(paik);
                }

            }

            //IEnumerable<Paikka> paikat = db.Paikka.Where(t => t.Kaupunki == hakuehto).OrderByDescending(a => ((float)a.ArvostelujenSumma / (a.KommenttienMaara.GetValue())).Take(100);

            if (paikat is null)
            {
                return NotFound();
            }

            return Ok(lähetettävät.OrderByDescending(a => ((float)a.ArvostelujenSumma / a.KommenttienMaara)).Take(100));
        }



        // PUT: api/Paikka/5
        [ResponseType(typeof(void))]
        //[Authorize]
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
        // [Authorize]
        public IHttpActionResult PostPaikka(Paikka paikka)
        {
            //var location = new GoogleLocationService();
            //var point = location.GetLatLongFromAddress(paikka.Kaupunki + ", " + paikka.Maa);
            paikka.Kayttaja_id = 1;
            // paikka.Kayttaja_id = User.Identity.GetUserId<int>(); //kunnes identifikointi toimii
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
        // [Authorize]
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