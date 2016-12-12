using System.Data.Entity;

namespace UniversityIot.UsersDataService
{
    using System;
    using System.Threading.Tasks;
    using UniversityIot.UsersDataAccess;
    using UniversityIot.UsersDataAccess.Models;

    public class UsersDataService : IUsersDataService
    {
        public async Task<User> AddUserAsync(User user)
        {
            using (var ctx = new UsersContext())
            {
                ctx.Users.Add(user);

                await ctx.SaveChangesAsync();

                return user;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            using (var ctx = new UsersContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
                if (user != null)
                {
                    ctx.Users.Remove(user);
                }

                await ctx.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserAsync(int id)
        {
            using (var ctx = new UsersContext())
            {
                var user = await ctx.Users.Include(u => u.UserGateways).SingleOrDefaultAsync(u => u.Id == id);

                return user;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            using (var ctx = new UsersContext())
            {
                var dbEntry = await ctx.Users.SingleOrDefaultAsync(u => u.Id == user.Id);

                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.CustomerNumber = user.CustomerNumber;
                    dbEntry.Password = user.Password;
                    dbEntry.UserGateways = user.UserGateways;
                    await ctx.SaveChangesAsync();
                }

                return dbEntry;
            }
        }
    }
}