namespace EFCoreStudentRelationship
{
    public class Addressdto
    {
        public int StudentAddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public int StudentId { get; set; } = 1;
    }
}
