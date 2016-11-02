using System.Threading.Tasks;

namespace Money.Domain.Identity.RegisterUser
{
  public interface IConfirmationEmailSender
  {
    Task Send(User user);
  }
}