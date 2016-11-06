using System.Data;
using System.Threading.Tasks;

namespace Money.Core.Common.Infrastructure.DataAccess
{
  public interface IDbConnectionFactory
  {
    Task<IDbConnection> Open();
  }
}