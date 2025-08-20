using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectAssessment.Data.Models.Entities;

namespace ConnectAssessment.Common.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<tbCustomer>> GetAllCustomersAsync();
    }
}
