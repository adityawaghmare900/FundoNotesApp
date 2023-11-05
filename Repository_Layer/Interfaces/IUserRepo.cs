

using Common_Layer.Models;
using Repository_Layer.Entity;
using System.Collections.Generic;

namespace Repository_Layer.Interfaces
{
    public interface IUserRepo
    {
        public UserEntity UserRegistration(RegisterModel model);
        public string UserLogin(LoginModel loginModel);
        public bool CheckExist(string email);
        public UserEntity CheckEmailPresent(string email);
        public List<UserEntity> GetList();
        public string ForgotPassword(string Email);
        public bool ResetPassword(string email, resetPassword reset);







    }
}
