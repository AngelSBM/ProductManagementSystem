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
             IRepository<Customer> customerRepository,
             IRepository<Item> itemRepository,
             IRepository<Category> categoryRepository
            )
        {
            _context = context;
            this.customerRepository = customerRepository;
            this.itemRepository = itemRepository;
            this.categoryRepository = categoryRepository;
        }

        private readonly ApplicationDbContext _context;
        public IRepository<Customer> customerRepository { get; set; }
        public IRepository<Item> itemRepository { get; set; }
        public IRepository<Category> categoryRepository { get; set; }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void Complete()
        {
            _context.SaveChangesAsync();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
