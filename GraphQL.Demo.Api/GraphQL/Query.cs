using GraphQL.Demo.Api.Data;
using GraphQL.Demo.Api.DataAccess;

namespace GraphQL.Demo.Api.GraphQL
{
    public class Query
    {
        private readonly IDataAccessProvider _IDataAccessProvider;
        public Query(IDataAccessProvider IDataAccessProvider)
        {
            _IDataAccessProvider = IDataAccessProvider;
        }

        // Retrieves a list of all users from the system.
        public async Task<List<User>> GetAllUsers()
        {
            return await _IDataAccessProvider.GetAllUsers();
        }

        public User GetUserById([ID] int id)
        {
            return _IDataAccessProvider.GetUserById(id);
        }

    }
}
