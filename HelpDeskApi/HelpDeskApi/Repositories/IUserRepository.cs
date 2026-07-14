using HelpDeskApi.Models;

namespace HelpDeskApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();

        Task<User?> GetUserByIdAsync(int id);

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int id);
        Task<bool> UserExistsAsync(string username);

        Task<bool> EmailExistsAsync(string email);

        Task RegisterUserAsync(User user);
    }
}
