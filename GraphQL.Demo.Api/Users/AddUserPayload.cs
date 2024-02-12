using GraphQL.Demo.Api.Common;
using GraphQL.Demo.Api.Data;

namespace GraphQL.Demo.Api.Users
{
    public class AddUserPayload : UserPayloadBase
    {

        public AddUserPayload(User user) : base(user) { }

        public AddUserPayload(IReadOnlyList<UserError> errors)
           : base(errors)
        {
        }
    }
}