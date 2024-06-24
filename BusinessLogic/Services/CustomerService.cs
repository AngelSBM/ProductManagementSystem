using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.UnitOfWork;
using Domain.Entities;
using ProductManagementSystem.Shared.DTOs;
using ProductManagementSystem.Shared.DTOs.Item;
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
        Task CreateCustomerItemAsync(CreateCustomerItemDto newCustomerItem);
        Task UpdateCustomerAsync(CustomerDto updatedCustomer);
        Task DeleteCustomerAsync(int customerId);
        Task<CustomerInfoDto> GetCustomersInformationAsync(int customerId);
        Task DeleteCustomerItemAsync(DeleteCustomerItemDto customerItemTodelete);
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
            await _unitOfWork.Complete();
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

        public async Task<CustomerInfoDto> GetCustomersInformationAsync(int customerId)
        {
            var customer = await _unitOfWork.customerRepository.GetCustomerInfoByIdAsync(customerId);

            var customerInfoDto = _mapper.Map<CustomerInfoDto>(customer);

            foreach (var customerItem in customer.CustomerItems)
            {
                var itemDto = _mapper.Map<ItemDto>(customerItem.Item);
                customerInfoDto.Items.Add(itemDto);
            }

            return customerInfoDto;
        }

        public async Task CreateCustomerItemAsync(CreateCustomerItemDto newCustomerItem)
        {
  
            var customer = await _unitOfWork.customerRepository.GetByAsync(x => x.Id == newCustomerItem.CustomerId);
            var item = await _unitOfWork.itemRepository.GetByAsync(x => x.Id == newCustomerItem.ItemId);

            if (customer is null || item is null)
                throw new InvalidOperationException("Customer or item not found");

            var customerItemDb = new CustomerItem()
            {
                CustomerId = customer.Id,
                ItemId = item.Id
            };

            customer.CustomerItems.Add(customerItemDb);

            await _unitOfWork.Complete();

        }

        public async Task DeleteCustomerItemAsync(DeleteCustomerItemDto customerItemTodelete)
        {
            var customerItem = await _unitOfWork.customerItemRepository.GetByAsync(x => x.ItemId == customerItemTodelete.ItemId && x.CustomerId == customerItemTodelete.CustomerId);
            await _unitOfWork.customerItemRepository.DeleteAsync(customerItem.Id);
        }
        
    }
}
