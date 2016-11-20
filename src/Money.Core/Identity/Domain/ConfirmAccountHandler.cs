using System.Threading.Tasks;
using Money.Core.Identity.Boundary;

namespace Money.Core.Identity.Domain
{
  public class ConfirmAccount : IConfirmAccount
  {
    private readonly IUserRepository _userRepository;

    public ConfirmAccount(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<ConfirmAccountResponse> Execute(ConfirmAccountRequest request)
    {
      var response = new ConfirmAccountResponse { Status = ConfirmAccountStatus.Success };

      var user = await _userRepository.GetUserByConfirmationId(request.Id);

      if (user == null)
      {
        response.Status = ConfirmAccountStatus.FailureConfirmationIdNotFound;
        return response;
      }

      if (user.Status == UserStatus.Confirmed)
      {
        response.Status = ConfirmAccountStatus.FailureAlreadyConfirmed;
        return response;
      }

      user.Status = UserStatus.Confirmed;
      user.ConfirmationId = null;

      await _userRepository.Update(user);

      return response;
    }
  }
}