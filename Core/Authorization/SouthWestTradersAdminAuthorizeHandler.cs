using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization
{
  /// <summary>
  /// Represents operations to handle policies based on a given requirement
  /// </summary>
  public class SouthWestTradersAdminAuthorizeHandler : AuthorizationHandler<SouthWestTradersAdminAuthorizeRequirement>
  {

    /// <summary>
    /// Handles the registered policy requirements
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement"></param>
    /// <returns></returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SouthWestTradersAdminAuthorizeRequirement requirement)
    {
      if (!context.User.Identity.IsAuthenticated)
      {
        context.Fail();
        return Task.CompletedTask;
      }

      if (!context.User.HasClaim(c => c.Type == JwtClaimNames.Sub))
      {
        context.Fail();
        return Task.CompletedTask;
      }

      var role = context.User.FindFirst(JwtClaimNames.Role)?.Value;

      if (!string.Equals(requirement.Role, role, StringComparison.CurrentCultureIgnoreCase))
      {
        context.Fail();
        return Task.CompletedTask;
      }

      context.Succeed(requirement);
      return Task.CompletedTask;
    }
  }
}
