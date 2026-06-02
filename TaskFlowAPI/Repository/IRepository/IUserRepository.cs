using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repository.IRepository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserByEmail(string email);
    Task<User?> AddUser(User user);
}