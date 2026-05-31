using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Repository;

public class UserRepository : IUserRepository
{
    public AppDbContext _dbContext { get; set; }
    
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }
}