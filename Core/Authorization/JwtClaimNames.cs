using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization
{
  /// <summary>
  /// Represents a set of json web token claims
  /// </summary>
  public static class JwtClaimNames
  {
    /// <summary>
    /// The subject of the jwt, the user
    /// </summary>
    public const string Sub = "sub";

    /// <summary>
    /// The unique name of the subject
    /// </summary>
    public const string UniqueName = "unique_name";

    /// <summary>
    /// The unique name of the subject
    /// </summary>
    public const string Email = "email";

    /// <summary>
    /// The name of the subject
    /// </summary>
    public const string Name = "name";

    /// <summary>
    /// The surname of the subject
    /// </summary>
    public const string Surname = "family_name";  

    /// <summary>
    /// The idenity provider
    /// </summary>
    public const string Idp = "idp";

    /// <summary>
    /// The user role
    /// </summary>
    public const string Role = "role";

    /// <summary>
    /// The OAuth client id
    /// </summary>
    public const string client_id = "client_id";

  }
}
