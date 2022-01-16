using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexosApi.Models
{

    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("Title", TypeName = "varchar(100)")]
        public string Title { get; set; }
   
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime Year { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Column("Gender", TypeName = "varchar(20)")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int NoPages { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
