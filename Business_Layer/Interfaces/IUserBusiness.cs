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
        public bool CheckExist(string email);
        public UserEntity CheckEmailPresent(string email);
        public List<UserEntity> GetList();
        public string ForgotPassword(string Email);
        public bool ResetPassword(string email, resetPassword reset);

    }
}
