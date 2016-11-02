using System;
using System.Threading.Tasks;
using Money.Boundary.Identity.RegisterUser;

namespace Money.Domain.Identity.RegisterUser
{
  public class RegisterUserHandler : IRegisterUserHandler
  {
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationEmailSender _emailer;
    private readonly IPasswordHasher _hasher;

    public RegisterUserHandler(
      IUserRepository userRepository, 
      IConfirmationEmailSender emailer,
      IPasswordHasher hasher)
    {
      _userRepository = userRepository;
      _emailer = emailer;
      _hasher = hasher;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserRequest request)
    {
      var response = await ValidateRequest(request);
      if (response.Status != RegisterUserStatus.Success)
      {
        return response;
      }

      var user = new User
      {
        Email = request.Email, 
        Password = _hasher.Hash(request.Password),
        ConfirmationId = Guid.NewGuid().ToString(),
        Status = UserStatus.Pending
      };

      await _userRepository.Add(user);
      response.UserId = user.Id;

      await _emailer.Send(user);

      return response;
    }

    private async Task<RegisterUserResponse> ValidateRequest(RegisterUserRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.Email))
      {
        return Response(RegisterUserStatus.FailureEmailRequired);
      }

      if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
      {
        return Response(RegisterUserStatus.FailurePasswordRequirementsNotMet);
      }

      if (request.Password != request.ConfirmPassword)
      {
        return Response(RegisterUserStatus.FailurePasswordAndConfirmDoNotMatch);
      }

      var existing = await _userRepository.GetUserByEmail(request.Email);
      if (existing != null)
      {
        return Response(RegisterUserStatus.FailureEmailAlreadyExists);
      }

      return Response(RegisterUserStatus.Success);
    }

    private static RegisterUserResponse Response(RegisterUserStatus status)
    {
      return new RegisterUserResponse { Status = status };
    }
  }
}