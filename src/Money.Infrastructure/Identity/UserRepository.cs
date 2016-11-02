using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Money.Domain.Identity;
using Money.Infrastructure.DataAccess;

namespace Money.Infrastructure.Identity
{
  public class UserRepository : IUserRepository
  {
    private readonly IDbConnectionFactory _db;
    private const string SelectSql = "select [Id], [Email], [Password], [ConfirmationId], [Status] from [User]";

    public UserRepository(IDbConnectionFactory db)
    {
      _db = db;
    }

    public async Task AddAsync(User user)
    {
      const string sql = 
        @"insert into [User] ([Email], [Password], [ConfirmationId], [Status]) 
        values (@email, @password, @confirmationId, @status)
        
        select cast(SCOPE_IDENTITY() as int)";
      
      using (var conn = await _db.OpenAsync())
      {
        user.Id = (await conn.QueryAsync<int>(sql, user)).Single();
      }
    }

    public async Task UpdateAsync(User user)
    {
      const string sql = 
        @"update [User] set [Email] = @email, [Password] = @password, 
        [ConfirmationId] = @confirmationId, [Status] = @status 
        where [Id] = @id";
      
      using (var conn = await _db.OpenAsync())
      {
        await conn.ExecuteAsync(sql, user);
      }
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
      const string sql = SelectSql + " where [Email] = @email";
      
      using (var conn = await _db.OpenAsync())
      {
        return (await conn.QueryAsync<User>(sql, new { email })).SingleOrDefault();
      }
    }

    public async Task<User> GetUserByConfirmationIdAsync(string confirmationId)
    {
      const string sql = SelectSql + " where [ConfirmationId] = @confirmationId";
      
      using (var conn = await _db.OpenAsync())
      {
        return (await conn.QueryAsync<User>(sql, new { confirmationId })).SingleOrDefault();
      }
    }
  }
}