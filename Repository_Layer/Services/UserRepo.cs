
using Common_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System.Linq;

namespace Repository_Layer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundoDbContext fundoDbContext;

        public UserRepo(FundoDbContext fundoDbContext)
        {
            this.fundoDbContext = fundoDbContext;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity entity = new UserEntity();
            entity.First_Name = model.FirstName;
            entity.Last_Name = model.LastName;
            entity.Email = model.Email;
            entity.Password = model.Password;
            fundoDbContext.Users.Add(entity);
            var result = fundoDbContext.SaveChanges();
            if (result > 0)
            {
                return entity;
            }
            else
            {
                return null;
            }
        }

        public string UserLogin(LoginModel loginModel)
        {
            UserEntity checkEmailPassword=fundoDbContext.Users.FirstOrDefault(x=> x.Email == loginModel.Email && x.Password==loginModel.Password);
            if(checkEmailPassword != null)
            {
                return "login Successful";
            }
            else
            {
                return "login Failed";
            }

        }

        
    }
}
