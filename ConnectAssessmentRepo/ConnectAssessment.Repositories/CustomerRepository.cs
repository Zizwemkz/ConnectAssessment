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
        private readonly DbContextOptions<ConnectAssessmentDbContext> _options;
        public CustomerRepository(DbContextOptions<ConnectAssessmentDbContext> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options), "DbContextOptions cannot be null.");
        }

        public async Task<tbCustomer> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Customer ID must be a positive integer.", nameof(id));
            try
            {
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    var result = await context.tbCustomers.FindAsync(id);
                    if (result == null)
                        throw new KeyNotFoundException($"Customer with ID {id} not found.");
                    return result;
                }
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
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    return await context.tbCustomers.ToListAsync();
                }
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
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    await context.tbCustomers.AddAsync(customer);
                    await context.SaveChangesAsync();
                }
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
                using (var context = new ConnectAssessmentDbContext(_options))
                {
                    context.tbCustomers.Update(customer);
                    await context.SaveChangesAsync();
                }
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
            if (string.IsNullOrWhiteSpace(customer.CompanyName))
                throw new ArgumentException("Company name is required.", nameof(customer.CompanyName));
            if (string.IsNullOrWhiteSpace(customer.AccountNumber))
                throw new ArgumentException("Customer AccountNumber is required.", nameof(customer.AccountNumber));
            if (string.IsNullOrWhiteSpace(customer.Branch))
                throw new ArgumentException("Customer Branch is required.", nameof(customer.Branch));
        }
    }
}