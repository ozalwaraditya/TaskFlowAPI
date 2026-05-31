using TaskFlowAPI.Models;

namespace TaskFlowAPI.Repository.IRepository;

public interface IUserRepository
{
    Task<IEnumerable<Users>> GetAllUsers();
}