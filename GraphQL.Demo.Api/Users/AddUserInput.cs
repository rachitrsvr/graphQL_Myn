namespace GraphQL.Demo.Api.Users
{
    public class AddUserInput
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        public string? Postal { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string Email { get; set; }
        public string? WebUrl { get; set; }
    }
}