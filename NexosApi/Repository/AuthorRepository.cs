using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NexosApi.Data;
using NexosApi.Models;
using NexosApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexosApi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _appDbContext;
        private IMapper _mapper;

        public AuthorRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<AuthorDto> CreateUpdate(AuthorDto authorDto)
        {
            Author author = _mapper.Map<AuthorDto, Author>(authorDto);
            if (author.Id > 0)
            {
                _appDbContext.Update(author);
            }
            else
            {
                await _appDbContext.AddAsync(author);
            }

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<Author, AuthorDto>(author);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            try
            {
                Author author = await _appDbContext.Authors.FindAsync(id);
                if (author == null)
                {
                    return false;
                }
                _appDbContext.Authors.Remove(author);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<AuthorDto> GetAuthor(int id)
        {
            Author author = await _appDbContext.Authors.FindAsync(id);

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthors()
        {
            IEnumerable<Author> lista = await _appDbContext.Authors.ToListAsync();

            return _mapper.Map<IEnumerable<AuthorDto>>(lista);
        }
    }
}
