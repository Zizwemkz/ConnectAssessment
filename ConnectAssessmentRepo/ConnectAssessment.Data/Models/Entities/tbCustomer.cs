using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectAssessment.Data.Models.Entities
{
    public class tbCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public required string AccountNumber { get; set; }
        public string Branch { get; set; }
    }
}
