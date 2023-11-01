
using Common_Layer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundoDbContext fundoDbContext;
        private IConfiguration configuration;

        public UserRepo(FundoDbContext fundoDbContext, IConfiguration configuration)
        {
            this.fundoDbContext = fundoDbContext;
            this.configuration = configuration;
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
            var EncodedPassword = EncryptPass(loginModel.Password);
            UserEntity checkEmailPassword=fundoDbContext.Users.FirstOrDefault(x=> x.Email == loginModel.Email && x.Password==EncodedPassword);
            if(checkEmailPassword != null)
            {
                var token = GenerateJWTToken(checkEmailPassword.Email, checkEmailPassword.UserId);
                return token;
            }
            else
            {
                return "Login Failed";
            }
        }
        

        public string EncryptPass(string password)
        {
            try
            {
                string msg = "";
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                msg = Convert.ToBase64String(encode);
                return msg;
            }
            catch
            {
                throw;
            }
        }

        public string DecodeFrom64(string encodedData)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        private string GenerateJWTToken(string Email,int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public bool CheckExist(string email)
        {
            var checkEmail=fundoDbContext.Users.Where(x=>x.Email == email).Count();
            return checkEmail > 0;
        }

        public UserEntity CheckEmailPresent(string email)
        {
            try
            {
                var checkEmail = fundoDbContext.Users.FirstOrDefault(x => x.Email == email);
                return checkEmail;
            }
            catch(Exception ex) 
            {
                throw;
            }
        }

        public List<UserEntity> GetList()
        {
            try
            {
                List<UserEntity> result = (List<UserEntity>)fundoDbContext.Users.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ForgotPassword(string Email)
        {
            try
            {
                var result = fundoDbContext.Users.FirstOrDefault(x => x.Email == Email);
                if (result != null)
                {
                    var token = this.GenerateJWTToken(result.Email, result.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.SendMessage(token, result.Email, result.First_Name);
                    return token.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch( Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var user = fundoDbContext.Users.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = confirmPassword;
                    fundoDbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetnewPassword(string email, resetPassword reset)
        {
            try
            {
                if (reset.Password.Equals(reset.ConfirmPassword))
                {
                    var user = fundoDbContext.Users.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = reset.ConfirmPassword;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
