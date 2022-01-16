using NexosApi.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexosApi.Repository
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<PublisherDto>> GetAPublishers();
        Task<PublisherDto> GetPublisher(int id);
        Task<PublisherDto> CreateUpdate(PublisherDto publisherDto);
        Task<bool> DeletePublisher(int id);
    }
}
