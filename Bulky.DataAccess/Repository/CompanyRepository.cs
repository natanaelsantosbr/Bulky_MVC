using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Bulky.Models.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(Company category)
        {
            _context.Companies.Update(category);
        }
    }
}
