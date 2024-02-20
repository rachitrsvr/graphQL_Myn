using GraphQL.Demo.Api.Data;
using GraphQL.Demo.Api.DataLoader;

namespace GraphQL.Demo.Api.Users
{
    //[ExtendObjectType(OperationTypeNames.Query)]
    public class UserQueries
    {
        [UsePaging(MaxPageSize = 20, IncludeTotalCount = true)]
        [UseFiltering(typeof(UserFilterType))]
        public IQueryable<User> GetAllUsers([Service] AppDbContext context)
        {
            return context.Users.OrderByDescending(t => t.Id);
        }
        //public IQueryable<User> GetAllUsers(
        //   [ScopedService] AppDbContext context)
        //   => context.Users;

        //public Task<User> GetUserByIdAsync(
        //  [ID(nameof(User))] int id,
        //  UserByIdDataLoader attendeeById,
        //  CancellationToken cancellationToken)
        //  => attendeeById.LoadAsync(id, cancellationToken);
        //[NodeResolver]
        public Task<User> GetUserByIdAsync(
            int id,
            UserByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
            => dataLoader.LoadAsync(id, cancellationToken);
    }
}
