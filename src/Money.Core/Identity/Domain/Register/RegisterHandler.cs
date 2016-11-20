using System;
using System.Threading.Tasks;
using Money.Core.Identity.Boundary.Register;

namespace Money.Core.Identity.Domain.Register
{
  public class RegisterHandler : IRegisterHandler
  {
    private readonly IUserRepository _userRepository;
    private readonly IConfirmationEmailSender _emailer;
    private readonly IPasswordHasher _hasher;

    public RegisterHandler(
      IUserRepository userRepository, 
      IConfirmationEmailSender emailer,
      IPasswordHasher hasher)
    {
      _userRepository = userRepository;
      _emailer = emailer;
      _hasher = hasher;
    }

    public async Task<RegisterResponse> Handle(RegisterRequest request)
    {
      var response = await ValidateRequest(request);
      if (response.Status != RegisterStatus.Success)
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

    private async Task<RegisterResponse> ValidateRequest(RegisterRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.Email))
      {
        return Response(RegisterStatus.FailureEmailRequired);
      }

      if (string.IsNullOrEmpty(request.Password) || request.Password.Length < 8)
      {
        return Response(RegisterStatus.FailurePasswordRequirementsNotMet);
      }

      if (request.Password != request.ConfirmPassword)
      {
        return Response(RegisterStatus.FailurePasswordAndConfirmDoNotMatch);
      }

      var existing = await _userRepository.GetUserByEmail(request.Email);
      if (existing != null)
      {
        return Response(RegisterStatus.FailureEmailAlreadyExists);
      }

      return Response(RegisterStatus.Success);
    }

    private static RegisterResponse Response(RegisterStatus status)
    {
      return new RegisterResponse { Status = status };
    }
  }
}