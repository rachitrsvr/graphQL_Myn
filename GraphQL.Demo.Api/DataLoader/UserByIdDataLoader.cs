using GraphQL.Demo.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.Api.DataLoader
{
    public class UserByIdDataLoader
    : BatchDataLoader<int, User>
    {
        private readonly AppDbContext _dbContext;

        public UserByIdDataLoader(
            AppDbContext appDbContext,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContext = appDbContext;
        }

        protected override async Task<IReadOnlyDictionary<int, User>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
