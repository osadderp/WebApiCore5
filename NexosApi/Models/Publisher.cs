using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexosApi.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
     
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("Name", TypeName = "varchar(100)")]
        public string Name { get; set; }
    
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("Address", TypeName = "varchar(100)")]
        public string Address { get; set; }
     
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("Phone", TypeName = "varchar(20)")]
        public string Phone { get; set; }
     
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("Email", TypeName = "varchar(50)")]
        public string Email { get; set; }
   
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int MaxBook { get; set; }

    }
}
