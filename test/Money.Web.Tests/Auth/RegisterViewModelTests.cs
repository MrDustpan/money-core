using Money.Boundary.Identity.Register;
using Xunit;
using Money.Web.Features.Auth.ViewModels;

namespace Money.Web.Tests.Auth
{
  public class RegisterViewModelTests
  {
    [Fact]
    public void MissingEmailError()
    {
      var viewModel = new RegisterViewModel();
      viewModel.LoadResult(RegisterStatus.FailureEmailRequired);

      Assert.Equal("Email address is required.", viewModel.ErrorMessage);
    }

    [Fact]
    public void EmailExistsError()
    {
      var viewModel = new RegisterViewModel();
      viewModel.LoadResult(RegisterStatus.FailureEmailAlreadyExists);

      Assert.Equal("An account already exists for that email.", viewModel.ErrorMessage);
    }

    [Fact]
    public void PasswordRequirementsNotMetError()
    {
      var viewModel = new RegisterViewModel();
      viewModel.LoadResult(RegisterStatus.FailurePasswordRequirementsNotMet);

      Assert.Equal("Password must be at least 8 characters.", viewModel.ErrorMessage);
    }

    [Fact]
    public void PasswordsDoNotMatchError()
    {
      var viewModel = new RegisterViewModel();
      viewModel.LoadResult(RegisterStatus.FailurePasswordAndConfirmDoNotMatch);

      Assert.Equal("Password and Confirm Password do not match.", viewModel.ErrorMessage);
    }
  }
}