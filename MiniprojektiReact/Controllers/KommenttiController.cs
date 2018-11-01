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
using Microsoft.AspNet.Identity;
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

        //GET: api/Kommentti
        [AllowAnonymous]
        public IQueryable<Kommentti> GetKommentti()
        {
            return db.Kommentti.OrderByDescending(pvm => pvm.Aikaleima).Take(100);
        }



        // GET: api/Kommentti/5
        //[ResponseType(typeof(Kommentti))]
        [AllowAnonymous]
        public IQueryable<Kommentti> GetKommentti(int id)
        {
        //    Kommentti kommentti = db.Kommentti.Find(id);
        //    if (kommentti == null)
        //    {
        //        return NotFound();
        //    }

           return db.Kommentti.Where(a => a.Paikka_id == id).OrderByDescending(pvm => pvm.Aikaleima).Take(100);
        }

// GET: api/Kommentti/5
//[ResponseType(typeof(Kommentti))]
//[ActionName("paikkaID")]
//        //[ResponseType(typeof(IEnumerable<Kommentti>))]
//        public IEnumerable<Kommentti> GetPaikkaKommentti(int id)
//        {
//            //List<Kommentti> kommentit = new List<Kommentti>();

//            IEnumerable<Kommentti> kommentit = db.Kommentti.Where(a => a.Paikka_id == id).OrderByDescending(pvm => pvm.Aikaleima);

//            //foreach(var jotain in haetut)
//            //{
//            //    kommentit.Add(jotain);
//            //}

//            //if (kommentit is null)
//            //{
//            //    return NotFound();
//            //}

//            return kommentit;

//        }
                
                
        // PUT: api/Kommentti/5
        [ResponseType(typeof(void))]
        [Authorize]
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
        //[Authorize]
        public IHttpActionResult PostKommentti(Kommentti kommentti)
        {

            kommentti.Aikaleima = DateTime.Now;
            //var id = User.Identity.GetUserId();
            kommentti.Kayttaja_id = 1; //kunnes identifiointi toimii
            kommentti.OnkoKuva = false;
            //kommentti.Paikka_id = 3; 
            //update paikka-tauluun kommenttien määrä ja summa
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            Paikka paikka = db.Paikka.Find(kommentti.Paikka_id);
            db.Paikka.Attach(paikka);
            paikka.KommenttienMaara = paikka.KommenttienMaara + 1;
            paikka.ArvostelujenSumma = paikka.ArvostelujenSumma + kommentti.Arvosana;
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

            return CreatedAtRoute("DefaultApi", new { id = kommentti.Kommentti_id }, kommentti);
        }

        // DELETE: api/Kommentti/5
        [ResponseType(typeof(Kommentti))]
        [Authorize]
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