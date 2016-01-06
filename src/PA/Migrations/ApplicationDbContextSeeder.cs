using Microsoft.AspNet.Identity;
using PA.Models;
using PA.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PA.Migrations
{
    public class ApplicationDbContextSeeder
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbContextSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            //if (!_context.Users.Any())
            //{
            //    await _userManager.CreateAsync(new IdentityUser("admin"), "1234");

            //    for (int i = 0; i < 6; i++)
            //    {
            //        var user = new IdentityUser
            //        {
            //            UserName = $"test{i}",
            //            Email = $"test{i}@tester.com"
            //        };

            //        await _userManager.CreateAsync(user, "000"+i);
            //    }
            //}

            if (!_context.AreaSet.Any())
            {
                var areaList = new List<Area>
                {
                    new Area { Name = "ASP.NET", Enabled = true },
                    new Area { Name = "C#", Enabled = true },
                    new Area { Name = "Entity Framework", Enabled = true },
                    new Area { Name = "Web API", Enabled = true },
                    new Area { Name = "AngularJS", Enabled = true },
                    new Area { Name = "HTML5", Enabled = true },
                    new Area { Name = "Bootstrap", Enabled = true },
                    new Area { Name = "ASP.NET 5", Enabled = true }
                };

                var subjectList = new List<Subject>
                {
                    new Subject { Name = "Sugestão", Enabled = true },
                    new Subject { Name = "Reclamação", Enabled = true },
                    new Subject { Name = "Pedido Apoio", Enabled = true }
                };

                _context.AreaSet.AddRange(areaList);
                _context.SubjectSet.AddRange(subjectList);
                await _context.SaveChangesAsync();
            }
        }
    }
}
