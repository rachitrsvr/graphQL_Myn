using GraphQL.Demo.Api.Common;
using GraphQL.Demo.Api.Data;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.Api.Users
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class UserMutations
    {
        public async Task<AddUserPayload> AddUserAsync(
         AddUserInput input, [Service] AppDbContext context)
        {
            var user = new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                CompanyName = input.CompanyName,
                Address = input.Address,
                City = input.City,
                County = input.County,
                Postal = input.Postal,
                Phone1 = input.Phone1,
                Phone2 = input.Phone2,
                Email = input.Email,
                WebUrl = input.WebUrl
            };


            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new AddUserPayload(user);
        }

        public async Task<ModifyUserPayload> ModifyUserAsync(
           ModifyUserInput input,
           [Service] AppDbContext context,
           CancellationToken cancellationToken)
        {
            if (input.FirstName.HasValue && input.FirstName.Value is null)
            {
                return new ModifyUserPayload(
                    new UserError("First Name cannot be null", "NAME_NULL"));
            }
            if (input.Email.HasValue && input.Email.Value is null)
            {
                return new ModifyUserPayload(
                    new UserError("Email cannot be null", "EMAIL_NULL"));
            }
            if (input.Phone1.HasValue && input.Phone1.Value is null)
            {
                return new ModifyUserPayload(
                    new UserError("Phone 1 cannot be null", "PHONE1_NULL"));
            }

            User? user = await context.Users.FindAsync(input.Id);

            if (user is null)
            {
                return new ModifyUserPayload(
                    new UserError("User with id not found.", "USER_NOT_FOUND"));
            }

            if (input.FirstName.HasValue)
            {
                user.FirstName = input.FirstName;
            }

            if (input.LastName.HasValue)
            {
                user.LastName = input.LastName;
            }

            if (input.Phone1.HasValue)
            {
                user.Phone1 = input.Phone1;
            }

            if (input.Phone2.HasValue)
            {
                user.Phone2 = input.Phone2;
            }

            if (input.Address.HasValue)
            {
                user.Address = input.Address;
            }

            if (input.County.HasValue)
            {
                user.County = input.County;
            }

            if (input.Postal.HasValue)
            {
                user.Postal = input.Postal;
            }

            if (input.City.HasValue)
            {
                user.City = input.City;
            }

            if (input.Email.HasValue)
            {
                user.Email = input.Email;
            }

            if (input.WebUrl.HasValue)
            {
                user.WebUrl = input.WebUrl;
            }

            if (input.CompanyName.HasValue)
            {
                user.CompanyName = input.CompanyName;
            }

            await context.SaveChangesAsync(cancellationToken);

            return new ModifyUserPayload(user);
        }

        public bool DeleteUser([ID] int id, [Service] AppDbContext context)
        {
            try
            {
                var entity = context.Users.FirstOrDefault(t => t.Id == id);
                if (entity == null)
                    return false;
                context.Users.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}