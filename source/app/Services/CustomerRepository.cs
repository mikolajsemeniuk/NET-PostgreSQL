using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Data;
using app.Inputs;
using app.Interfaces;
using app.Models;
using app.Payloads;
using Microsoft.EntityFrameworkCore;

namespace app.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context) =>
            _context = context;

        public async Task<IEnumerable<CustomerPayload>> GetCustomersAsync() =>
            await _context.Customers.Select(customer => new CustomerPayload
                {
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    FullName = customer.FullName,
                    BirthDate = customer.BirthDate
                })
                .ToListAsync();

        public async Task<CustomerPayload> AddCustomerAsync(CustomerInput input)
        {
            var customer = new Customer
            {
                Name = input.Name,
                FullName = input.FullName,
                BirthDate = input.BirthDate
            };

            _context.Customers.Add(customer);

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("something gone wrong please try again later");

            return new CustomerPayload
            {
                Id = customer.CustomerId,
                Name = customer.Name,
                FullName = customer.FullName,
                BirthDate = customer.BirthDate
            };
        }

        public async Task<CustomerPayload> GetCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new Exception("Customer not found");
            return new CustomerPayload
            {
                Id = customer.CustomerId,
                Name = customer.Name,
                FullName = customer.FullName,
                BirthDate = customer.BirthDate
            };
        }

        public async Task RemoveCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new Exception("Customer not found");
            _context.Customers.Remove(customer);
            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("something gone wrong please try again later");
        }

        public async Task<CustomerPayload> UpdateCustomerAsync(int id, CustomerInput input)
        {
            var customer = await _context.Customers.FindAsync(id);
            
            if (customer == null)
                throw new Exception("Customer not found");
            
            if (customer.Name == input.Name 
                && customer.FullName == input.FullName
                && customer.BirthDate == input.BirthDate)
                return new CustomerPayload
                {
                    Id = customer.CustomerId,
                    Name = customer.Name,
                    FullName = customer.FullName,
                    BirthDate = customer.BirthDate
                };

            customer.Name = input.Name;
            customer.FullName = input.FullName;
            customer.BirthDate = input.BirthDate;

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("something gone wrong please try again later");

            return new CustomerPayload
            {
                Id = customer.CustomerId,
                Name = customer.Name,
                FullName = customer.FullName,
                BirthDate = customer.BirthDate
            };
        }
    }
}