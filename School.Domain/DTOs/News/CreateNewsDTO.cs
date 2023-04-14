using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.News
{
    public class CreateNewsDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? UrlName { get; set; }
        public IFormFile? Image { get; set; }
    }
    public enum CreateNewsResult
    {
        UrlExist,
        NotImage,
        Success
    }

}

