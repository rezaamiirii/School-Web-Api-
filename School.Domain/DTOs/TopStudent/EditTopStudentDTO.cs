using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.DTOs.TopStudent
{
    public class EditTopStudentDTO
    {
        public string? ImageName { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public IFormFile? Image { get; set; }
    }
    public enum EditTopStudentResult
    {
        Success,
        NotFound



    }
}
