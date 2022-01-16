using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexosApi.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("FullName", TypeName = "varchar(100)")]
        public string FullName { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime BirhtDay { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("From", TypeName = "varchar(100)")]
        public string From { get; set; }
        
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("Email", TypeName = "varchar(50)")]
        public string Email { get; set; }

    }
}
