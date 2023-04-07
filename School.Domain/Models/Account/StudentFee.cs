using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.Account
{
    public class StudentFee:BaseEntity
    {
        public int StudentId { get; set; }
        public int Amount { get; set; }
        public string? Description { get; set; }
        public bool IsPay { get; set; }

        public Student? Student { get; set; }
    }
}
