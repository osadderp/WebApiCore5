using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NexosApi.Data;
using NexosApi.Models;
using NexosApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexosApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        private IMapper _mapper;

        public BookRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<BookDto> CreateUpdate(BookDto bookDto)
        {
            Book book = _mapper.Map<BookDto, Book>(bookDto);
            if (book.Id > 0)
            {
                _appDbContext.Update(book);
            }
            else
            {
                await _appDbContext.AddAsync(book);
            }

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<Book, BookDto>(book);
        }

        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                Book book = await _appDbContext.Books.FindAsync(id);
                if (book == null)
                {
                    return false;
                }
                _appDbContext.Books.Remove(book);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BookDto>> GetABooks()
        {
            IEnumerable<Book> lista = await _appDbContext.Books.ToListAsync();

            return _mapper.Map<IEnumerable<BookDto>>(lista);
        }

        public async Task<BookDto> GetBook(int id)
        {
            Book book = await _appDbContext.Books.FindAsync(id);

            return _mapper.Map<BookDto>(book);
        }



        public async Task<AuthorDto> GetAuthor(int id)
        {
            Author author = await _appDbContext.Authors.FindAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }
        public async Task<PublisherDto> GetPublisher(int id)
        {
            Publisher publisher = await _appDbContext.Publishers.FindAsync(id);
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task<int> GetBookByPublisher(int id)
        {
            List<Book> lista = await _appDbContext.Books.ToListAsync();

            return lista.FindAll(b=>b.PublisherId == id).Count;
        }

    }
}
