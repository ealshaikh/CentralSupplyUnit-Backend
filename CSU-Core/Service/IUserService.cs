namespace CSU_Core.Service
{
    using CSU_Core.DTO;
    using CSU_Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();


        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(int userId);

        Task<string> GetRoleName(int roleId);
    }
}
