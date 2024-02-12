using GraphQL.Demo.Api.Data;
using HotChocolate;
using HotChocolate.Types.Relay;

namespace GraphQL.Demo.Api.Users
{
    public record ModifyUserInput(
        [property: ID(nameof(User))]
        int Id,
        Optional<string?> FirstName,
        Optional<string?> LastName,
        Optional<string?> CompanyName,
        Optional<string?> Address,
        Optional<string?> City,
        Optional<string?> County,
        Optional<string?> Postal,
        Optional<string?> Phone1,
        Optional<string?> Phone2,
        Optional<string?> Email,
        Optional<string?> WebUrl);
}
