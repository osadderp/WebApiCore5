using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NexosApi.Data;
using NexosApi.Models;
using NexosApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexosApi.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _appDbContext;
        private IMapper _mapper;

        public PublisherRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<PublisherDto> CreateUpdate(PublisherDto publisherDto)
        {
            Publisher publisher = _mapper.Map<PublisherDto, Publisher>(publisherDto);
            if (publisher.Id > 0)
            {
                _appDbContext.Update(publisher);
            }
            else
            {
                await _appDbContext.AddAsync(publisher);
            }

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<Publisher, PublisherDto>(publisher);
        }

        public async Task<bool> DeletePublisher(int id)
        {
            try
            {
                Publisher publisher = await _appDbContext.Publishers.FindAsync(id);
                if (publisher == null)
                {
                    return false;
                }
                _appDbContext.Publishers.Remove(publisher);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PublisherDto>> GetAPublishers()
        {
            IEnumerable<Publisher> lista = await _appDbContext.Publishers.ToListAsync();

            return _mapper.Map<IEnumerable<PublisherDto>>(lista);
        }

        public async Task<PublisherDto> GetPublisher(int id)
        {
            Publisher publisher = await _appDbContext.Publishers.FindAsync(id);

            return _mapper.Map<PublisherDto>(publisher);
        }
    }
}
