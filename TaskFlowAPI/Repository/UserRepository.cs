using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Models;
using TaskFlowAPI.Repository.IRepository;

namespace TaskFlowAPI.Repository
{
    public class UserRepository(AppDbContext _dbContext) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            return await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> GetUserExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> AddUser(User user)
        {
            if(user == null)
                return null;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}