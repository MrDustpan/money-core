using System.Threading.Tasks;

namespace Money.Domain.Identity.RegisterUser
{
  public interface IConfirmationEmailSender
  {
    Task SendAsync(User user);
  }
}