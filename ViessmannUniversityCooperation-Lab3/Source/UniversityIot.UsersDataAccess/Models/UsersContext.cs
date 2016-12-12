using System.Data.Entity;

namespace UniversityIot.UsersDataAccess.Models
{
    public class UsersContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
    }
}