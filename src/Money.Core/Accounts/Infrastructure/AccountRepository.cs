using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Money.Core.Accounts.Domain;
using Money.Core.Common.Infrastructure.DataAccess;

namespace Money.Core.Accounts.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
      private readonly IDbConnectionFactory _db;

      public AccountRepository(IDbConnectionFactory db)
      {
        _db = db;
      }

      public async Task Add(Account account)
      {
        const string sql = 
          @"insert into [Account] ([Name], [Balance]) 
          values (@name, @balance)
          
          select cast(SCOPE_IDENTITY() as int)";
        
        using (var conn = await _db.Open())
        {
          account.Id = (await conn.QueryAsync<int>(sql, account)).Single();
        }
      }
    }
}