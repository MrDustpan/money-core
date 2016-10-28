using System;
using System.Threading.Tasks;
using Money.Boundary.Identity.RegisterUser;

namespace Money.Domain.Identity.RegisterUser
{
  public class RegisterUserHandler : IRegisterUserHandler
  {
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationEmailSender _emailer;

    public RegisterUserHandler(IUserRepository userRepository, IConfirmationEmailSender emailer)
    {
      _userRepository = userRepository;
      _emailer = emailer;
    }

    public async Task<RegisterUserResponse> HandleAsync(RegisterUserRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.Email))
      {
        return new RegisterUserResponse { Status = RegisterUserStatus.FailureEmailRequired };
      }

      if (request.Password.Length < 8)
      {
        return new RegisterUserResponse { Status = RegisterUserStatus.FailurePasswordRequirementsNotMet };
      }

      var user = new User
      {
        Email = request.Email, 
        Password = request.Password,
        ConfirmationId = Guid.NewGuid().ToString()
      };

      await _userRepository.AddAsync(user);

      await _emailer.SendAsync(user);

      return new RegisterUserResponse
      { 
        Status = RegisterUserStatus.Success,
        UserId = user.Id
      };
    }
  }
}