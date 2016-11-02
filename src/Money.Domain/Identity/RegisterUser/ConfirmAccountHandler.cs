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

    public async Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request)
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