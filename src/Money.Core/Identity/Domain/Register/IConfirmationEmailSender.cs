using System.Threading.Tasks;

namespace Money.Core.Identity.Domain.Register
{
  public interface IConfirmationEmailSender
  {
    Task Send(User user);
  }
}