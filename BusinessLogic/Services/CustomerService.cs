using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.UnitOfWork;
using Domain.Entities;
using ProductManagementSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task CreateNewCustomerAsync(CreateCustomerDto newCustomer);
        Task UpdateCustomerAsync(CustomerDto updatedCustomer);
        Task DeleteCustomerAsync(int customerId);
    }

    public class CustomerService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICustomerService
    {
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.customerRepository.GetByAsync(x => x.Id == id);
            
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.customerRepository.GetAllCustomersInfoAsync();

            var customersDto = _mapper.Map <IEnumerable<CustomerDto>>(customers);
            
            return customersDto;
        }

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

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customerDb = await _unitOfWork.customerRepository.GetByAsync(x => x.Id == customerId);
            if (customerDb is null)
                throw new InvalidOperationException("Customer not found");

            customerDb.Active = false;
            _unitOfWork.Complete();
        }

        public async Task UpdateCustomerAsync(CustomerDto updatedCustomer)
        {
            var customer = await _unitOfWork.customerRepository.GetByAsync(x => x.Id == updatedCustomer.Id);

            if (customer is null)
                throw new InvalidOperationException("Customer not found");

            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            customer.Phone = updatedCustomer.Phone;
            customer.Active = updatedCustomer.Active;

            await _unitOfWork.Complete();

        }

    }
}
