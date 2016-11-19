using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Money.Core.Accounts.Boundary.GetAccountIndex;
using Money.Core.Accounts.Domain;
using Money.Core.Common.Infrastructure.DataAccess;

namespace Money.Core.Accounts.Infrastructure
{
  public class AccountGateway : IAccountGateway
  {
    private readonly IDbConnectionFactory _db;

    public AccountGateway(IDbConnectionFactory db)
    {
      _db = db;
    }

    public async Task<GetAccountIndexResponse> GetAccountIndex(GetAccountIndexRequest request)
    {
      const string sql = "select [Id], [Name], [Balance] from [Account]";
      
      using (var conn = await _db.Open())
      {
        return new GetAccountIndexResponse
        {
          Accounts = (await conn.QueryAsync<AccountIndexItem>(sql)).OrderBy(x => x.Name).ToList()
        };
      }
    }
  }
}