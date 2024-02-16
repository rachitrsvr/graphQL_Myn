
using GraphQL.Demo.Api.Users;
using Testcontainers.PostgreSql;
namespace GraphQL.Demo.Tests
{
    public class UserTests
    {
        private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
           .WithImage("postgres:15-alpine")
           .Build();
        private readonly IRequestExecutorResolver _resolver;

        public UserTests()
        {
            // Initialize the service collection and configure HotChocolate
            var services = new ServiceCollection();
            services.AddGraphQL()
                    .AddQueryType<UserQueries>()
                    .AddMutationType<UserMutations>();

            // Build the service provider and resolve the IRequestExecutorResolver
            var serviceProvider = services.BuildServiceProvider();
            _resolver = serviceProvider.GetRequiredService<IRequestExecutorResolver>();
        }
        public Task InitializeAsync()
        {
            return _postgres.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _postgres.DisposeAsync().AsTask();
        }
        [Fact]
        public async Task TestGetUsersQuery()
        {
            // Resolve the IRequestExecutor
            IRequestExecutor executor = await _resolver.GetRequestExecutorAsync();

            // Create and execute the query
            var request = QueryRequestBuilder.New()
                .SetQuery("{ getBooks { id title author } }")
                .Create();

            // Act
            IExecutionResult result = await executor.ExecuteAsync(request);

            // Assert
            //Assert.Null(result.Errors); // Ensure no errors occurred
            //Assert.NotNull(result.Data); // Ensure data is returned
            // You can perform additional assertions on the returned data if needed
        }
    }
}
