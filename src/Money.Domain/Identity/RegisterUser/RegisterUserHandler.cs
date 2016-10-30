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
      var response = await ValidateRequest(request);
      if (response.Status != RegisterUserStatus.Success)
      {
        return response;
      }

      var user = new User
      {
        Email = request.Email, 
        Password = request.Password,
        ConfirmationId = Guid.NewGuid().ToString()
      };

      await _userRepository.AddAsync(user);
      response.UserId = user.Id;

      await _emailer.SendAsync(user);

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

      var existing = await _userRepository.GetUserByEmailAsync(request.Email);
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