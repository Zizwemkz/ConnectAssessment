using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectAssessment.Common.Repository;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Entities;

namespace ConnectAssessment.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository
                ?? throw new ArgumentNullException(nameof(customerRepository), "Customer repository cannot be null.");
        }

        public async Task<IEnumerable<tbCustomer>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in CustomerService.GetAllAsync: {ex.Message}");
                throw new Exception("An unexpected error occurred while retrieving customers.", ex);
            }
        }
    }
}
