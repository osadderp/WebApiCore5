using System;

namespace NexosApi.Models.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirhtDay { get; set; }
        public string From { get; set; }
        public string Email { get; set; }
    }
}
