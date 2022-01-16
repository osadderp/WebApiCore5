using AutoMapper;
using NexosApi.Models;
using NexosApi.Models.Dto;

namespace NexosApi
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mapingConfig= new MapperConfiguration(config=>
                {
                    config.CreateMap<AuthorDto, Author>();
                    config.CreateMap<Author, AuthorDto>();
                    config.CreateMap<PublisherDto, Publisher>();
                    config.CreateMap<Publisher, PublisherDto>();
                    config.CreateMap<BookDto, Book>();
                    config.CreateMap<Book, BookDto>();
                } );
            return mapingConfig;
        }
    }
}
