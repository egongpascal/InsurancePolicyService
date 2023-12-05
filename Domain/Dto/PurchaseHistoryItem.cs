namespace InsurancePolicyService.Domain
{
    public class PurchaseHistoryItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public string BodyType { get; set; }
        public double Premium { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
