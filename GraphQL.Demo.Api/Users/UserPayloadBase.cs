using GraphQL.Demo.Api.Common;
using GraphQL.Demo.Api.Data;

namespace GraphQL.Demo.Api.Users
{
    public class UserPayloadBase : Payload
    {
        protected UserPayloadBase(User user)
        {
            User = user;
        }

        protected UserPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public User? User { get; }
    }
}
