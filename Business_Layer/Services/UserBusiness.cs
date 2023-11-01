
using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System.Collections.Generic;

namespace Business_Layer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;
        public UserBusiness(IUserRepo userRepo )
        {
            this.userRepo = userRepo;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            return userRepo.UserRegistration( model );
        }

        public string UserLogin(LoginModel loginModel)
        {
            return userRepo.UserLogin(loginModel);
        }

        public bool CheckExist(string email)
        {
            return userRepo.CheckExist(email);
        }
        public UserEntity CheckEmailPresent(string email)
        {
            return userRepo.CheckEmailPresent(email);
        }
        public List<UserEntity> GetList()
        {
            return userRepo.GetList();
        }

        public string ForgotPassword(string Email)
        {
            return userRepo.ForgotPassword(Email);
        }

        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            return userRepo.ResetPassword(email, password, confirmPassword);
        }
        public bool ResetnewPassword(string email, resetPassword reset)
        {
            return userRepo.ResetnewPassword(email, reset);
        }


    }
}
