using GraphQL.Demo.Api.Common;
using GraphQL.Demo.Api.Data;

namespace GraphQL.Demo.Api.Users
{
    public class ModifyUserPayload : UserPayloadBase
    {
        public ModifyUserPayload(User user)
            : base(user)
        {
        }

        public ModifyUserPayload(UserError error)
            : base(new[] { error })
        {
        }
    }
}
