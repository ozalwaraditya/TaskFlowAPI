using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repository.IRepository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int userId);
    Task<User?> GetUserByEmail(string email);
    Task<bool> GetUserExists(string email);
    Task<User?> AddUser(User user);
    Task DeleteUser(User user);
    Task Save();
}