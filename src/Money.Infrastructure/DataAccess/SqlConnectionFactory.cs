using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Money.Infrastructure.Configuration;

namespace Money.Infrastructure.DataAccess
{
  public class SqlConnectionFactory : IDbConnectionFactory
  {
    private readonly IConfigurationGateway _config;

    public SqlConnectionFactory(IConfigurationGateway config)
    {
      _config = config;
    }
    
    public async Task<IDbConnection> OpenAsync()
    {
      var cs = await _config.GetConnectionStringAsync();
      var conn = new SqlConnection(cs);
      await conn.OpenAsync();
      return conn;
    }
  }
}