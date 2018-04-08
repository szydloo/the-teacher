namespace TheTeacher.Core.Domain
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }

        public Address(string street, string city, string zipcode, string country)
        {
            // TODO: Add some sort of validation maybe
            Street = street;
            City = city;
            Zipcode = zipcode;
            Country = country;
        }
    }
}