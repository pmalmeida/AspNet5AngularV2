//using Microsoft.Data.Entity.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.Models.Entities
{
   
    public class Subject
    {
        public Subject()
        {
            Enabled = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(200, ErrorMessage = "Introduza de 5 a 200 caracteres.", MinimumLength = 5)]
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }    
}
