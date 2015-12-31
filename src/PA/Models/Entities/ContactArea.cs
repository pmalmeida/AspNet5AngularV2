
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.Models.Entities
{  
    public class ContactArea
    {
        public long IdContact { get; set; }

        public long IdArea { get; set; }
       
        public Contact Contact { get; set; }

        public Area Area { get; set; }
    }
}
