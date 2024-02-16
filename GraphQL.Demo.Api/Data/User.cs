using GraphQL.Demo.Api.Users;
using HotChocolate.Types.Relay;
using System.ComponentModel.DataAnnotations;

namespace GraphQL.Demo.Api.Data
{
    //[Node(NodeResolver = nameof(UserQueries.GetUserByIdAsync))]
    public class User
    {
        [ID]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? County { get; set; }
        public string? Postal { get; set; }
        [Required]
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? WebUrl { get; set; }
    }

}
