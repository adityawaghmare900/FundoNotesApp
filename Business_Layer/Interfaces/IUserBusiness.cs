using Common_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IUserBusiness
    {
        UserEntity UserRegistration(RegisterModel model);

        string UserLogin(LoginModel loginmodel);

    }
}
