using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using PA.Models;
using PA.Models.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Query;
using PA.Models.ViewModels;
using PA.Services.MessageServices;
using System.Text;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PA.API
{
    //http://docs.asp.net/projects/mvc/en/latest/getting-started/first-web-api.html



    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IEmailSender _emailSender;

        private ApplicationDbContext _context;

        public ContactController(IEmailSender emailSender, ApplicationDbContext db)
        {
            _emailSender = emailSender;
            this._context = db;
        }

        #region Configuration
        private IConfigurationRoot _Configuration;
        public IConfigurationRoot Configuration
        {
            get
            {
                if (_Configuration == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables();
                    _Configuration = builder.Build();
                }
                return _Configuration;
            }
        }
        #endregion

        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            //using (var db = new PADbContext())
            //{
                return _context.ContactSet.ToArray();
            //}
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            //using (var db = new PADbContext())
            //{
                var rec = (from r in _context.ContactSet
                           where r.Id == id
                           select new
                           {
                               r.Id,
                               Subject = r.Subject.Name,
                               Areas = r.ContactAreas.Select(a => a.Area.Name)
                           }).SingleOrDefault();

                if (rec == null)
                    return HttpNotFound();

                var viewModel = new ContactDetailViewModel() { Id = rec.Id, Areas = rec.Areas.ToArray(), Subject = rec.Subject };
                return new ObjectResult(viewModel);
            //}
        }

        [HttpPost]
        public IActionResult Post([FromBody]Contact item)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            //using (var db = new PADbContext())
            //{
                if (item == null)
                    return HttpBadRequest();

                _context.ContactSet.Add(item);

                _context.SaveChanges();

                #region get Subject and Areas
                var subjectName = _context.SubjectSet.SingleOrDefault(r => r.Id == item.IdSubject).Name;

                StringBuilder areasSb = new StringBuilder();

                if (_context.ContactAreaSet.Any(r => r.IdContact == item.Id))
                {
                    areasSb.Append("<ul>");
                    foreach (var ca in _context.ContactAreaSet.Include(r => r.Area).Where(r => r.IdContact == item.Id))
                        areasSb.Append(string.Format("<li>{0}</li>", ca.Area.Name));
                    areasSb.Append("</ul>");
                }
                #endregion

                #region Envio de emails

                var replyMessage = string.Format("<p>Caro {0},</p><p>Registamos o seu contato no nosso sistema.</p><p>Os Melhores Cumprimentos,<br/>Obrigado</p>", item.Name);
                _emailSender.Send(item.Email, "Contacto", replyMessage);

                var contactInfoMessage = string.Format("<h4>Recebemos o contacto com os seguintes dados:</h4>"
                    + "<p><b>Nome</b>: {0}</p>"
                    + "<p><b>Telefone</b>: {1}</p>"
                    + "<p><b>Email</b>: {2}</p>"
                    + "<p><b>Assunto</b>: {3}</p>"
                    + "<p><b>Areas de Interesse</b>:<br> {4}</p>"
                    + "<p><b>Mensagem</b>: {5}</p>"
                    , item.Name
                    , item.Telefone
                    , item.Email
                    , subjectName
                    , areasSb.ToString()
                    , item.Message);

                var emailAddressTo = Configuration["ApplicationSettings:ContactSettings:EmailAddressTo"];
                _emailSender.Send(emailAddressTo, "Contacto Recebido", contactInfoMessage);

                #endregion

                return Ok();
            //}
        }
    }
}
