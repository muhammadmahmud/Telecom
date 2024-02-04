using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class PhoneNumber
    {
        public int Id { get; set; }
        public int? PhoneNo { get; set; }
        public string? CustomerName { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
