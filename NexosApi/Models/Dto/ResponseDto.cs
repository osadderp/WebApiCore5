using System.Collections.Generic;

namespace NexosApi.Models.Dto
{
    public class ResponseDto
    {
        public bool IsOk { get; set; }
        public object Result { get; set; }
        public string ResultMessage { get; set; }
        public List<string> Errors { get; set; }

    }
}
