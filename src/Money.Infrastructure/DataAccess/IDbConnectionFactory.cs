using System.Data;
using System.Threading.Tasks;

namespace Money.Infrastructure.DataAccess
{
  public interface IDbConnectionFactory
  {
    Task<IDbConnection> Open();
  }
}