using System.Threading.Tasks;
using Money.Domain.Identity.Boundaries;
using Money.Domain.Identity.Entities;

namespace Money.Domain.Identity.Interactors
{
  public class RegisterUser : IRegisterUser
  {
    private readonly IUserRepository _userRepository;

    public RegisterUser(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.Email))
      {
        return new RegisterUserResponse { Status = RegisterUserStatus.FailureEmailRequired };
      }

      if (request.Password.Length < 8)
      {
        return new RegisterUserResponse { Status = RegisterUserStatus.FailurePasswordRequirementsNotMet };
      }

      var user = new User { Email = request.Email, Password = request.Password };

      await _userRepository.AddAsync(user);

      return new RegisterUserResponse
      { 
        Status = RegisterUserStatus.Success,
        UserId = user.Id
      };
    }
  }
}