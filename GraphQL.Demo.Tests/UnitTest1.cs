using GraphQL.Demo.Api.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GraphQL.Demo.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task SchemaChangeTest()
        {
            var schema = await new ServiceCollection()
                .AddGraphQLServer()
                .AddQueryType<UserQueries>()
                .AddMutationType()
         .AddTypeExtension<UserMutations>()
    .AddFiltering()
                .BuildSchemaAsync();
            schema.ToString().MatchSnapshot();
        }
    }
}