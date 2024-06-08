using CSU_Core.DTO;
using CSU_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(int userId);

        Task<string> GetRoleName(decimal roleId);

    }
}
