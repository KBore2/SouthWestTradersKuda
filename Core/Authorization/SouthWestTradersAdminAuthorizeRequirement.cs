using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization
{
  /// <summary>
  /// Represents requirements for a policy
  /// </summary>
  public class SouthWestTradersAdminAuthorizeRequirement : IAuthorizationRequirement
  {
    /// <summary>
    /// Gets or sets the role
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Initialises the constructor
    /// </summary>
    public SouthWestTradersAdminAuthorizeRequirement(string role)
    {
      Role = role;
    }
  }
}
