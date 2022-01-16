using NexosApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexosApi.Repository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorDto>> GetAuthors();
        Task<AuthorDto> GetAuthor(int id);
        Task<AuthorDto> CreateUpdate(AuthorDto authorDto);
        Task<bool> DeleteAuthor(int id);

    }
}
