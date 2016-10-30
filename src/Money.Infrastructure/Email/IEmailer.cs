using System.Threading.Tasks;

namespace Money.Infrastructure.Email
{
  public interface IEmailer
  {
    Task SendAsync(EmailMessage message);
  }
}