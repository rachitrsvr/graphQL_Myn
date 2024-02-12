using GraphQL.Demo.Api.Data;
using HotChocolate.Data.Filters;

namespace GraphQL.Demo.Api.Users
{
    public class UserFilterType : FilterInputType<User>
    {
        protected override void Configure(
            IFilterInputTypeDescriptor<User> descriptor)
        {
            descriptor.Ignore(x => x.WebUrl);
            descriptor.Ignore(x => x.Address);
        }
    }

}
