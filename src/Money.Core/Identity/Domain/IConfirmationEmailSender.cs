using System.Threading.Tasks;

namespace Money.Core.Identity.Domain
{
  public interface IConfirmationEmailSender
  {
    Task Send(User user);
  }
}