using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class User : IModel<string>
    {
        public User()
        {
            Roles = new HashSet<UserRole>();
        }
        public string Id { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityKey { get; set; }

        public ICollection<UserRole> Roles { get; }
    }

    public class Role : IModel<string>
    {
        public Role()
        {
            Users = new HashSet<UserRole>();
        }
        public string Id { get; set; }
        public string NormalizedId { get; set; }

        public ICollection<UserRole> Users { get; }
    }

    public class UserRole 
    {
        public UserRole()
        {

        }
        public UserRole(string role):this()
        {
            this.RoleId = role;
        }
        public string UserId { get; set; }
        public User User { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
