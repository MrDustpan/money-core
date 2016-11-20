using System.Threading.Tasks;

namespace Money.Core.Common.Infrastructure.Email
{
  public interface IEmailer
  {
    Task Send(EmailMessage message);
  }
}