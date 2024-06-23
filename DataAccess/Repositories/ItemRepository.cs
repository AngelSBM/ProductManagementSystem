using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        public Task<Item> GetItemByIdAsync(int id);
    }
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            var items = await _context.Items
                            .Where(x => x.Id == id)                        
                            .Include(x => x.Category)                                         
                            .FirstOrDefaultAsync();

            return items;
        }
    }
}
