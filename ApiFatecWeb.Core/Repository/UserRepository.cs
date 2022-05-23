using ApiFatecWeb.Core.Database;
using ApiFatecWeb.Core.Entity;
using ApiFatecWeb.Code.Helpers;
using ApiFatecWeb.Core.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ApiFatecWeb.Core.Repository;

public class UserRepository : IUserRepository
{
    private readonly BaseDbContext _context;
    private readonly ILogHandler _log;

    public UserRepository(BaseDbContext dbContext, ILogHandler log)
    {
        _context = dbContext;
        _log = log;
    }


    public UserEntity? GetOneByEmail(string email)
    {
        return _context.User.FirstOrDefault(user => user.Email == email);
    }

    public async Task<UserEntity?> GetOneByEmailAsync(string email)
    {
        return await _context.User.FirstOrDefaultAsync(user => user.Email == email);
    }

    public UserEntity? GetOneById(int id)
    {
        return _context.User.FirstOrDefault(user => user.IdUser == id);
    }

    public async Task<UserEntity?> GetOneByIdAsync(int id)
    {
        return await _context.User.FirstOrDefaultAsync(user => user.IdUser == id);
    }

    public IEnumerable<UserEntity?> List()
    {
        return _context.User.ToList();
    }

    public async Task<IEnumerable<UserEntity?>> ListAsync()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<bool> Save(UserEntity user)
    {
        try
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            _log.SaveLog("API | User/Save", "Saved a new user", user.IdUser);
            return true;
        }
        catch (Exception ex)
        {
            _log.SaveLog("API | Error | User/Save", ex.Message, 0);
            throw;
        }
    }


    public async Task<bool> Update(UserEntity user)
    {
        try
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            _log.SaveLog("API | User/Update", "Update a user", user.IdUser);
            return true;
        }
        catch (Exception ex)
        {
            _log.SaveLog("API | Error | User/Update", ex.Message, 0);
            throw;
        }
    }

}