using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomersInfoAsync();
        Task<Customer> GetCustomerInfoByIdAsync(int customerId);
    }
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersInfoAsync()
        {
            var customers = await _context.Customers.Include(x => x.CustomerItems)
                                                    .ThenInclude(x => x.Item)
                                                    .ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomerInfoByIdAsync(int customerId)
        {
            var customer = await _context.Customers
                                        .Where(x => x.Id == customerId)
                                        .Include(x => x.CustomerItems)
                                        .ThenInclude(x => x.Item)
                                        .ThenInclude(x => x!.Category)
                                        .FirstOrDefaultAsync();

            return customer;
        }
    }
}
