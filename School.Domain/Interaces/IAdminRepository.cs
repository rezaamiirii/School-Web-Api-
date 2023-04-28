using School.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Interaces
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin> GetadminByPhoneNumber(string phoneNumber);
    }
}
