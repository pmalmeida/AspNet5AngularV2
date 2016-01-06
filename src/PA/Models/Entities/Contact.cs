//using Microsoft.Data.Entity.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA.Models.Entities
{
    public class Contact
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [EmailAddress(ErrorMessage ="Formato inválido")]
        [Required(ErrorMessage = "Campo obrigatorio")]
        public string Email { get; set; }

        [StringLength(200,ErrorMessage ="Introduza de 5 a 200 caracteres.",MinimumLength =5)]
        [Required(ErrorMessage = "Campo obrigatorio")]
        public string Name { get; set; }

        [Phone(ErrorMessage="Formato inválido")]
        [StringLength(30)]
        public string Telefone { get; set; }
        
        [ForeignKey("IdSubject")]
        public Subject Subject { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio")]
        public long IdSubject { get; set; }

        [StringLength(int.MaxValue, ErrorMessage = "Introduza pelo menos 5 caracteres.", MinimumLength = 5)]
        [Required(ErrorMessage = "Campo obrigatorio")]
        public string Message { get; set; }

        public virtual ICollection<ContactArea> ContactAreas { get; set; }        
    }
    
}
