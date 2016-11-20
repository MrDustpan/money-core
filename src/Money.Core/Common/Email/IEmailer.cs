using System.Threading.Tasks;

namespace Money.Core.Common.Email
{
  public interface IEmailer
  {
    Task Send(EmailMessage message);
  }
}