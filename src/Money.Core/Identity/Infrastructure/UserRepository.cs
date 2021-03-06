using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Money.Core.Common.Infrastructure.DataAccess;
using Money.Core.Identity.Domain;

namespace Money.Core.Identity.Infrastructure
{
  public class UserRepository : IUserRepository
  {
    private readonly IDbConnectionFactory _db;
    private const string SelectSql = "select [Id], [Email], [Password], [ConfirmationId], [Status] from [User]";

    public UserRepository(IDbConnectionFactory db)
    {
      _db = db;
    }

    public async Task Add(User user)
    {
      const string sql = 
        @"insert into [User] ([Email], [Password], [ConfirmationId], [Status], [FailedAttempts]) 
        values (@email, @password, @confirmationId, @status, @failedAttempts)
        
        select cast(SCOPE_IDENTITY() as int)";
      
      using (var conn = await _db.Open())
      {
        user.Id = (await conn.QueryAsync<int>(sql, user)).Single();
      }
    }

    public async Task Update(User user)
    {
      const string sql = 
        @"update [User] set [Email] = @email, [Password] = @password, 
        [ConfirmationId] = @confirmationId, [Status] = @status 
        where [Id] = @id";
      
      using (var conn = await _db.Open())
      {
        await conn.ExecuteAsync(sql, user);
      }
    }

    public async Task<User> GetUserByEmail(string email)
    {
      const string sql = SelectSql + " where [Email] = @email";
      
      using (var conn = await _db.Open())
      {
        return (await conn.QueryAsync<User>(sql, new { email })).SingleOrDefault();
      }
    }

    public async Task<User> GetUserByConfirmationId(string confirmationId)
    {
      const string sql = SelectSql + " where [ConfirmationId] = @confirmationId";
      
      using (var conn = await _db.Open())
      {
        return (await conn.QueryAsync<User>(sql, new { confirmationId })).SingleOrDefault();
      }
    }
  }
}