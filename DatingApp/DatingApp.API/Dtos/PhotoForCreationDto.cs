using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAddede { get; set; }
        //public string PublicId { get; set; }
        public string CloudDinaryPublicId { get; set; }
        public PhotoForCreationDto() 
        {
            DateAddede = DateTime.Now;
        }
    }
}
