using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ICustomerItemRepository : IRepository<CustomerItem>
    {
        Task<IEnumerable<CustomerItem>> GetItemsWithinRangeAsync(int fromItemNumber, int toItemNumber);
        Task<IEnumerable<CustomerItem>> GetCustomerItemsAsync();
    }
    public class CustomerItemRepository : Repository<CustomerItem>, ICustomerItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerItemRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerItem>> GetItemsWithinRangeAsync(int fromItemNumber, int toItemNumber)
        {

            var rangeResult = await _context.CustomerItem.Include(x => x.Customer)
                                         .Include(x => x.Item)
                                         .ThenInclude(x => x!.Category)
                                         .Where(x => x.Item!.Number >= fromItemNumber && x.Item.Number <= toItemNumber)
                                         .OrderByDescending(x => x.Item!.Number)
                                         .ToListAsync();

            return rangeResult;
        }

        public async Task<IEnumerable<CustomerItem>> GetCustomerItemsAsync()
        {
            var customerItems = await _context.CustomerItem.Include(x => x.Customer)
                                         .Include(x => x.Item)
                                         .ThenInclude(x => x!.Category)
                                         .ToListAsync();

            return customerItems;
        }

    }
}
