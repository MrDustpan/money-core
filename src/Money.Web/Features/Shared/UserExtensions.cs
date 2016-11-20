using System;
using System.Security.Claims;

namespace Money.Web.Features.Shared
{
  public static class UserExtensions
  {
    public static int GetId(this ClaimsPrincipal user)
    {
      if (user == null)
      {
        throw new Exception("Cannot find user ID: user is null.");
      }

      var claim = user.FindFirst(ClaimTypes.NameIdentifier);
      if (claim == null)
      {
        throw new Exception("Cannot find user ID: NameIdentifier claim is null.");
      }

      int userId;
      if (!int.TryParse(claim.Value, out userId))
      {
        throw new Exception("Cannot find user ID: NameIdentifier claim is not an int.");
      }

      return userId;
    }
  }
}