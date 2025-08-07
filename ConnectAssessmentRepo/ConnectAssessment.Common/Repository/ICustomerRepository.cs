using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectAssessment.Data.Models.Entities;

namespace ConnectAssessment.Common.Repository
{
    public interface ICustomerRepository
    {
        Task<tbCustomer> GetByIdAsync(int id);
        Task<IEnumerable<tbCustomer>> GetAllAsync();
        Task AddAsync(tbCustomer customer);
        Task UpdateAsync(tbCustomer customer);
    }
}
