namespace CSU_Infra.Service
{
    using CSU_Core.DTO;
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using CSU_Core.Service;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
            await _userRepository.CreateUser(user);
        }

        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUser(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<string> GetRoleName(int roleId)
        {
            return await _userRepository.GetRoleName(roleId);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
        }
    }
}
