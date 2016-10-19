using System.Threading.Tasks;
using Money.Infrastructure;

namespace Money.Identity.Register
{
  public class RegisterHandler
  {
    private readonly IRepository<User> _userRepository;

    public RegisterHandler(IRepository<User> userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<RegisterResponse> HandleAsync(RegisterRequest request)
    {
      if (string.IsNullOrWhiteSpace(request.Email))
      {
        return new RegisterResponse { Success = false };
      }

      if (request.Password.Length < 8)
      {
        return new RegisterResponse { Success = false };
      }

      var user = new User { Email = request.Email, Password = request.Password };

      await _userRepository.AddAsync(user);

      return new RegisterResponse { Success = true };
    }
  }
}