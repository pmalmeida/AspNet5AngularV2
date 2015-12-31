using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PA.Models.ViewModels
{
    public class ContactDetailViewModel
    {
        public long Id { get; set; }

        public string Subject { get; set; }

        public string[] Areas { get; set; }
    }
}
