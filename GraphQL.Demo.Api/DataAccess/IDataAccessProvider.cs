﻿using GraphQL.Demo.Api.Data;

namespace GraphQL.Demo.Api.DataAccess
{
    public interface IDataAccessProvider
    {

        // Retrieves a list of all users from the system.
        Task<List<User>> GetAllUsers();

        // Retrieves an IQueryable collection of users from the system.
        IQueryable<User> GetUsers();

        // Retrieves a user from the system based on the specified ID.
        User GetUserById(int id);

        // Adds a new user to the system.
        User AddUser(User user);

        // Updates the information of an existing user in the system.
        User UpdateUser(User user);

        // Deletes a user with the specified ID from the system.
        bool DeleteUser(int id);
    }
}