

using Common_Layer.Models;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
    public interface IUserRepo
    {
            public UserEntity UserRegistration(RegisterModel model);

            public string UserLogin(LoginModel loginModel);


    }
}
