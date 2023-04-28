using Microsoft.EntityFrameworkCore;
using School.Domain.Interaces;
using School.Domain.Models.Account;
using Shop.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infra.Data.Repositories
{
    public class AdminRepository: GenericRepository<Admin>, IAdminRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }




        #endregion

        #region properties
        public async Task<Admin> GetadminByPhoneNumber(string phoneNumber)
        {
            return await _context.Admins.AsQueryable()
                .Where(x => x.PhoneNumber == phoneNumber)
                .SingleOrDefaultAsync();
        }
        #endregion

    }
}
