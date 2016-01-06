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
    public class AreaController : Controller
    {
        private ApplicationDbContext _context;

        public AreaController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Area> Get()
        {
            //using (var db = new ApplicationDbContext())
            //{               
                return _context.AreaSet.Where(r=>r.Enabled).ToArray();
            //}            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            //using (var db = new PADbContext())
            //{
                var item = _context.AreaSet.SingleOrDefault(r => r.Id == id);

                if (item != null)
                    return HttpNotFound();

                return new ObjectResult(item);
            //}
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Area record)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);


            if (record == null)
                return HttpBadRequest();
            
            //using (var db = new PADbContext())
            //{
                if (_context.AreaSet.Any(r => r.Name == record.Name && record.Enabled == false))
                {
                    var rec = _context.AreaSet.Single(r => r.Name == record.Name && record.Enabled == false);
                    rec.Enabled = true;
                    _context.Entry(rec).State = EntityState.Modified;
                }
                else
                    _context.AreaSet.Add(record);

                _context.SaveChanges();

                return Ok();
            //}
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Area record)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            if (record == null)
                return HttpBadRequest();
            
            //using (var db = new PADbContext())
            //{
                _context.Entry(record).State = Microsoft.Data.Entity.EntityState.Modified;
                _context.SaveChanges();

                return Ok();
            //}
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            //using (var db = new PADbContext())
            //{
                var item = _context.AreaSet.SingleOrDefault(r => r.Id == id);

                if (item == null)
                    return HttpNotFound();

                if (!_context.ContactAreaSet.Any(r => r.IdArea == item.Id))
                    _context.AreaSet.Remove(item);
                else
                {
                    item.Enabled = false;
                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChanges();

                return Ok();
            //}
        }
    }
}
