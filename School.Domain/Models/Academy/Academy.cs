using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.Academy
{
    public class Academy:BaseEntity
    {
        #region properties
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? ImageName { get; set; }
        #endregion

        
    }
}
