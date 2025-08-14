using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConnectAssessment.Data.Models.Requests
{
    public class SettleCustomerRequest
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 200000, ErrorMessage = "Amount must be greater than 0 and less than or equal to 200,000.")]
        public decimal Amount { get; set; }
    }
}
