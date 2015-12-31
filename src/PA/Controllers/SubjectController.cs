using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using PA.Models;
using PA.Models.Entities;
using Microsoft.Data.Entity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PA.API
{
    //http://docs.asp.net/projects/mvc/en/latest/getting-started/first-web-api.html

    [Route("api/[controller]")]
    public class SubjectController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Subject> Get()
        {
            using (var db = new PADbContext())
            {               
                return db.SubjectSet.Where(r => r.Enabled).ToArray();
            }            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            using (var db = new PADbContext())
            {
                var item = db.SubjectSet.SingleOrDefault(r => r.Id == id);

                if (item != null)
                    return HttpNotFound();

                return new ObjectResult(item);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Subject record)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            if (record == null)
                return HttpBadRequest();
            
            using (var db = new PADbContext())
            {                

                if(db.SubjectSet.Any(r=>r.Name == record.Name && record.Enabled == false))
                {
                    var rec = db.SubjectSet.Single(r => r.Name == record.Name && record.Enabled == false);
                    rec.Enabled = true;
                    db.Entry(rec).State = EntityState.Modified;
                }else
                    db.SubjectSet.Add(record);

                db.SaveChanges();

                return Ok();
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Subject record)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            if (record == null)
                return HttpBadRequest();
            
            using (var db = new PADbContext())
            {
                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();

                return Ok();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            using (var db = new PADbContext())
            {
                var item = db.SubjectSet.SingleOrDefault(r => r.Id == id);

                if (item == null)
                    return HttpNotFound();

                if (!db.ContactSet.Any(r => r.IdSubject == item.Id))
                    db.SubjectSet.Remove(item);
                else
                {
                    item.Enabled = false;
                    db.Entry(item).State = EntityState.Modified;
                }

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
