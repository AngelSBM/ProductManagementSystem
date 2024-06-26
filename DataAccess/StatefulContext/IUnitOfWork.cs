﻿using DataAccess.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Collections.Specialized.BitVector32;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ICustomerRepository customerRepository { get; set; }
        public IItemRepository itemRepository { get; set; }
        public ICustomerItemRepository customerItemRepository { get; set; }
        public IRepository<Category> categoryRepository { get; set; }

        public void BeginTransaction();
        public void CommitTransaction();
        public void RollbackTransaction();
        public Task Complete();
    }
}
