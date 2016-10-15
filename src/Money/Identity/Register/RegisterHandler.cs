using System.Threading.Tasks;
using Money.Identity;
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
      return await Task.FromResult(new RegisterResponse { Success = false });
    }
  }
}