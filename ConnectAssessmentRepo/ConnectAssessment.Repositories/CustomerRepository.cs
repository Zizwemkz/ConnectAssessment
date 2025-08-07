using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectAssessment.Common.Repository;
using ConnectAssessment.Data;
using ConnectAssessment.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectAssessment.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConnectAssessmentDbContext _context;
        public CustomerRepository(ConnectAssessmentDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "DbContext cannot be null.");
        }

        public async Task<tbCustomer> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Customer ID must be a positive integer.", nameof(id));
            try
            {
                var result = await _context.tbCustomers.FindAsync(id);
                if (result == null)
                    throw new KeyNotFoundException($"Customer with ID {id} not found.");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetByIdAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while retrieving the customer.", ex);
            }
        }

        public async Task<IEnumerable<tbCustomer>> GetAllAsync()
        {
            try
            {
                return await _context.tbCustomers.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetAllAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while retrieving all customers.", ex);
            }
        }

        public async Task AddAsync(tbCustomer customer)
        {
            ValidateCustomer(customer);

            try
            {
                await _context.tbCustomers.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in AddAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while adding the customer.", ex);
            }
        }

        public async Task UpdateAsync(tbCustomer customer)
        {
            ValidateCustomer(customer);

            try
            {
                _context.tbCustomers.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error in UpdateAsync: {dbEx.Message}");
                throw new Exception("A database error occurred while updating the customer.", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in UpdateAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while updating the customer.", ex);
            }
        }

        private void ValidateCustomer(tbCustomer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");

            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Customer name is required.", nameof(customer.Name));
            if (string.IsNullOrWhiteSpace(customer.SurName))
                throw new ArgumentException("Customer name is required.", nameof(customer.SurName));
            if (string.IsNullOrWhiteSpace(customer.AccountNumber))
                throw new ArgumentException("Customer AccountNumber is required.", nameof(customer.AccountNumber));
            if (string.IsNullOrWhiteSpace(customer.Branch))
                throw new ArgumentException("Customer Branch is required.", nameof(customer.Branch));
        }
    }
}