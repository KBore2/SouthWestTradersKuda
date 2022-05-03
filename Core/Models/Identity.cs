using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
  /// <summary>
  /// Represents the current identity
  /// </summary>
  public class Identity
  {   
      /// <summary>
      /// Gets or sets the user.
      /// </summary>
      [Key]
      public string UserId { get; set; }

      /// <summary>
      /// Gets or sets the username of the user.
      /// </summary>
      public string Username { get; set; }

      /// <summary>
      /// Gets or sets the user type description
      /// </summary>
      public string UserType { get; set; }

      /// <summary>
      /// Gets or sets the name of the user.
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Gets or sets the surname of the user
      /// </summary>
      public string Surname { get; set; }

      /// <summary>
      /// Gets or sets the telephone number of the user
      /// </summary>
      public string TelephoneNumber { get; set; }

      /// <summary>
      /// Gets or sets the email address of the user
      /// </summary>
      public string Email { get; set; }

      /// <summary>
      /// Gets or sets the roles that the user belongs to.
      /// </summary>
      public IEnumerable<string> Roles { get; set; }

    }
  }
