namespace Client.Models
{
    public class PhoneNumberDto
    {
        public int id { get; set; }
        public int? phoneNo { get; set; }
        public string? customerName { get; set; }
        public bool? isActive { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; }
        public string? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
    }
}
