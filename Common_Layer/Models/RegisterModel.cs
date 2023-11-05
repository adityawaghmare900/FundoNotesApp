using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Createat {  get; set; }
        public DateTime Updateat {  get; set; }
    }
}
