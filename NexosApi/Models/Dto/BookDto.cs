using System;
namespace NexosApi.Models.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }
        public string Gender { get; set; }
        public int NoPages { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
