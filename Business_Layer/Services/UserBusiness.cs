
using Business_Layer.Interfaces;
using Common_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

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
    }
}
