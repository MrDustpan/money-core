using System.Threading.Tasks;

namespace Money.Boundary.Common.Email
{
  public interface IEmailer
  {
    Task SendAsync(EmailMessage message);
  }
}