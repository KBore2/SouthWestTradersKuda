using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Controllers
{
  public class UsersController
  {
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
      _userService = userService;
    }
    public string GetUser()
    {
     // var user = new UserService();
      return _userService.GetUsername();
    }
  }

  public class UserService : IUserService
  {
    public string Username { get; set;}
    public UserService(string action, int val)
    {

    }

    public string GetUsername()
    {
      return "Kuda";
    }
  }

  public interface IUserService
  {
    public string GetUsername();
  }
}
