using System.Threading.Tasks;

namespace Money.Infrastructure.Email
{
  public interface IEmailer
  {
    Task Send(EmailMessage message);
  }
}