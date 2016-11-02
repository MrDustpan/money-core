using System.Threading.Tasks;
using Money.Boundary.Identity.RegisterUser;

namespace Money.Domain.Identity.RegisterUser
{
  public class ConfirmAccountHandler : IConfirmAccountHandler
  {
    private readonly IUserRepository _userRepository;

    public ConfirmAccountHandler(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<ConfirmAccountResponse> HandleAsync(ConfirmAccountRequest request)
    {
      var response = new ConfirmAccountResponse { Status = ConfirmAccountStatus.Success };

      var user = await _userRepository.GetUserByConfirmationIdAsync(request.Id);

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

      await _userRepository.UpdateAsync(user);

      return response;
    }
  }
}