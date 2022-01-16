using NexosApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexosApi.Repository
{
    public interface IBookRepository
    {

        Task<IEnumerable<BookDto>> GetABooks();
        Task<BookDto> GetBook(int id);
        Task<BookDto> CreateUpdate(BookDto bookDto);
        Task<bool> DeleteBook(int id);


        Task<AuthorDto> GetAuthor(int id);
        Task<PublisherDto> GetPublisher(int id);
        Task<int> GetBookByPublisher(int id);


    }
}
