using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Money.Core.Common.Infrastructure.Configuration;

namespace Money.Core.Common.Infrastructure.DataAccess
{
  public class SqlConnectionFactory : IDbConnectionFactory
  {
    private readonly IConfigurationGateway _config;

    public SqlConnectionFactory(IConfigurationGateway config)
    {
      _config = config;
    }
    
    public async Task<IDbConnection> Open()
    {
      var cs = await _config.GetConnectionString();
      var conn = new SqlConnection(cs);
      await conn.OpenAsync();
      return conn;
    }
  }
}