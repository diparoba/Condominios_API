namespace ServiceUser_API.Models
{
    public class Address
    {
        public string? Neighborhood { get; set; }
        public string? Street { get; set; }
        public Provinces Province { get; set; }
        public Cities City { get; set; }
        public string? Country { get; set; }
    }
}