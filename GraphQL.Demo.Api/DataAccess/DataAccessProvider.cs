using GraphQL.Demo.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.Api.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        AppDbContext _context;
        //private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public DataAccessProvider(AppDbContext contex)
        {
            _context = contex;
        }

       
        // Retrieves a list of all users from the system.
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var list = await _context.Users.ToListAsync();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // Retrieves a user from the system based on the specified ID.
        public User GetUserById(int id)
        {
            try
            {
                return _context.Users.FirstOrDefault(t => t.Id == id);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IQueryable<User> GetUsers()
        {

            var list = _context.Users.ToList();
            return list.AsQueryable();
        }


        // Retrieves an IQueryable collection of users from the system.
        public User AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                // Get the last inserted user
                var lastUser = _context.Users.OrderByDescending(u => u.Id).FirstOrDefault();
                return lastUser;
            }
            catch (Exception ex)
            {
                throw new GraphQLException("An error occurred while adding the user.", ex);
            }
        }

        // Updates the information of an existing user in the system.
        public User UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return _context.Users.FirstOrDefault(t => t.Id == user.Id);
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        // Deletes a user with the specified ID from the system.
        public bool DeleteUser(int id)
        {
            try
            {
                var entity = _context.Users.FirstOrDefault(t => t.Id == id);
                _context.Users.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
