using System.Data;
using System.Threading.Tasks;

namespace Money.Core.Common.DataAccess
{
  public interface IDbConnectionFactory
  {
    Task<IDbConnection> Open();
  }
}