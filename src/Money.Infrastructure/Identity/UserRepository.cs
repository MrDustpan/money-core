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

    public async Task<User> GetUserByEmailAsync(string email)
    {
      return await Task.FromResult((User)null);
    }
  }
}