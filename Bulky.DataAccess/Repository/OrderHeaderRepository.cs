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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(OrderHeader obj)
        {
            _context.OrderHeaders.Update(obj);
        }
    }
}
