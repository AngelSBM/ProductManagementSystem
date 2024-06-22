using BusinessLogic.DTOs;
using DataAccess.UnitOfWork;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface ICustomerService
    {
        Task CreateNewCustomerAsync(CreateCustomerDto newCustomer);
    }

    public class CustomerService(IUnitOfWork _unitOfWork) : ICustomerService
    {
        public async Task CreateNewCustomerAsync(CreateCustomerDto newCustomer)
        {
            var sameInfoCustomers = await _unitOfWork.customerRepository.FilterAsync(x => x.Phone == newCustomer.Phone || x.Email == newCustomer.Email);

            if (sameInfoCustomers.Any())
            {
                throw new InvalidOperationException("Customer with contact info already exists");
            }

            var customerDb = new Customer()
            {
                Name = newCustomer.Name,
                Phone = newCustomer.Phone,
                Email = newCustomer.Email,
                Active = true
            };

            await _unitOfWork.customerRepository.AddAsync(customerDb);
        }
    }
}
