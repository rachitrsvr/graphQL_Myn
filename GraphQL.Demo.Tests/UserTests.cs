
using GraphQL.Demo.Api.Data;
using GraphQL.Demo.Api.DataLoader;
using GraphQL.Demo.Api.Users;
using Microsoft.EntityFrameworkCore;
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
            var sqlConnectionString = "Host=localhost;Database=postgres;Username=postgres;Password=start;Port=5432";
            // Initialize the service collection and configure HotChocolate
            var services = new ServiceCollection();
            services
                 .AddDbContext<AppDbContext>(options => options.UseNpgsql(sqlConnectionString), ServiceLifetime.Scoped)
                .AddGraphQL()
                  .AddQueryType()
                .AddMutationType()
                .AddTypeExtension<UserQueries>()
                .AddTypeExtension<UserMutations>()
                .AddDataLoader<UserByIdDataLoader>()
                  .AddFiltering()
                .AddSorting();

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
                .SetQuery(@"query {
                                  allUsers {
                                    nodes{
                                    id
                                    firstName
                                    lastName
                                    email
                                    address
                                  }
                                  }
                                }")
                .Create();

            // Act
            IExecutionResult result = await executor.ExecuteAsync(request);
            
            // Assert
            //Assert.Null(result); // Ensure no errors occurred
            Assert.NotNull(result); // Ensure data is returned
            // You can perform additional assertions on the returned data if needed
        }

        [Fact]
        public async Task TestAddUsersQuery()
        {
            // Resolve the IRequestExecutor
            IRequestExecutor executor = await _resolver.GetRequestExecutorAsync();

            // Create and execute the query
            var request = QueryRequestBuilder.New()
                .SetQuery(@"mutation {
                          addUser(input: {
                            firstName: ""Ram""
                            lastName: ""Kapoor""
                            email: ""ram@test.com""
                            address: ""ram address"",
                            phone1:""668788556""
                          }) {
                            user {
                              id
                              firstName
                              lastName
                              email
                              address
                            }
                          }
                        }")
                .Create();

            // Act
            IExecutionResult result = await executor.ExecuteAsync(request);

            // Assert
            //Assert.Null(result); // Ensure no errors occurred
            Assert.NotNull(result); // Ensure data is returned
            // You can perform additional assertions on the returned data if needed
        }
    }
}
