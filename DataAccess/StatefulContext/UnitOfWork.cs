using DataAccess.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(
             ApplicationDbContext context,
             ICustomerRepository customerRepository,
             IItemRepository itemRepository,
             IRepository<CustomerItem> customerItemRepository,
             IRepository<Category> categoryRepository
            )
        {
            _context = context;
            this.customerRepository = customerRepository;
            this.itemRepository = itemRepository;
            this.categoryRepository = categoryRepository;
            this.customerItemRepository = customerItemRepository;
        }

        private readonly ApplicationDbContext _context;
        public ICustomerRepository customerRepository { get; set; }
        public IItemRepository itemRepository { get; set; }
        public IRepository<CustomerItem> customerItemRepository { get; set; }
        public IRepository<Category> categoryRepository { get; set; }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
