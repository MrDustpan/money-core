using System.Threading.Tasks;

namespace Money.Domain.Identity.Register
{
  public interface IConfirmationEmailSender
  {
    Task Send(User user);
  }
}